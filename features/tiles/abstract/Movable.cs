using Godot;

namespace Movable{
    public interface Model{
        public void Move(Vector2I source, Vector2I direction);
    }
}