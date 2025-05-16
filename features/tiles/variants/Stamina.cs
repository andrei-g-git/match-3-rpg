using Godot;

namespace Tiles {
    public partial class Stamina : Tile_old {
        private Vector2I position;
        public override string Name => "stamina";
        public Stamina(Vector2I position) : base(position) {

        }

    }
}


