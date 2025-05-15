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
        public Control Create(TileNames tileName, Tile model, Node parent) { //I don't like that I'm exposing this factory to the tile model when I already have a tile model factory
            var tileNode =  (Control) preInstantiatedTiles[tileName.ToString().ToLower()].Instantiate();
            ((/* Modelable */Controllable) tileNode).Model = model;//tileFactory.Create(tileName, position);
            parent.AddChild(tileNode);

            InitializeNode(tileName, tileNode, model);
            return tileNode;
        }

        private void InitializeNode(TileNames name, Node tileNode, Tile model){

            switch(name){
                case TileNames.Player: InitPlayer(tileNode, model); break;
                case TileNames.Melee: InitMelee(tileNode, model); break;
                case TileNames.Walk: InitWalk(tileNode, model); break;
                default: Foo(); break;
            }
        }

        private void Foo(){

        }
        private void InitArcher(Node tileNode){

        }
        private void InitChest(Node tileNode){

        }
        private void InitDefend(Node tileNode){

        }
        private void InitEnergy(Node tileNode){

        }
        private void InitFighter(Node tileNode){

        }
        private void InitHealth(Node tileNode){

        }
        private void InitMelee(Node tileNode, Tile model){
            ((Controllable) tileNode).SetController(new MeleeView(
                model,
                (BaseButton) tileNode,
                0,
                0,
                0
            ));
        }
        private void InitPlayer(Node tileNode, Tile model){
            ((Controllable) tileNode).SetController(new PlayerView(
                model,
                (BaseButton) tileNode,
                0,
                0,
                0
            ));
                                    //player is an implementation, gotta cast to interface...
            var transportBehavior = ((Player) model).TransportBehavior;    
            //var transportAnimator = ((PlayerView)((Controllable) tileNode).Controller).TransportAnimator;   
            var transportAnimator = new TransportAnimator(
                tileNode, 
                ((ViewAndController) ((Controllable) tileNode).Controller).SideLength,
                ((ViewAndController) ((Controllable) tileNode).Controller).Margin
            );                                    
            ((Listenable) transportAnimator).SignalEmitter = (Node) transportBehavior;
            ((Listenable) transportAnimator).Signal = ((Signalable) transportBehavior).Signal;
            ((Listenable) transportAnimator).Connect(); //might not need this
        }
        private void InitRanged(Node tileNode){

        }
        private void InitStamina(Node tileNode){

        }
        private void InitUnlock(Node tileNode){

        }
        private void InitWalk(Node tileNode, Tile model){
            ((Controllable) tileNode).SetController(new WalkView(
                model,
                (BaseButton) tileNode,
                0,
                0,
                0
            ));
        } 
    }
}
