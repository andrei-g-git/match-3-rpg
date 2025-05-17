using Godot;

namespace Melee{
    public partial class Controller: TileNode{
        public override void _Ready(){
            base._Ready(); //well this is problematic...

            var model = GetNode<Node>("%Model") as Tiles.Model;
            (model as Tiles.Melee.Model).DamageBuffer = GetNode<Node>("%DamageBuffer") as BuffableDamage.Model;
        }
    }
}