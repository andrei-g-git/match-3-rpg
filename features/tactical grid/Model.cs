using System;
using System.Linq;
using System.Threading.Tasks;
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
        private Array<Array<Tiles.Model>> grid = new Array<Array<Tiles.Model>>();
        private Array<PackedScene> tileResources;
        private Tiles.Factory tileFactory;
        private Array<Array<TileNode>> observers = new Array<Array<TileNode>>();  
        public Array<Array<TileNode>> Grid { get => observers; }
        public Model(
            Array<PackedScene> tileResources_, //HUHH???? Ce fac cu astea??
            Array<Array<string>> tileNameMatrix_,
            Tiles.Factory tileFactory_
        ){
            tileResources = tileResources_;
            tileNameMatrix = tileNameMatrix_;
            tileFactory = tileFactory_;
        }
        public void Initialize(){
            rows = tileNameMatrix.Count;
            columns = tileNameMatrix[0].Count;
            observers.Resize(rows);
            foreach(Array<TileNode> observer in observers){
                observer.Resize(columns);
            }
        }

        public void Register(TileNode tileNode_, int x_, int y_) {
            SetTile(tileNode_, x_, y_);

            var tileModel = ((Controllable) tileNode_).Model;
            (tileModel as GridItem).Board = this;

            //trying this too because why not....
            //(tileNode_.Controller as Tiles.Controller).Model = tileModel; //apparently I gotta do it like this, exportiing doesn't work...(OBVIOUSLY IT SHOULD BE DONE FIRST)
            //((Swapable.Model)tileModel).ConnectWithSwapSignal((Vector2I source, Vector2I direction) => Swap2(source, direction));
        } 

        public void SetTile(TileNode tileNode_, int x_, int y_) { 
            var observer = observers[x_][y_] = tileNode_;
            var tileModel = ((Controllable) observer).Model;
            (tileModel as Tiles.Model).Position = new Vector2I(x_, y_);

            if(x_ == 0 && y_ == 0){(tileModel as Tiles.Model).Position = new Vector2I(69, 420);} //test
            var bp = 234;
        }

        public Tiles.Model GetTileModel(Vector2I position){
            return observers[position.X][position.Y].Model as  Tiles.Model;
        }

        public Array<Array<Tiles.Model>> GetAllTileModels(){
            var columns = observers.Count;
            var rows = observers[0].Count;
            var models = new Array<Array<Tiles.Model>>();
            models.Resize(columns);
            for(int x=0; x<columns; x++){
                models[x].Resize(rows);
                for(int y=0; y<rows; y++){
                    models[x][y] = (Tiles.Model) observers[x][y].Model;
                }
            }
            return models;
        }
        public Array<Tiles.Model> GetModelsFromMatches(Array<Vector2I> matches){
            var matchedModels = new Godot.Collections.Array<Tiles.Model>();
            foreach(var match in matches){
                matchedModels.Add(observers[match.X][match.Y].Model as Tiles.Model);
            }
            return matchedModels;
        }

        public void ConnectAllMatchesWithSwappedTile(Matchable.Model swappedTileMatcher, Array<Vector2I> matches){
            var matchedModels = GetModelsFromMatches(matches);
            foreach(var model in matchedModels){
                //model.SwapBehavior.MatchEmitter = (Node) swappedTileMatcher;
                (model as Listenable).Connect(swappedTileMatcher as Node);
            }
        }

        public void PrintGridInitials(string header){
            var nameMatrix = new Array<Array<String>>();
            var cols = observers.Count;
            var rows = observers[0].Count;
            nameMatrix.Resize(cols);
            for(int x=0; x<cols; x++){
                nameMatrix[x].Resize(rows);
                for(int y=0; y<cols; y++){
                    nameMatrix[x][y] = (observers[x][y].Model as Tiles.Model).Name; //name is a very dangerous interface since Node has it too and if I don't cast I'll get the wrong value
                }
            }
            GridUtilities.PrintGridInitialsFromStringMatrix(nameMatrix, header);
        }
