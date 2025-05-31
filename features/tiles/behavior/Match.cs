using System;
using Godot;
using Godot.Collections;

public partial class Match : Node, Matchable.Model, Updating.Model{
    //[Export]
    //private Node swapper;
    // private Grid.Model board;
    // public Grid.Model Board { set => board = value; }
    private Swapping swapper;
    //public Node Swappeder { set => throw new NotImplementedException(); }
    [Signal] 
    public delegate void StartedCollapseEventHandler();
    [Signal]
    public delegate void UpdateEventHandler();

    public override void _Ready(){
        swapper = GetNode<Node>("%Swapper") as Swapping;
        swapper.GotMatches += OnGotMatches;
    }

    public void OnGotMatches(Array<Vector2I> matches){
        var board = (GetNode<Node>("%Model") as Tiles.Model).Board;
        board.ConnectAllMatchesWithSwappedTile(this, matches);  //doesn't work and is undesirable
        var tileNode = GetParent().GetParent(); //wonderful...
        var isPlayerAdjacent = board.CheckIfActorNearPath(tileNode as TileNode, matches);
        //if(isPlayerAdjacent){ //should be able to match whether or not player is adjacent
            //can't use this yet, emits before receivers can connect to it's sender...
            EmitSignal(SignalName.StartedCollapse);  //THIS IS FINE, ONLY EMMITS TO MATCHES THAT HAVE BEEN CONNECTED BUT I GOTTA DISCONNECT 
            EmitSignal(SignalName.Update);
            //provisory
            //board.NotifyMathedTileToPerformBehaviors(matches);                      
        //}


    }

    public void ConnectUpdate(Action action)
    {
        Connect(SignalName.Update, Callable.From(action));
    }
}