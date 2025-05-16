using Godot;
using System;
using Tiles;
public partial class Swapping : Node, Swapable.Model{
	[Signal] public delegate void TriedSwappingEventHandler(Vector2I source, Vector2I direction);
	private Tile_old tile = null;
	private bool isSwapable;
	public bool IsSwapable {get => isSwapable; set => isSwapable = value;}
	private bool canSwap = false;
    public bool CanSwap {get => canSwap; set => canSwap = value;}

    public Swapping(Tile_old tile_){
		tile = tile_;
	}

    public void NotifySwap(Vector2I direction){
        EmitSignal(SignalName.TriedSwapping, tile.Position, direction);
    }

    public void ConnectWithSwapSignal(TriedSwappingEventHandler callback){
        TriedSwapping += callback;
    }


}
