using Godot;

namespace Tiles {
    public partial class Fighter : Tile_old {
        private Vector2I position;
        public override string Name => "fighter";
        public Fighter(Vector2I position) : base(position) {

        }

    }
}


