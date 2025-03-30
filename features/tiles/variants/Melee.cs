using Godot;

namespace Tiles {
    public partial class Melee : Tile {
        private Vector2I position;
        public override string Name => "melee";
        public Melee(Vector2I position) : base(position) {

        }

    }
}


