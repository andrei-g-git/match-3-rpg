using Godot;
using Tiles;

public partial class Walkway : Node, Walkable.Model{
    [Export]
    private int staminaCost;
    public int StaminaCost => staminaCost;

}