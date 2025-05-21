using Godot;

public partial class Obtain : Node, Obtainable.Model
{
    [Export]
    private bool isObtainable;
    public bool IsObtainable => isObtainable;
}