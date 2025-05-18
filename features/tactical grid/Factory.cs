using Abstractions;
using Constants;
using Godot;
using Godot.Collections;

namespace Grid {
    public partial class Factory: Node{
        private Dictionary<string, PackedScene> preInstantiatedTiles = [];
        private Array<string> tileNames = [];
        private Array<PackedScene> tileScenes;

        public Factory(Array<PackedScene> tileScenes_) {
            tileScenes = tileScenes_;
        }

        public void Initialize() { 
            var tileNodes = new Array<Control>();
            foreach(PackedScene scene in tileScenes){
                tileNodes.Add(scene.Instantiate() as Control);
            }
            foreach(Control node in tileNodes){
                tileNames.Add(((string) node.Name).ToLower());
            }
            for(int i = 0; i < tileNames.Count; i++) {
                preInstantiatedTiles.Add(tileNames[i], tileScenes[i]);
            }
        }

        public TileNode Create(TileNames tileName, Node parent, Vector2I position) { 
            var tileNode =  (TileNode) preInstantiatedTiles[tileName.ToString().ToLower()].Instantiate();
            parent.AddChild(tileNode);

            InitializeNode(tileName, tileNode, position);
            return tileNode;
        }

        private void InitializeNode(TileNames name, Node tileNode, Vector2I position){
            var _model = ((Controllable) tileNode).Model as Tiles.Model;
            _model.Position = position;
            
            switch(name){
                case TileNames.Player: InitPlayer(tileNode, _model); break;
                case TileNames.Melee: InitMelee(tileNode, _model); break;
                case TileNames.Walk: InitWalk(tileNode, _model); break;
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
        private void InitMelee(Node tileNode, Tiles.Model model){
            // ((Controllable) tileNode).SetController(new MeleeView(
            //     model,
            //     (BaseButton) tileNode,
            //     0,
            //     0,
            //     0
            // ));
        }
        private void InitPlayer(Node tileNode, Tiles.Model model){
            // ((Controllable) tileNode).SetController(new PlayerView(
            //     model,
            //     (BaseButton) tileNode,
            //     0,
            //     0,
            //     0
            // ));
            //                         //player is an implementation, gotta cast to interface...
            // var transportBehavior = ((Player) model).TransportBehavior;    
            // //var transportAnimator = ((PlayerView)((Controllable) tileNode).Controller).TransportAnimator;   
            // var transportAnimator = new TransportAnimator(
            //     tileNode, 
            //     ((ViewAndController) ((Controllable) tileNode).Controller).SideLength,
            //     ((ViewAndController) ((Controllable) tileNode).Controller).Margin
            // );    
            // ((Parentable) transportAnimator).ParentNode = tileNode;                                
            // ((Listenable) transportAnimator).SignalEmitter = (Node) transportBehavior;
            // ((Listenable) transportAnimator).Signal = ((Signalable) transportBehavior).Signal;
            // ((Listenable) transportAnimator).Connect(); //might not need this
        }
        private void InitRanged(Node tileNode){

        }
        private void InitStamina(Node tileNode){

        }
        private void InitUnlock(Node tileNode){

        }
        private void InitWalk(Node tileNode, Tiles.Model model){
            // ((Controllable) tileNode).SetController(new WalkView(
            //     model,
            //     (BaseButton) tileNode,
            //     0,
            //     0,
            //     0
            // ));
        } 
    }
}
