using Godot.Collections;

public interface Movable{
    public Array<string> ShortMoveTiles{get;}
    public bool VerifyShortMoveEligibility(string tileName);
    public void AddTile(string tileName);
    public void AddTiles(string[] tiles);
    public void RemoveTile(string tileName);
}