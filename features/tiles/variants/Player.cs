using Godot;

namespace Tiles {
    public partial class Player : Tile {
        private Vector2I position;
        public override string Name => "player";
        public Player(Vector2I position) : base(position) {

        }

    }
}


