using Godot.Collections;

public interface Movable_old{
    public Array<string> ShortMoveTiles{get;}
    public bool VerifyShortMoveEligibility(string tileName);
    public void AddTile(string tileName);
    public void AddTiles(string[] tiles);
    public void RemoveTile(string tileName);
}