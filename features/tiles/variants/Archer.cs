using Godot;

namespace Tiles {
    public partial class Archer : Tile_old{
        private Vector2I position;
        public override string Name => "archer";
        public Archer(Vector2I position): base(position) {

        }

    }
}

