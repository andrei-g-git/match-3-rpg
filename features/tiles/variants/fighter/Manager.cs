using Abstractions;
using Godot;
using System;

namespace Fighter{
    public partial class Manager: TileNode{
        public override void _Ready(){
            base._Ready(); 

            var model = GetNode<Node>("%Model");
            var defenseAnimator  = GetNode<Node>("%Defend");
            //(model as Defensive.Model).ConnectTookDamage((defenseAnimator as Defensive.View).AnimateMethod);
            (model as Defensive.Model).ConnectTookDamage((defenseAnimator as Defensive.View).AnimateDefending);

            var attackAnimator = GetNode<Node>("%Attack") as Box;
            attackAnimator.Width = SideLength;
            attackAnimator.Height = SideLength;
            attackAnimator.Margin = Margin;

            var damageNumber = GetNode<Node>("%DamageNumber");
            (model as Defensive.Model).ConnectTookDamage((damageNumber as DisplayableNumber).DisplayNumberAt);

            (defenseAnimator as Box).Width = SideLength;
            (defenseAnimator as Box).Height = SideLength;
            (defenseAnimator as Box).Margin = Margin;

            var model2 = (GetParent().GetParent() as TacticalGrid).Model;
            var movement = GetNode<Node>("%Movement") as Updating.Model;
            movement.ConnectUpdate(model2.Notify);
        }
    }
}