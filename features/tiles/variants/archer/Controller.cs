using Godot;

namespace Archer{
    public partial class Controller: TileNode{

        public override void _Ready(){
            base._Ready();

            //this whole thing should be a component, it's a repeat from the Fighter controller --- and this isn't even a controller since it doesn't handle input events...
            var model = GetNode<Node>("%Model");
            var defenseAnimator  = GetNode<Node>("%Defend");
            (model as Defensive.Model).ConnectTookDamage((defenseAnimator as Defensive.View).AnimateDefending);

            var damageNumber = GetNode<Node>("%DamageNumber");
            (model as Defensive.Model).ConnectTookDamage((damageNumber as DisplayableNumber).DisplayNumberAt);

            (defenseAnimator as Box).Width = SideLength;
            (defenseAnimator as Box).Height = SideLength;
            (defenseAnimator as Box).Margin = Margin;
        }
    } 
}