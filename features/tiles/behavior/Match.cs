using System;
using Godot;
using Godot.Collections;

public partial class Match : Node, Matchable.Model{
    //[Export]
    //private Node swapper;
    private Grid.Model board;
    public Grid.Model Board { set => board = value; }
    private Swapping swapper;
    //public Node Swappeder { set => throw new NotImplementedException(); }
    [Signal] 
    public delegate void StartedCollapseEventHandler();

    public override void _Ready(){
        swapper = GetNode<Node>("%Swapper") as Swapping;
        swapper.GotMatches += OnGotMatches;
    }

    public void OnGotMatches(Array<Vector2I> matches){
        board.ConnectAllMatchesWithSwappedTile(this, matches);
        var isPlayerAdjacent = board.CheckIfActorNearPath(GetParent() as TileNode, matches);
        if(isPlayerAdjacent){
            EmitSignal(SignalName.StartedCollapse);            
        }

    }
}