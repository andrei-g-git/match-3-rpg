using Godot;

namespace Tiles {
    public partial class Stamina : Tiles.Model {
        private Vector2I position;
        public override string Name => "stamina";
        public Stamina(Vector2I position) : base(position) {

        }

    }
}


