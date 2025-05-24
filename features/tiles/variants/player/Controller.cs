using Godot;

namespace Player{
    public partial class Controller: TileNode{

        public override void _Ready(){
            base._Ready(); 

            var transportAnimator = GetNode<Node>("%Transport") as Box;
            transportAnimator.Width = SideLength;
            transportAnimator.Height = SideLength;
            transportAnimator.Margin = Margin;

            var attackAnimator = GetNode<Node>("%Attack") as Box;
            attackAnimator.Width = SideLength;
            attackAnimator.Height = SideLength;
            attackAnimator.Margin = Margin;

            var offender = GetNode<Node>("%Offender") as Offensive.Model;
            var hauler = GetNode<Node>("%Hauler") as Haulable.Model;
            hauler.ConnectTryFighting(offender.Attack);
        }
    }
}