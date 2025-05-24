using System;
using System.Threading.Tasks;
using Constants;
using Godot;
using Godot.Collections;

namespace Transfering{
    public interface Model{
        public Collections.FixedSizeArray<TileNames> Transferables {get; set;}

        public Task TransferTileTask();
        public void TransferTile();

        public void ConnectTransferingActor(Action<Vector2I> action);
    }
}