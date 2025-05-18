using Godot;
using System;
using Tiles;
public partial class Swapping : Node, Swapable.Model{
    [Export] 
    Node model; //for some reaosn this doesn't get the right node position...
    private Node hardcodedModel = null;
	[Signal] public delegate void TriedSwappingEventHandler(Vector2I source, Vector2I direction);
	private Tiles.Model tile = null;
	private bool isSwapable;
	public bool IsSwapable {get => isSwapable; set => isSwapable = value;}
	private bool canSwap = false;
    public bool CanSwap {get => canSwap; set => canSwap = value;}

    // public Swapping(Tiles.Model tile_){  //can't have constructor, adding directly to scene tree in dummy node
    // 	tile = tile_;
    // }

    public override void _Ready(){
        hardcodedModel = GetNode<Node>("%Model") as Tiles.Model;
    }

    public void NotifySwap(Vector2I direction){
        EmitSignal(SignalName.TriedSwapping, (hardcodedModel as Tiles.Model).Position, direction);
    }

    public void ConnectWithSwapSignal(TriedSwappingEventHandler callback){
        TriedSwapping += callback;
    }


}
