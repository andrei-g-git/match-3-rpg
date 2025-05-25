using Godot;

namespace Walk{
    public partial class Controller: TileNode{
        public override void _Ready(){
            base._Ready();

            var model = GetNode<Node>("%Model") as Tiles.Walk.Model;
            model.Walkway = GetNode<Node>("%Walkway") as Walkable.Model;
        }
    }
}