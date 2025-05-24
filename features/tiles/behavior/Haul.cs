using System;
using Godot;
using Tiles;

public partial class Haul : Node, Haulable.Model{ //this occurs BEFORE player is set to the position of the tile that drags it so the least worst thing that's gonna happen is that one of his neighbours is going to be himself
    [Signal]
    public delegate void TryFightingEventHandler(Tiles.Model tile);
    public void AssessSurroundings(Vector2I position){
        var board = (GetNode<Node>("%Model") as Tiles.Model).Board;
        var neighbors = board.GetSurroundingTileModels(position);
        foreach(var tile in neighbors){

            if(tile is Hostility.Model && (tile as Hostility.Model).IsEnemy){
                EmitSignal(SignalName.TryFighting, tile); 
            }
        }
    }

    public void ConnectTryFighting(Action<Tiles.Model> tile){
        Connect(SignalName.TryFighting, Callable.From(tile));
    }
}