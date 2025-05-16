using Abstractions;
using Constants;
using Godot;
using Godot.Collections;


namespace Tiles {
    public abstract partial class Tile_old: Node, Modelable, Swapable.Model, Collapsable { //probably shouldn't be a Node but I have some functionality elswere that depends on it...
        private Vector2I position;
        public Vector2I Position{get => position;}
        private string name;
        public abstract string Name { get; }
        private Swapable.Model swapBehavior = null;
        public Swapable.Model SwapBehavior{get => swapBehavior; set => swapBehavior = value;}

        public bool IsSwapable {get => swapBehavior.IsSwapable; set => swapBehavior.IsSwapable = value;}
        public bool CanSwap { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        private Collapsable collapseBehavior = null;
        public Collapsable CollapseBehavior{get => collapseBehavior; set => collapseBehavior = value;}
        //public Array<TileNames> Transportables {get => collapseBehavior.Transportables;}
        private Control tileNode = null;


        public Tile_old(Vector2I position_) {
            position = position_;
        }

        public void NotifySwap(Vector2I direction){
            swapBehavior.NotifySwap(direction);
        }

        public void ConnectWithSwapSignal(Swapping.TriedSwappingEventHandler callback){
            swapBehavior.ConnectWithSwapSignal(callback);
        }

        // public void AddTransportable(TileNames tile){
        //     collapseBehavior.AddTransportable(tile);
        // }

        public void Register(Control tileNode){
            this.tileNode = tileNode;
        }
    }

}
