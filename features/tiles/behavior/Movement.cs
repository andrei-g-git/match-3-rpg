using Godot;
using Godot.Collections;
using System;

public partial class Movement : Node, Movable{
	private Array<string> shortMoveTiles = [];
    public Array<string> ShortMoveTiles {get => shortMoveTiles;}

    public void AddTile(string tileName){
        shortMoveTiles.Add(tileName);
    }

    public void RemoveTile(string tileName){
		shortMoveTiles.Remove(tileName);
    }

    public bool VerifyShortMoveEligibility(string tileName){
        return shortMoveTiles.Contains(tileName);
    }

}
