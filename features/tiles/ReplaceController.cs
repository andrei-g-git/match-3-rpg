using Godot;
using Replaceable;
using System;

public partial class ReplaceController : Node, Replaceable.Controller{
    //public Replaceable.Controller ReplaceController { get => this; }

    public bool _CanDropData(Vector2 atPosition, Variant data)
    {
        throw new NotImplementedException();
    }

    public void _DropData(Vector2 atPosition, Variant data){
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
