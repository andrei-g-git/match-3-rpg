using Godot;
using Godot.Collections;
using Tiles;

public partial class Swapping : Node, Swapable.Model, GridItem{
    // [Export]
    // private Node signalEmitter; //this is the node controller, should not be in a model but I'm just using it as a signal source reference so meh...
    //[Export]
    private Node model;
    private Grid.Model board;
    public Grid.Model Board { get => board; set => board = value; }


    [Signal] 
    public delegate void TryMovingEventHandler(Vector2I source, Vector2I direction);

    public override void _Ready(){
        var signalEmitter = GetNode<Node>("%Controller");
        model = GetNode<Node>("%Model");
        (signalEmitter as Tiles.Controller).TriedSwapping += TrySwapping;
    }
    
    public void TrySwapping(Vector2I direction){
        var source = (model as Tiles.Model).Position;
        var collapsePath = board.TryMatching(source, direction);

        if(collapsePath.Count > 0){

        }else{
            EmitSignal(SignalName.TryMoving, source, direction);
        }
    }

    //^doesn't work well, can't get right model, trying this instead:
    // public void NotifySwap(Vector2I direction){
    //     var source = (model as Tiles.Model).Position;
    //     var collapsePath = board.TryMatching(source, direction);

    //     if(collapsePath.Count > 0){

    //     }else{
    //         //EmitSignal(SignalName.TryMoving, source, direction);
    //     }        
    // }
}