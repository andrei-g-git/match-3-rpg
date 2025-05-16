using Godot;
using System;
using Tiles;
public partial class Swapping : Node, Swapable.Model{
    //[Export] Node tile;
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
        tile = GetNode<Node>("%Model") as Tiles.Model;
    }

    public void NotifySwap(Vector2I direction){
        EmitSignal(SignalName.TriedSwapping, tile.Position, direction);
    }

    public void ConnectWithSwapSignal(TriedSwappingEventHandler callback){
        TriedSwapping += callback;
    }


}
