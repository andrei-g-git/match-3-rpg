using Godot;

namespace Tiles {
    public partial class Energy : Tile_old {
        private Vector2I position;
        public override string Name => "energy";
        public Energy(Vector2I position) : base(position) {

        }

    }
}


