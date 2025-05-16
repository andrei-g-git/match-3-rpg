using Godot;

namespace Swapable
{
    public interface Model{
        [Signal] public delegate void TriedSwappingEventHandler(Vector2I source, Vector2I direction); //doesn't pester me to implement this like it normally shoudld...
        public void NotifySwap(Vector2I direction);
        public bool IsSwapable { get; set; }
        public void ConnectWithSwapSignal(Swapping.TriedSwappingEventHandler callback);
        public bool CanSwap { get; set; }
    }

    public interface View{
        public void SwapTo(Vector2I target);
    }  
}
