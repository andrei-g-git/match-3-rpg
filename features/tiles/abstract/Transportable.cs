using Godot;

namespace Transportable{
    public interface Model{
        public void NotifyTransport(Vector2I target);
        //public Node TransportAnimator {get; set;}
    }   

    public interface View{
        //public void JumpTo(Vector2I target);
    } 
}

