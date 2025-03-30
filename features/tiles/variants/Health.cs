using Godot;

namespace Tiles {
    public partial class Health : Tile {
        private Vector2I position;
        public override string Name => "health";
        public Health(Vector2I position) : base(position) {

        }

    }
}


