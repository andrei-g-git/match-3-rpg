using Godot;

namespace Replaceable{
    public interface Model{

    }
    public interface Controller{
        public bool _CanDropData(Vector2 atPosition, Variant data);
        public void _DropData(Vector2 atPosition, Variant data);
    }
}