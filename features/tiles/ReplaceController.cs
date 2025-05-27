using Constants;
using Godot;
using Godot.Collections;
using Replaceable;
using System;


public partial class ReplaceController : Control, Replaceable.Controller{
    [Signal]
    public delegate void ReplacingTileEventHandler(TileNames type);
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
        //int enumValue = (int)(TileNames)Enum.Parse(typeof(TileNames), (string))
        TileNames tileType = (TileNames)(int)data;
        GD.Print("REPLACING TILE WITH:   " + tileType.ToString()); //check if it's an actual string...
        EmitSignal(SignalName.ReplacingTile, (int) data);
    }
    public Controller GetReplaceController(){
        return this;
    }

    public void ConnectReplacingTile(Action<int> action){
        Connect(SignalName.ReplacingTile, Callable.From(action));
    }

    // public void ConnectCanDropData(Action<Vector2, Variant> action){
    //     Connect(Signal)
    // }

    // public void ConnectDropData(Action<Vector2, Variant> action){
    //     throw new NotImplementedException();
    // }

}
