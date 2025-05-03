using System;
using System.Linq;
using Abstractions;
using Godot;
using Godot.Collections;
using Tiles;

namespace Grid {
    public partial class Model : Node {
        private int columns = 0;
        private int rows = 0;
        private Array<Array<string>> tileNameMatrix = new Array<Array<string>>();
        private Array<Array<Tile>> grid = new Array<Array<Tile>>();
        private Array<PackedScene> tileResources;
        private Tiles.Factory tileFactory;
        private Array<Array<Control>> observers = new Array<Array<Control>>();  
        public Array<Array<Tile>> Grid { get => grid; }
        public Model(
            Array<PackedScene> tileResources_, //HUHH???? Ce fac cu astea??
            Array<Array<string>> tileNameMatrix_,
            Tiles.Factory tileFactory_
        ){
            tileResources = tileResources_;//.ToArray().Select(item => (PackedScene) item).ToArray();
            tileNameMatrix = tileNameMatrix_;
            tileFactory = tileFactory_;
        }
        public void Initialize(){
            rows = tileNameMatrix.Count;
            columns = tileNameMatrix[0].Count;
            observers.Resize(rows);
            foreach(Array<Control> observer in observers){
                observer.Resize(columns);
            }
            //Console.WriteLine("GRID MODEL INITIALIZED");
        }
        public void CreateGrid() {

            grid.Resize(rows);
            for (int x = 0; x < rows; x++) {
                grid[x].Resize(columns);
                for(int y = 0; y < columns; y++) {
                    grid[x][y] = tileFactory.Create(tileNameMatrix[x][y], new Vector2I(x, y));
                }
            }  
        }

        public void Register(Control tileNode_, int x_, int y_) {
            var observer = observers[x_][y_] = tileNode_;
            var tileModel = ((Modelable) observer).Model;

            //((Tiles.Model)tileModel).TriedSwapping += (Vector2I source, Vector2I direction) => Swap2(source, direction); //Jesus Christ what an ordeal
            ((Swapable)tileModel).ConnectWithSwapSignal((Vector2I source, Vector2I direction) => Swap2(source, direction));
        }


        private void Swap2(Vector2I source, Vector2I direction){
            Console.WriteLine("source and direction below \n");
            Console.WriteLine("source:  ", source.X, "   ", source.Y);
            Console.WriteLine("direction:  ", direction.X, "   ", direction.Y);

            (var sourceMatches, var destinationMatches) = FindMatchGroups(source, direction);
        }

        private (Array<Vector2I>, Array<Vector2I>) FindMatchGroups(Vector2I source, Vector2I direction){
            var destination = source + direction;
            GD.Print("dir   " + direction);
            Array<Vector2I> sourceMatches = [];
            Array<Vector2I> destinationMatches = [];
            if(destination.X >= 0 && destination.Y >= 0){
                var newGrid = grid.Duplicate();
                var sourceTile = newGrid[source.X][source.Y];
                var destinationTile = newGrid[destination.X][destination.Y];

                newGrid[source.X][source.Y] = destinationTile;
                newGrid[destination.X][destination.Y] = sourceTile;

                GridUtilities.PrintGridInitials(
                    GridUtilities.GetNamesGridFromTileGrid(newGrid),
                    "SWAPPED GRID"
                );

                var threeTileSourceMatches = FindMatchesWith(sourceTile, newGrid);
                var threeTileDestinationMatches = FindMatchesWith(destinationTile, newGrid);
                if(threeTileSourceMatches.Count > 0){ //this does not allow L combinations where a 90deg L is matched at the origin, where the actor sits at the shorter side and can short match
                    //check if anyy adjacent to actor
                    sourceMatches = threeTileSourceMatches;
                } else {
                    //check if any actor short matches are compatible
                    var twoTileSourceMatches = GridUtilities.FindAllMatchingAdjacentTiles(sourceTile, newGrid); 
                    if(twoTileSourceMatches.Count > 0){ 
                        sourceMatches = twoTileSourceMatches;    
                    }                
                }

                if(threeTileDestinationMatches.Count > 0){
                    //check if anyy adjacent to actor
                    destinationMatches = threeTileDestinationMatches;
                } else {
                    //check if any actor short matches are compatible
                    var twoTileDestinationMatches = GridUtilities.FindAllMatchingAdjacentTiles(destinationTile, newGrid);
                    if(twoTileDestinationMatches.Count > 0){ 
                        destinationMatches = twoTileDestinationMatches;    
                    }                
                }
            }

            return (sourceMatches: sourceMatches, destinationMatches: destinationMatches);
        }


        private Array<Vector2I> FindMatchesWith(Tile tile_, Array<Array<Tile>> grid_){
            var horizontalMatches = FindHorizontal(tile_, grid_);
            var verticalMatches = FindVertical(tile_, grid_);
            var mergedCsharpArray = horizontalMatches.Concat(verticalMatches).ToArray();            
            return new Array<Vector2I>(mergedCsharpArray);
        }

        private Array<Vector2I> FindHorizontal(Tile tile_, Array<Array<Tile>> grid_){
            var name = tile_.Name;
            var matches = new Array<Vector2I>();

            for(int x = 0; x < rows; x++){
                for(int y = 0; y < columns; y++){
                    if(y > 0 && y < (columns - 1)){
                        if(
                            name == grid_[x][y - 1].Name && 
                            name == grid_[x][y].Name && 
                            name == grid_[x][y + 1].Name 					
                        ){
                            matches.Add(new Vector2I(x, y - 1));
                            matches.Add(new Vector2I(x, y));
                            matches.Add(new Vector2I(x, y + 1));                              
                        }       
                    }  
                }                    
            }						
            return Collections.RemoveDuplicates(matches);	
        }

        private Array<Vector2I> FindVertical(Tile tile_, Array<Array<Tile>> grid_){
            var name = tile_.Name;
            var matches = new Array<Vector2I>();

            for(int x = 0; x < columns; x++){
                for(int y = 0; y < rows; y++){
                    if(y > 0 && y < (rows - 1)){
                        if(
                            name == grid_[y - 1][x].Name && 
                            name == grid_[y][x].Name && 
                            name == grid_[y + 1][x].Name 					
                        ){
                            matches.Add(new Vector2I(y - 1, x));
                            matches.Add(new Vector2I(y, x));
                            matches.Add(new Vector2I(y + 1, x));                              
                        }       
                    }  
                }                    
            }						
            return Collections.RemoveDuplicates(matches);	
        }  

        // private Array<Vector2I> FindTwoTileMatch(Tile tile_, Array<Array<Tile>> grid_){
        //     Array<Vector2I> matches = [];
        //     matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Up)); 
        //     matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Right)); 
        //     matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Down)); 
        //     matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Left)); 
            
        //     var cSharpValidMatches = matches.Where(match => match.X >= 0 && match.Y >= 0);
        //     return new Array<Vector2I>(cSharpValidMatches);
        // }   

        // private Vector2I FindMatchWithAdjacentTile(Tile tile_, Array<Array<Tile>> grid_, Vector2I direction){
        //     if(GridUtilities.CheckIfDirectionExists(tile_.Position, direction)){
        //         var neighboringPosition = tile_.Position + direction;
        //         if(tile_.Name == grid_[neighboringPosition.X][neighboringPosition.Y].Name){
        //             return neighboringPosition;
        //         }
        //     } 
        //     return new Vector2I(-1, -1);
        // }   
       
    }

}

