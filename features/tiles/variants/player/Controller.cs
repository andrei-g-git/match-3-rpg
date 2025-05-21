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
            (transportAnimator as Box).Width = SideLength;
            (transportAnimator as Box).Height = SideLength;
            (transportAnimator as Box).Margin = Margin;

            //Animators //must give a script so it stores sub animators by name
        }
    }
}