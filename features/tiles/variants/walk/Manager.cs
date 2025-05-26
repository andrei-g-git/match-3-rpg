using Godot;

namespace Walk{
    public partial class Manager: TileNode, Replaceable.Controller{
        public override void _Ready(){
            base._Ready();

            var model = GetNode<Node>("%Model") as Tiles.Walk.Model;
            model.Walkway = GetNode<Node>("%Walkway") as Walkable.Model;
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data){
            return base._CanDropData(atPosition, data);
        }

        public override void _DropData(Vector2 atPosition, Variant data)
        {
            base._DropData(atPosition, data);
        }
    }
}