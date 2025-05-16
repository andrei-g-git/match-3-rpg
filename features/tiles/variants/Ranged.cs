using Godot;

namespace Tiles {
    public partial class Ranged : Tiles.Model {
        private Vector2I position;
        public override string Name => "ranged";
        public Ranged(Vector2I position) : base(position) {

        }

    }
}



