using Godot;

namespace Tiles {
    public partial class Walk : Tile_old {
        private Vector2I position;
        public override string Name => "walk";
        public Walk(Vector2I position) : base(position) {

        }

    }
}


