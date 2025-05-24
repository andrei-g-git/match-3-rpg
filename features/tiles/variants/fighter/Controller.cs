using Godot;

namespace Fighter{
    public partial class Controller: TileNode{
        public override void _Ready(){
            base._Ready(); 

            var model = GetNode<Node>("%Model");
            var defenseAnimator  = GetNode<Node>("%Defend");

            (model as Defensive.Model).ConnectTookDamage((defenseAnimator as Defensive.View).AnimateMethod);
            
            (defenseAnimator as Box).Width = SideLength;
            (defenseAnimator as Box).Height = SideLength;
            (defenseAnimator as Box).Margin = Margin;
        }
    }
}