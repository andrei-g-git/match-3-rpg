using Abstractions;
using Constants;
using Godot;
using Godot.Collections;
using System;
//using System.Collections.Generic;
using System.Linq;
using Tiles;

namespace Grid {
    public partial class Factory: Node{
        private Dictionary<string, PackedScene> preInstantiatedTiles = new Dictionary<string, PackedScene>();
        private Array<string> tileNames = new Array<string>();//private List<string> tileNames;
        private Array<PackedScene> tileScenes;//private List<PackedScene> tileScenes;
        //private Tiles.Factory tileFactory = null;
        public Factory(Array<PackedScene> tileScenes_/* , Tiles.Factory tileFactory_ */) {
            tileScenes = tileScenes_;
            //tileFactory = tileFactory_;
            var bp = 123;
        }

        public void Initialize() { //this should be called from director type class
            //List<Control> tileNodes = (List<Control>) tileScenes.Select(scene => scene.Instantiate());
            //tileNames = (List<string>)tileNodes.Select(node => node.Name);

            //var result = tileScenes.Select(scene => scene.Instantiate() as Control);
            //var tileNodes = new Array<Control>(result);
            var tileNodes = new Array<Control>();
            foreach(PackedScene scene in tileScenes){
                tileNodes.Add(scene.Instantiate() as Control);
            }
            //tileNames = (Array<string>) tileNodes.Select(node => node.Name.ToString());
            foreach(Control node in tileNodes){
                tileNames.Add(((string) node.Name).ToLower());
            }
            for(int i = 0; i < tileNames.Count; i++) {
                preInstantiatedTiles.Add(tileNames[i], tileScenes[i]);
            }
            var bp = 123;
        }

        //I should create each tile node with it's specifics like I create the tile models
        //each tile piece should have it's own animation behavior that connects to it's model behavior through signals
        //for example in the behavior folder I could have an Animation script eg. TransportAnimation.cs3q or SwappingAnimation.cs
        public Control Create(string tileName, Tile model, ) { //I don't like that I'm exposing this factory to the tile model when I already have a tile model factory
            var tileNode =  (Control) preInstantiatedTiles[tileName].Instantiate();
            ((Viewable) tileNode).SignalEmitter = model; 
            ((/* Modelable */Controllable) tileNode).Model = model;//tileFactory.Create(tileName, position);
            return tileNode;
        }

        private void InitializeNode(TileNames name, Node tileNode){
            name switch{
                TileNames.Archer => InitArcher(tileNode),
            }
        }

        private void InitArcher(Node tileNode){

        }
        private void InitChest(Node tileNode){
            var tile = new Chest(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private void InitDefend(Node tileNode){
            var tile = new Defend(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private void InitEnergy(Node tileNode){
            var tile = new Energy(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private void InitFighter(Node tileNode){
            var tile = new Fighter(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private void InitHealth(Node tileNode){
            var tile = new Health(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private void InitMelee(Node tileNode){
            var tile = new Melee(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private void InitPlayer(Node tileNode, Tile model){
            // var tile = new Player(position);
            // tile.SwapBehavior = new Swapping(tile);
            // tile.MoveBehavior = new Movement();
            // tile.AddTiles([
            //     TileNames.Defend.ToString().ToLower(),
            //     TileNames.Melee.ToString().ToLower(),
            //     TileNames.Ranged.ToString().ToLower(),
            //     TileNames.Walk.ToString().ToLower(),
            // ]);
            // tile.TransportBehavior = new Transport();
            // return tile;
                                    //player is an implementation, gotta cast to interface...
            var transportBehavior = ((Player) model).TransportBehavior;                                                        
            ((Listenable) tileNode).SignalEmitter = (Node) transportBehavior;
            ((Listenable) tileNode).Signal = ((Signalable) transportBehavior).Signal;


        }
        private void InitRanged(Node tileNode){
            var tile = new Ranged(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private void InitStamina(Node tileNode){
            var tile = new Stamina(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private void InitUnlock(Node tileNode){
            var tile = new Unlock(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private void InitWalk(Node tileNode){
            var tile = new Walk(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            tile.AddTransportable(TileNames.Player);
            return tile;
        } 
    }
}
