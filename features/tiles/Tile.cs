using Godot;


namespace Tiles {
    public abstract partial class Tile: Node, Swapable { //probably shouldn't be a Node but I have some functionality elswere that depends on it...
        private Vector2I position;
        public Vector2I Position{get => position;}
        private string name;
        public abstract string Name { get; }
        private Swapable swapBehavior = null;
        public Swapable SwapBehavior{get => swapBehavior; set => swapBehavior = value;}

        public bool IsSwapable {get => swapBehavior.IsSwapable; set => swapBehavior.IsSwapable = value;}

        public Tile(Vector2I position_) {
            position = position_;
        }

        public void NotifySwap(Vector2I direction){
            swapBehavior.NotifySwap(direction);
        }

        public void ConnectWithSwapSignal(Swapping.TriedSwappingEventHandler callback){
            swapBehavior.ConnectWithSwapSignal(callback);
        }
    }

}
