using Godot;

namespace Tiles {
    public partial class Defend : Tiles.Model {
        private Vector2I position;
        public override string Name => "defend";
        public Defend(Vector2I position) : base(position) {

        }

    }
}


