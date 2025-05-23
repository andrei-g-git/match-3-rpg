using System.Threading.Tasks;
using Constants;
using Godot;
using Godot.Collections;

namespace Transfering{
    public interface Model{
        public Collections.FixedSizeArray<TileNames> Transferables {get; set;}

        public Task TransferTile();
    }
}