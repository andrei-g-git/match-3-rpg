using System;
using Godot;

namespace Defend{
    public partial class Manager: TileNode{
        //[Export]
        private Node controller;
        public /* override */ Node Controller => controller; //< ---------------- Overriding causes a bajillion errors -- should prob remove Controller as a base tile child and just add the controllar versions to each inheriting tile

        // public override bool _CanDropData(Vector2 atPosition, Variant data){
        //     return base._CanDropData(atPosition, data);
        // }

        // public override void _DropData(Vector2 atPosition, Variant data){
        //     base._DropData(atPosition, data);
        //     GD.Print("DROPPED DATA:   " + (string) data); //should check if data is string first
        // }      

        public override void _Ready(){
            base._Ready();


            //Controller = GetNode<Node>("%Controller");

            // var replaceController = GetNode<Node>("%ReplaceController") as Replaceable.Controller;
            // Connect(SignalName.ButtonUp, Callable.From((Action<Vector2, Variant>) replaceController._DropData));


            // //test
            // Connect(/* BaseButton. */SignalName.ButtonUp, Callable.From(Bar));
        }

        // public void Bar(){
        //     GD.Print("BUTTON UP, from defend manager");
        // }

        // public override bool _CanDropData(Vector2 atPosition, Variant data)
        // {
        //     base._CanDropData(atPosition, data);
        //     return true;
        // }

        // public override void _DropData(Vector2 atPosition, Variant data)
        // {
        //     base._DropData(atPosition, data);
        //     GD.Print("DATA FROM ROOT DEFENSE MANAGER:   " + (string) data);
        // }
    }
}