/////////////////////////////////////////////////////////////
        public Array<Vector2I> TryMatching(Vector2I source, Vector2I direction){
            Vector2I destination = source + direction;
            if(destination.X >= 0 && destination.Y >= 0){
                var newGrid = observers.Duplicate(true);
                var sourceTile = observers[source.X][source.Y]; //these shouldn't be here...
                var destinationTile = observers[destination.X][destination.Y];

                // GridUtilities.PlaceTileOnBoard(destinationTile, newGrid, source.X, source.Y); //can't update player model position here
                // GridUtilities.PlaceTileOnBoard(sourceTile, newGrid, destination.X, destination.Y);
                newGrid[source.X][source.Y] = destinationTile;
                newGrid[destination.X][destination.Y] = sourceTile;

                (var sourceMatches, var destinationMatches) = FindMatchGroups(sourceTile, destinationTile, newGrid);

                 TileNode sourceNode = observers[source.X][source.Y]; //MAKE SURE THESE CHANGE WITH THE MODEL
                 TileNode destinationNode = observers[destination.X][destination.Y];
                if((sourceMatches.Count > 0) || (destinationMatches.Count > 0)){ //not enough but w/e      
                    //new
                    SetTile(destinationTile, source.X, source.Y); //this is a bit redundant but I can only change player model positions here...
                    SetTile(sourceTile, destination.X, destination.Y);


                    (sourceNode.SwapAnimator as Swapable.View).SwapTo(destination);
                    (destinationNode.SwapAnimator as Swapable.View).SwapTo(source);    
                    observers = newGrid;        

                    var (sourceMatchColumn, sourceMatchRow) = FindLines(sourceMatches);
                    var (destinationMatchColumn, destinationMatchRow) = FindLines(destinationMatches);

                    var collapsePath = MakeCollapsePath(
                        sourceMatchColumn, 
                        FindTilesByName(TileNames.Player)[0]
                    );
                    GD.Print("Path:  \n" + collapsePath);    

                    return collapsePath;                                                                                           
                }
            }
            return [];
        }

        public Tiles.Model TryMoving(Vector2I source, Vector2I direction){
            var destination = source + direction;
            var targetTile = observers[destination.X][destination.Y];
            if((targetTile as Obtainable.Model).IsObtainable){
                var sourceTile = observers[source.X][source.Y];
                SetTile(sourceTile, destination.X, destination.Y);
                return targetTile.Model as Tiles.Model;
            }
            return null;
        }

        
        public void Fight(Tiles.Model attacker, Tiles.Model defender){
            (attacker as Offensive.Model).Attack(defender);
        }


        public void BuffDamage(Tiles.Model tile, Vector2I positionInPath){
            var playerPosition = FindTilesByName(TileNames.Player)[0];                
            var player = observers[playerPosition.X][playerPosition.Y];
            var pos = positionInPath;
            if(tile is BuffableDamage.Model){ //probably shouldn't buff if active actor not adjacent
                ((BuffableDamage.Model) player.Model).IncreaseDamageOfMelee(((BuffableDamage.Model) tile).MeleeBuff);
                ((BuffableDamage.Model) player.Model).IncreaseDamageOfRanged(((BuffableDamage.Model) tile).RangedBuff);
                ((BuffableDamage.Model) player.Model).IncreaseDamageOfSpell(((BuffableDamage.Model) tile).SpellBuff);
            }   

            // //GridUtilities.PlaceTileOnBoard(player, observers, pos.X, pos.Y);
            // SetTile(player, pos.X, pos.Y);
            // //observers[pos.X][pos.Y] = player; 
            
            // ((Transportable.Model) player.Model).NotifyTransport(new Vector2I(pos.X, pos.Y));

            // var emitter = ((player as Animatable).Animators as Player.Animators).TransportAnimator as Node;
            // await ToSignal(emitter, "Transported");
        }

        public Tiles.Model GetPlayer(){
            var playerPosition = FindTilesByName(TileNames.Player)[0];                
            return observers[playerPosition.X][playerPosition.Y].Model as Tiles.Model;            
        }   

        public bool CheckIfActorNearPath(TileNode actor, Array<Vector2I> path){
            var position = (actor.Model as Tiles.Model).Position;
            return CheckIfTileIsNextToPath(path, position);
        }     

        //THANKFULLY I MAY NOT HAVE TO USE THIS DISGUSTING ABOMINATION
        //the behavior to behavior signal way of doing in doesn't work, behavior does not receive ok signal emitter before receiving singal
        //provisory
        public void NotifyMathedTileToPerformBehaviors(Array<Vector2I> matches){
            foreach(var match in matches){
                var tile = observers[match.X][match.Y].Model as Tiles.Model;
                if(
                    tile is Tiles.Melee.Model /* || 
                    tile is Tiles.Player.Model */
                ){
                    ((tile as Tiles.Melee.Model).DamageBuffer as Behavioral).Fulfill(); //well this is apocaliptic...   
                }

                ((tile as Tiles.Melee.Model).Transfer as Behavioral).Fulfill();
            }
        }

        public Tiles.Model GetActorModel(TileNames actor){ //should be limited to actors, not all tiles
            var pos =  FindTilesByName(actor)[0];
            return observers[pos.X][pos.Y].Model as Tiles.Model;
        }
        public TileNode GetActor(TileNames actor){ //should be limited to actors, not all tiles
            var pos =  FindTilesByName(actor)[0];
            return observers[pos.X][pos.Y];
        }    

        public Node TransferTile(Vector2I target, Node emitter){
            var playerNode = GetActor(TileNames.Player);
            SetTile(playerNode, target.X, target.Y); 

            //(playerNode as Listenable).SignalEmitter = emitter;
            var transportAnimator = ((playerNode as Animatable).Animators as Player.Animators).TransportAnimator;
            (transportAnimator as Listenable).Connect(emitter);

            (emitter as Transfering.Model).ConnectTransferingActor((playerNode.Model as Haulable.Model).AssessSurroundings);

            return transportAnimator as Node;
        }  

        public void AssessSurroundings(Tiles.Model actor, Vector2I position){
            var neighbors = GetSurroundingTileModels(position);
            foreach(var tile in neighbors){
                if((tile as Hostility.Model).IsEnemy){
                    //EmitSignal(Si)
                }
            }
        }
