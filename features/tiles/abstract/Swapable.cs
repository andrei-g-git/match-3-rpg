using Godot;

namespace Swapable
{
    public interface Model{
        public void TrySwapping(Vector2I direction);
        //public void NotifySwap(Vector2I direction); //meh
        //public void ConnectWithSwapSignal(Swapping.TriedSwappingEventHandler callback);
    }

    public interface View{
        public void SwapTo(Vector2I target);
    }  
}