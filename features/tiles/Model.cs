using Constants;
using Godot;
using Godot.Collections;
using System;
using Tiles;

namespace Tiles{
    public partial class Model : Node, Swapable.Model, Collapsable{
        public bool IsSwapable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CanSwap { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Array<TileNames> Transportables => throw new NotImplementedException();

        public void AddTransportable(TileNames tile)
        {
            throw new NotImplementedException();
        }


        public void ConnectWithSwapSignal(Swapping.TriedSwappingEventHandler callback)
        {
            throw new NotImplementedException();
        }

        public void NotifySwap(Vector2I direction)
        {
            throw new NotImplementedException();
        }
    }

}

