using System;
using Godot;
using Godot.Collections;

public partial class Match : Node, Matchable.Model{
    [Export]
    private Node swapper;
    private Grid.Model board;
    public Grid.Model Board { set => board = value; }
    [Signal]
    public delegate void TryMovingEventHandler(Vector2I source, Vector2I direction);
    public override void _Ready(){
        (swapper as Swapping_old).TriedSwapping += TryMatching;

    }

    public void TryMatching(Vector2I source, Vector2I direction){ // I don't think I need the source, the source the parent Node
        var collapsePath = board.TryMatching(source, direction);

        if(collapsePath.Count > 0){

        }else{
            EmitSignal(SignalName.TryMoving, source, direction);
        }
    }
}