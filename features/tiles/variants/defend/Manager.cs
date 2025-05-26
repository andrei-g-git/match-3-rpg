using Godot;

namespace Defend{
    public partial class Manager: TileNode{
        
        public override bool _CanDropData(Vector2 atPosition, Variant data){
            return base._CanDropData(atPosition, data);
        }

        public override void _DropData(Vector2 atPosition, Variant data){
            base._DropData(atPosition, data);
            GD.Print("DROPPED DATA:   " + (string) data); //should check if data is string first
        }        
    }
}