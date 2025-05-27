using Godot;
using System;

public partial class Zdelete : TextureButton
{
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        GD.Print("DROP DATA FROM Zdelete:  "   + (string) data);
    }

}
