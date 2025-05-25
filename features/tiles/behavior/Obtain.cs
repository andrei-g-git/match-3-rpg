using Godot;
using Tiles;

public partial class Obtain : Node, Obtainable.Model
{
    [Export]
    private bool isObtainable;
    public bool IsObtainable => isObtainable;

    public void ObtainFor(Model actor){
        throw new System.NotImplementedException();
    }
}