//   /////////////////////////////////////////////////////////
        public Tiles.Model JustSwap(Vector2I source, Vector2I destination){
            var sourceTile = observers[source.X][source.Y]; 
            var destinationTile = observers[destination.X][destination.Y];

            GridUtilities.PlaceTileOnBoard(destinationTile, observers, source.X, source.Y);
            GridUtilities.PlaceTileOnBoard(sourceTile, observers, destination.X, destination.Y);

            (sourceTile.SwapAnimator as Swapable.View).SwapTo(destination);
            (destinationTile.SwapAnimator as Swapable.View).SwapTo(source);    

            return (Tiles.Model)destinationTile.Model;
        }

        public Array<Tiles.Model> GetSurroundingTileModels(Vector2I center){ //should be a grid utility
            var neighbors = new Array<Tiles.Model>();
            var surroundings = new Array<Vector2I>{
                center + Vector2I.Up, 
                center + Vector2I.Right, 
                center + Vector2I.Down, 
                center + Vector2I.Left
            };

            foreach(var pos in surroundings){
                if(MathUtilities.CheckNegativeVectorAxes(pos)){
                    neighbors.Add(observers[pos.X][pos.Y].Model as Tiles.Model);
                }                
            }
            return neighbors;
        }   



        // private void Swap2(Vector2I source, Vector2I direction){
        //     Vector2I destination = source + direction;
        //     if(destination.X >= 0 && destination.Y >= 0){
        //         var newGrid = observers.Duplicate(true);
        //         var sourceTile = observers[source.X][source.Y]; //these shouldn't be here...
        //         var destinationTile = observers[destination.X][destination.Y];

        //         GridUtilities.PlaceTileOnBoard(destinationTile, newGrid, source.X, source.Y);
        //         GridUtilities.PlaceTileOnBoard(sourceTile, newGrid, destination.X, destination.Y);
        //         // newGrid[source.X][source.Y] = destinationTile;
        //         // newGrid[destination.X][destination.Y] = sourceTile;

        //         (var sourceMatches, var destinationMatches) = FindMatchGroups(sourceTile, destinationTile, newGrid);

        //          TileNode sourceNode = observers[source.X][source.Y]; //MAKE SURE THESE CHANGE WITH THE MODEL
        //          TileNode destinationNode = observers[destination.X][destination.Y];
        //         if((sourceMatches.Count > 0) || (destinationMatches.Count > 0)){ //not enough but w/e    
        //             (sourceNode.SwapAnimator as Swapable.View).SwapTo(destination);
        //             (destinationNode.SwapAnimator as Swapable.View).SwapTo(source);  //the model should probably not access the view's implementation, but I suppose swapto() is akin to update() a bit...     
        //                                                             //maybe I should use signals or something ...    
        //             observers = newGrid;        





        //             var (sourceMatchColumn, sourceMatchRow) = FindLines(sourceMatches);
        //             var (destinationMatchColumn, destinationMatchRow) = FindLines(destinationMatches);

        //             var collapsePath = MakeCollapsePath(
        //                 sourceMatchColumn, 
        //                 FindTilesByName(TileNames.Player)[0]
        //             );
        //             GD.Print("Path:  \n" + collapsePath);
        //             DestroyMatches(collapsePath);                                                                                                   
        //         } else {
        //             if(sourceTile.Type == TileNames.Player.ToString().ToLower()){
        //                 var player = sourceTile;
        //                 var target = destinationTile;
        //                 if(destinationTile is BuffableDamage.Model){
        //                     ((BuffableDamage.Model) player.Model).IncreaseDamageOfMelee(((BuffableDamage.Model) target).MeleeBuff);
        //                     ((BuffableDamage.Model) player.Model).IncreaseDamageOfRanged(((BuffableDamage.Model) target).RangedBuff);
        //                     ((BuffableDamage.Model) player.Model).IncreaseDamageOfSpell(((BuffableDamage.Model) target).SpellBuff);
        //                 }    
        //                 // sourceNode.SwapAnimator.SwapTo(destination);  //not swapping, replacing
        //                 // destinationNode.SwapAnimator.SwapTo(source); 
        //                 // observers = newGrid;                                                    
        //             }
        //         }
             
        //     }

        // }


        private (Array<Vector2I>, Array<Vector2I>) FindMatchGroups(TileNode sourceTile, TileNode destinationTile, Array<Array<TileNode>> newGrid){            
            Array<Vector2I> sourceMatches = [];
            Array<Vector2I> destinationMatches = [];

            var threeTileSourceMatches = FindMatchesWith(sourceTile, newGrid);
            var threeTileDestinationMatches = FindMatchesWith(destinationTile, newGrid);
            if(threeTileSourceMatches.Count > 0){ //this does not allow L combinations where a 90deg L is matched at the origin, where the actor sits at the shorter side and can short match
                sourceMatches = threeTileSourceMatches;
            } else {
                // var source = (sourceTile.Model as Tiles.Model).Position;   //POORLY IMPLEMENTED, GOTTA CHANGE, doesn't work when player tile swaps, it will match any 2 tiles of the same type on the board
                // var destination = (destinationTile.Model as Tiles.Model).Position;
                // if(CheckIfSwappingActor(source, destination)){
                //     var twoTileSourceMatches = GridUtilities.FindAllMatchingAdjacentTiles(sourceTile, newGrid); 
                //     if(twoTileSourceMatches.Count > 0){ 
                //         sourceMatches = twoTileSourceMatches;    
                //     }                          
                // }
            
            }

            if(threeTileDestinationMatches.Count > 0){
                destinationMatches = threeTileDestinationMatches;
            } else {
                // var twoTileDestinationMatches = GridUtilities.FindAllMatchingAdjacentTiles(destinationTile, newGrid);
                // if(twoTileDestinationMatches.Count > 0){ 
                //     destinationMatches = twoTileDestinationMatches;    
                // }                
            }
            var sourceMatchesWithoutDupes = Collections.RemoveDuplicates(sourceMatches);
            var destinationMatchesWithoutDupes = Collections.RemoveDuplicates(destinationMatches);

            return (sourceMatchesWithoutDupes, destinationMatchesWithoutDupes);
        }


        private bool CheckIfSwappingActor(Vector2I source, Vector2I destination){
            Tiles.Model sourceTile = observers[source.X][source.Y].Model as Tiles.Model;
            Tiles.Model destinationTile = observers[destination.X][destination.Y].Model as Tiles.Model;
            string player = TileNames.Player.ToString().ToLower();

            return (sourceTile.Name == player || destinationTile.Name == player);
        }

        private Array<Vector2I> FindTilesByName(TileNames tile){
            string tileName = tile.ToString().ToLower();
            Array<Vector2I> tilePositions = [];
            for(int x = 0; x < observers.Count; x++){
                for(int y = 0; y < observers[0].Count; y++){
                    if((observers[x][y] is not null) && ((observers[x][y].Model as Tiles.Model).Name == tileName)){
                        tilePositions.Add(new Vector2I(x, y));
                    }
                }
            }
            return tilePositions;
        }


        private Array<Vector2I> FindMatchesWith(TileNode tile_, Array<Array<TileNode>> grid_){
            var horizontalMatches = FindHorizontal(tile_, grid_);
            var verticalMatches = FindVertical(tile_, grid_);
            var mergedCsharpArray = horizontalMatches.Concat(verticalMatches).ToArray();            
            return new Array<Vector2I>(mergedCsharpArray);
        }

        private Array<Vector2I> FindHorizontal(TileNode tile_, Array<Array<TileNode>> grid_){
            var name = (tile_.Model as Tiles.Model).Name;  //CAREFUL, IF NO CAST IT ACCESSES Name PROP of Node, NOT Tiles.Model
            var matches = new Array<Vector2I>();

            for(int x = 0; x < rows; x++){
                for(int y = 0; y < columns; y++){
                    if(y > 0 && y < (columns - 1)){
                        if(
                            name == (grid_[x][y - 1].Model as Tiles.Model).Name && 
                            name == (grid_[x][y].Model as Tiles.Model).Name && 
                            name == (grid_[x][y + 1].Model as Tiles.Model).Name 					
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

        private Array<Vector2I> FindVertical(TileNode tile_, Array<Array<TileNode>> grid_){        
            var name = tile_.Model.Name;
            var matches = new Array<Vector2I>();

            for(int x = 0; x < columns; x++){
                for(int y = 0; y < rows; y++){
                    if(y > 0 && y < (rows - 1)){
                        if(
                            name == (grid_[y - 1][x].Model as Tiles.Model).Name && 
                            name == (grid_[y][x].Model as Tiles.Model).Name && 
                            name == (grid_[y + 1][x].Model as Tiles.Model).Name 					
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

        // private async void DestroyMatches(Array<Vector2I> allMatches){
        //     var playerPosition = FindTilesByName(TileNames.Player)[0]; //this function should be a utility
        //     var playerIsAdjacent = CheckIfTileIsNextToPath(allMatches, playerPosition);            
        //     for(int i = 0; i < allMatches.Count; i++){
        //         var pos = allMatches[i];
        //         var tile = observers[pos.X][pos.Y].Model as Tiles.Model;

        //         if(playerIsAdjacent){ 
        //             /* await */ PerformTileBehaviors(tile, pos);
        //             await PerformPlayerBehaviors(pos);
        //         }
                
        //     }
        // }

        // private /* async Task */ void PerformTileBehaviors(Tiles.Model tile, Vector2I positionInPath){
        //     var playerPosition = FindTilesByName(TileNames.Player)[0];                
        //     var player = observers[playerPosition.X][playerPosition.Y];
        //     var pos = positionInPath;
        //     if(tile is BuffableDamage.Model){ //probably shouldn't buff if active actor not adjacent
        //         ((BuffableDamage.Model) player.Model).IncreaseDamageOfMelee(((BuffableDamage.Model) tile).MeleeBuff);
        //         ((BuffableDamage.Model) player.Model).IncreaseDamageOfRanged(((BuffableDamage.Model) tile).RangedBuff);
        //         ((BuffableDamage.Model) player.Model).IncreaseDamageOfSpell(((BuffableDamage.Model) tile).SpellBuff);
        //     }   

        //     // //GridUtilities.PlaceTileOnBoard(player, observers, pos.X, pos.Y);
        //     // SetTile(player, pos.X, pos.Y);
        //     // //observers[pos.X][pos.Y] = player; 
            
        //     // ((Transportable.Model) player.Model).NotifyTransport(new Vector2I(pos.X, pos.Y));

        //     // var emitter = ((player as Animatable).Animators as Player.Animators).TransportAnimator as Node;
        //     // await ToSignal(emitter, "Transported");
        // }

        // private async Task PerformPlayerBehaviors(Vector2I positionInPath){
        //     var playerPosition = FindTilesByName(TileNames.Player)[0];                
        //     var player = observers[playerPosition.X][playerPosition.Y];
        //     var pos = positionInPath;

        //     SetTile(player, pos.X, pos.Y);

        //     var fighters = GridUtilities.FindTileInVincinity(TileNames.Fighter, pos, observers);
        //     var archers = GridUtilities.FindTileInVincinity(TileNames.Archer, pos, observers);
        //     if(fighters.Count>0 || archers.Count>0){
        //         //This is a bad idea
        //         //Whenever I need to scale this up I have to come back to this thing and change and tinker with it
        //         //Each tile should have a component that runs the entirety of it's behaviors when iterated 
        //         //That component should take in callbacks from it's parent board model and run it without being coupled to the board's implementations
        //         //could resemble a strategy pattern, I dunno
        //         var enemies = fighters + archers;
        //         for(int i=0; i < enemies.Count; i++){
        //             //(enemies[i].Model as Defensive.Model).TakeDamage(player.Model as )
        //             (player.Model as Offensive.Model).Attack((Tiles.Model) enemies[i].Model); 
        //             var bp3 = 1323;
        //         }
        //     }

        //     ((Transportable.Model) player.Model).NotifyTransport(new Vector2I(pos.X, pos.Y));
        //     var emitter = ((player as Animatable).Animators as Player.Animators).TransportAnimator as Node;
        //     await ToSignal(emitter, "Transported");            
        // }

        // private async void DestroyOneMatch(){
        //     grid[playerPosition.X][playerPosition.Y] = tileFactory.Create(    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //         TileNames.Stamina.ToString().ToLower(), 
        //         new Vector2I(69, 420)
        //     ); //I ALSO NEED TO CREATE THE VIEW NODE, BEST WAY IS TO REDRAW THE BOARD
        // }



        // private void ReplaceCollapsingTileWithActor(Tiles.Model tile){ //hmm helper function inside a helper function I dunno about this..
        //     var transportables = tile.Transportables;
        //     if(transportables.Contains(TileNames.Player)){
                

        //     }
        // }

        private bool CheckIfTileIsNextToPath(/* Array<Tiles.Model> */ Array<Vector2I> tileLine, Vector2I tilePosition){
            for(int i = 0; i < tileLine.Count; i++){
                if(
                    tileLine[i] + Vector2I.Up == tilePosition ||
                    tileLine[i] + Vector2I.Right == tilePosition ||
                    tileLine[i] + Vector2I.Down == tilePosition ||
                    tileLine[i] + Vector2I.Left == tilePosition 
                ){ 
                    return true;
                }               
            }
            return false;
        }  
    }

    // private TileNode CreateRandomTile(){

    // }

}

