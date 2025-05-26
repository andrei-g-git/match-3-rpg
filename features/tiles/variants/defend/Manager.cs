using System;
using Godot;

namespace Defend{
    public partial class Manager: TileNode{

        // public override bool _CanDropData(Vector2 atPosition, Variant data){
        //     return base._CanDropData(atPosition, data);
        // }

        // public override void _DropData(Vector2 atPosition, Variant data){
        //     base._DropData(atPosition, data);
        //     GD.Print("DROPPED DATA:   " + (string) data); //should check if data is string first
        // }      

        public override void _Ready(){
            base._Ready();

            var replaceController = GetNode<Node>("%ReplaceController") as Replaceable.Controller;
            Connect(SignalName.ButtonUp, Callable.From((Action<Vector2, Variant>) replaceController._DropData));
        }
    }
}