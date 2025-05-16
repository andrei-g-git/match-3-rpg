using Godot;

namespace Tiles {
    public partial class Unlock : Tile_old {
        private Vector2I position;
        public override string Name => "unlock";
        public Unlock(Vector2I position) : base(position) {

        }

    }
}



