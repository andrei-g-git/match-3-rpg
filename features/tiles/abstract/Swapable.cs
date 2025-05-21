using Godot;

namespace Swapable
{
    public interface Model{
        public void TrySwapping(Vector2I direction);
    }

    public interface View{
        public void SwapTo(Vector2I target);
    }  
}