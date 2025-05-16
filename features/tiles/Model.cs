using Constants;
using Godot;
using Godot.Collections;
using System;
using Tiles;

namespace Tiles{
    public /* abstract */ partial class Model : Node, Swapable.Model, Collapsable{        
        private Vector2I position;
        public Vector2I Position{get => position; set => position = value; }
        private string name;
        public /* abstract */ virtual string Name { get; } //hides Node.Name, but I don't think I'll be using that...
        private Swapable.Model swapBehavior = null;
        public Swapable.Model SwapBehavior{get => swapBehavior; set => swapBehavior = value;}

        public bool IsSwapable {get => swapBehavior.IsSwapable; set => swapBehavior.IsSwapable = value;}
        public bool CanSwap { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        private Collapsable collapseBehavior = null;
        public Collapsable CollapseBehavior{get => collapseBehavior; set => collapseBehavior = value;}

        //public Array<TileNames> Transportables => throw new NotImplementedException();

        // public Model(Vector2I position_) {
        //     position = position_;
        // }

        // public void AddTransportable(TileNames tile)
        // {
        //     throw new NotImplementedException();
        // }


        public void ConnectWithSwapSignal(Swapping.TriedSwappingEventHandler callback){
            swapBehavior.ConnectWithSwapSignal(callback);
        }

        public void NotifySwap(Vector2I direction){
            swapBehavior.NotifySwap(direction);
        }
    }

}

