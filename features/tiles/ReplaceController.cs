using Godot;
using Replaceable;
using System;

public partial class ReplaceController : Control, Replaceable.Controller{
    //public Replaceable.Controller ReplaceController { get => this; }


    // public override void _Ready()
    // {
    //     //ffs...
	// 	var tileNode = GetParent().GetParent();
	// 	tileNode.Connect(BaseButton.SignalName.ButtonUp, Callable.From((Action<Vector2, Variant>) _DropData));
    // }

    public override bool _CanDropData(Vector2 atPosition, Variant data){
        return ((string )data).Length > 0;
    }

    public override void _DropData(Vector2 atPosition, Variant data){
        GD.Print("REPLACING TILE WITH:   " + (string) data); //check if it's an actual string...
    }
    public Controller GetReplaceController(){
        return this;
    }
    
    // public void ConnectCanDropData(Action<Vector2, Variant> action){
    //     Connect(Signal)
    // }

    // public void ConnectDropData(Action<Vector2, Variant> action){
    //     throw new NotImplementedException();
    // }
}
