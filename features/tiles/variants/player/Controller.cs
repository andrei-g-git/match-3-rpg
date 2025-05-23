using Godot;

namespace Player{
    public partial class Controller: TileNode{
        //[Export]
        //private Node transportAnimator;
        public override void _Ready(){
            base._Ready(); //well this is problematic...
            //var rootNode = GetTree().CurrentScene; //don't need

            //var model = GetNode<Node>("%Model") as Tiles.Player.Model;
            //model.DamageBuffer = GetNode<Node>("%DamageBuffer") as BuffableDamage.Model; //should have generic interface for damagbuffer composition
            //model.Transporter = GetNode<Node>("%Transporter") as Transportable.Model;

            var transportAnimator = GetNode<Node>("%Transport") as Box;
            transportAnimator.Width = SideLength;
            transportAnimator.Height = SideLength;
            transportAnimator.Margin = Margin;

            var attackAnimator = GetNode<Node>("%Attack") as Box;
            attackAnimator.Width = SideLength;
            attackAnimator.Height = SideLength;
            attackAnimator.Margin = Margin;

            //Animators //must give a script so it stores sub animators by name
        }
    }
}