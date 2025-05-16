using Godot;

namespace Player{
    public partial class Controller: TileNode{

        public override void _Ready(){
            base._Ready(); //well this is problematic...
            var rootNode = GetTree().CurrentScene; //don't need
            var transportAnimator = GetNode<Node>("%Transport") as Box;
            transportAnimator.Width = SideLength;
            transportAnimator.Height = SideLength;
            transportAnimator.Margin = Margin;
        }
    }
}