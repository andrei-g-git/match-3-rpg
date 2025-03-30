using Godot;

namespace Tiles {
    public abstract partial class Tile: GodotObject {
        private Vector2I position;
        private string name;
        public abstract string Name { get; }
        public Tile(Vector2I position_) {
            position = position_;
        }
    }
}
