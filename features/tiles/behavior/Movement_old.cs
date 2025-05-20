using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Movement : Node, Movable_old{
	private Array<string> shortMoveTiles = [];
    public Array<string> ShortMoveTiles {get => shortMoveTiles;}

    public void AddTile(string tileName){
        shortMoveTiles.Add(tileName);
    }
    public void AddTiles(string[] tiles){
        shortMoveTiles.Concat(tiles).ToArray();
    }
    public void RemoveTile(string tileName){
		shortMoveTiles.Remove(tileName);
    }

    public bool VerifyShortMoveEligibility(string tileName){
        return shortMoveTiles.Contains(tileName);
    }

}
