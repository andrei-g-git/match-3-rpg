using System;
using System.Linq;
using Abstractions;
using Constants;
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
            var tileModel = ((/* Modelable */Controllable) observer).Model;
            //((Viewable) observer).SignalEmitter = tileModel;   in node factory

            //((Tiles.Model)tileModel).TriedSwapping += (Vector2I source, Vector2I direction) => Swap2(source, direction); //Jesus Christ what an ordeal
            ((Swapable)tileModel).ConnectWithSwapSignal((Vector2I source, Vector2I direction) => Swap2(source, direction));
        }


        private void Swap2(Vector2I source, Vector2I direction){
            Console.WriteLine("source and direction below \n");
            Console.WriteLine("source:  ", source.X, "   ", source.Y);
            Console.WriteLine("direction:  ", direction.X, "   ", direction.Y);

            Vector2I destination = source + direction;
            (var sourceMatches, var destinationMatches) = FindMatchGroups(source, direction);

            Control sourceNode = observers[source.X][source.Y]; //MAKE SURE THESE CHANGE WITH THE MODEL
            Control destinationNode = observers[destination.X][destination.Y];
            if((sourceMatches.Count > 0) || (destinationMatches.Count > 0)){ //not enough but w/e
                ((Viewable) sourceNode).Update(destination); //test
                ((Viewable) destinationNode).Update(source);                
            }

            var (sourceMatchColumn, sourceMatchRow) = FindLines(sourceMatches);
            var (destinationMatchColumn, destinationMatchRow) = FindLines(destinationMatches);

            GD.Print("source matches \n" + sourceMatches);
            GD.Print("destination matches \n" + destinationMatches);

            GD.Print("source lines \n" + sourceMatchColumn + "\n" + sourceMatchRow);
            GD.Print("destination lines \n" + destinationMatchColumn + "\n" + destinationMatchRow);

            var playerPosition = FindTilesByName(TileNames.Player)[0];
            var collapsePath = MakeCollapsePath(sourceMatchColumn, playerPosition);
            GD.Print("Path:  \n" + collapsePath);
            DestroyMatches(collapsePath);
        }

        private (Array<Vector2I>, Array<Vector2I>) FindMatchGroups(Vector2I source, Vector2I direction){
            var destination = source + direction;
            GD.Print("dir   " + direction);
            Array<Vector2I> sourceMatches = [];
            Array<Vector2I> destinationMatches = [];
            if(destination.X >= 0 && destination.Y >= 0){
                var newGrid = grid.Duplicate();
                var sourceTile = newGrid[source.X][source.Y]; //these shouldn't be here...
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
                    GD.Print("match 3");
                } else {
                    if(CheckIfSwappingActor(source, destination)){
                        GD.Print("player is part of swap");
                        //check if any actor short matches are compatible
                        var twoTileSourceMatches = GridUtilities.FindAllMatchingAdjacentTiles(sourceTile, newGrid); 
                        if(twoTileSourceMatches.Count > 0){ 
                            sourceMatches = twoTileSourceMatches;    
                        }                          
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
            var sourceMatchesWithoutDupes = Collections.RemoveDuplicates(sourceMatches);
            var destinationMatchesWithoutDupes = Collections.RemoveDuplicates(destinationMatches);
            return (sourceMatchesWithoutDupes, destinationMatchesWithoutDupes);
        }

        private bool CheckIfSwappingActor(Vector2I source, Vector2I destination){
            Tile sourceTile = grid[source.X][source.Y];
            Tile destinationTile = grid[destination.X][destination.Y];
            string player = TileNames.Player.ToString().ToLower();
            GD.Print("source: " + sourceTile.Name + "  dest:  " + destinationTile.Name);
            return (sourceTile.Name == player || destinationTile.Name == player);
        }

        private Array<Vector2I> FindTilesByName(TileNames tile){
            string tileName = tile.ToString().ToLower();
            Array<Vector2I> tilePositions = [];
            for(int x = 0; x < grid.Count; x++){
                for(int y = 0; y < grid[x].Count; y++){
                    if((grid[x][y] is not null) && (grid[x][y].Name == tileName)){
                        //tilePositions.Add(grid[x][y].Position);
                        tilePositions.Add(new Vector2I(x, y));
                        var bp = 1123;
                    }
                }
            }
            return tilePositions;
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

        private (Array<Vector2I>, Array<Vector2I>) FindLines(Array<Vector2I> matchGroup){
            Array<Vector2I> column = [];
            for(int a = 0; a < matchGroup.Count; a++){
                Array<Vector2I> line = [matchGroup[a]];
                for(int b = a + 1; b < matchGroup.Count; b++){
                    if(matchGroup[a].X == matchGroup[b].X){
                        line.Add(matchGroup[b]);
                    }
                }
                //if(line.Except(column).Any()){ //nope this can replace a large column with one tile outside the column
                if(line.Count > column.Count){
                    column = line; //assume there can be only one column
                }
            }

            Array<Vector2I> row = [];
            for(int a = 0; a < matchGroup.Count; a++){
                Array<Vector2I> line = [matchGroup[a]];
                for(int b = a + 1; b < matchGroup.Count; b++){
                    if(matchGroup[a].Y == matchGroup[b].Y){
                        line.Add(matchGroup[b]);
                    }
                }
                if(line.Count > row.Count){
                    row = line; //assume there can be only one row
                }
            }  
            var columnOrdered = column.OrderBy(v => v.Y);
            var rowOrdered = row.OrderBy(v => v.X);
            return (
                new Array<Vector2I>(columnOrdered),
                new Array<Vector2I>(rowOrdered)
            );          
        }


        Array<Vector2I> MakeCollapsePath(Array<Vector2I> line, Vector2I playerPosition){//there are a bajillion scenarios and permutations and I'll have to make cases for all of them...
            //right now it will only have the simplest path type so I can go on with tile iteration 
            var path = line;
            for(int i = 0; i < line.Count; i++){
                if(
                    line[i] + Vector2I.Up == playerPosition ||
                    line[i] + Vector2I.Right == playerPosition ||
                    line[i] + Vector2I.Down == playerPosition ||
                    line[i] + Vector2I.Left == playerPosition 
                ){
                    if(i > line.Count - 1 - i){
                        path.Reverse();
                        break;
                    }
                }
            }
            return path;
        }

        private void DestroyMatches(/* Array<Tile> allMatches */Array<Vector2I> allMatches){
            var playerPosition = FindTilesByName(TileNames.Player)[0]; //this function should be a utility
            var player = grid[playerPosition.X][playerPosition.Y];
            var playerIsAdjacent = CheckIfTileIsNextToPath(allMatches, playerPosition);            
            for(int i = 0; i < allMatches.Count; i++){
                //var tile = allMatches[i];
                var pos = allMatches[i];
                // var xx = tile.Position.X;
                // var yy = tile.Position.Y;
                var tile = grid[pos.X][pos.Y];

                if(playerIsAdjacent){ //this forwards the position of the player before the above first combatable buff at i = 1, which will attempt to cast the player as melee
                    playerPosition = FindTilesByName(TileNames.Player)[0];                
                    player = grid[playerPosition.X][playerPosition.Y];
                    //Combatable.Model combatTile = tile as Combatable.Model;
                    if(tile is BuffableDamage.Model){
                    //if(combatTile != null){
                        ((BuffableDamage.Model) player).IncreaseDamageOfMelee(((BuffableDamage.Model) tile).MeleeBuff);
                        ((BuffableDamage.Model) player).IncreaseDamageOfRanged(((BuffableDamage.Model) tile).RangedBuff);
                        ((BuffableDamage.Model) player).IncreaseDamageOfSpell(((BuffableDamage.Model) tile).SpellBuff);
                        var bp = 123;
                    }   




                    grid[/* xx */pos.X][/* yy */pos.Y] = player; //but then player.Position will remain unchanged...
                    
                    ((Transportable.Model) player).NotifyTransport(new Vector2I(/* xx */pos.X, /* yy */pos.Y));
                    grid[playerPosition.X][playerPosition.Y] = null; //maybe going with null values isn't such a hot idea...
                }
                 




            }
        }

        private void ReplaceCollapsingTileWithActor(Tile tile){ //hmm helper function inside a helper function I dunno about this..
            var transportables = tile.Transportables;
            if(transportables.Contains(TileNames.Player)){
                

            }
        }

        private bool CheckIfTileIsNextToPath(/* Array<Tile> */ Array<Vector2I> tileLine, Vector2I tilePosition){
            for(int i = 0; i < tileLine.Count; i++){
                if(
                    tileLine[i]/* .Position */ + Vector2I.Up == tilePosition ||
                    tileLine[i]/* .Position */ + Vector2I.Right == tilePosition ||
                    tileLine[i]/* .Position */ + Vector2I.Down == tilePosition ||
                    tileLine[i]/* .Position */ + Vector2I.Left == tilePosition 
                ){ 
                    return true;
                }               
            }
            return false;
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

