using Godot;

namespace Tiles {
    public partial class Chest : Tile_old {
        private Vector2I position;
        public override string Name => "chest";
        public Chest(Vector2I position) : base(position) {

        }

    }
}



