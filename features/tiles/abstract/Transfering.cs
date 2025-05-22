using Constants;
using Godot.Collections;

namespace Transfering{
    public interface Model{
        public Collections.FixedSizeArray<TileNames> Transferables {get; set;}

        public void TransferTile();
    }
}