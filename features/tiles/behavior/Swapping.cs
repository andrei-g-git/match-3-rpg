using Godot;
using System;
using Tiles;
public partial class Swapping : Node, Swapable{
	[Signal] public delegate void TriedSwappingEventHandler(Vector2I source, Vector2I direction);
	private Tile tile = null;
	private bool isSwapable;
	public bool IsSwapable {get => isSwapable; set => isSwapable = value;}
	public Swapping(Tile tile_){
		tile = tile_;
	}

    public void NotifySwap(Vector2I direction){
        EmitSignal(SignalName.TriedSwapping, tile.Position, direction);
    }

    public void ConnectWithSwapSignal(TriedSwappingEventHandler callback){
        TriedSwapping += callback;
    }


}
