using Godot;

namespace Player{
    public partial class Animators: Node/* , Transportable.View */{

        private Transportable.View transportAnimator = null;
        public Transportable.View TransportAnimator {get => transportAnimator; set => transportAnimator = value;}

        public override void _Ready(){
            transportAnimator = GetNode<Node>("%Transport") as Transportable.View; //should be export variable
        }
        
        // public void JumpTo(Vector2I target){
        //     transportAnimator.JumpTo(target);
        // }
    }
}