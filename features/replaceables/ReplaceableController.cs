using Godot;
using System;

public partial class ReplaceableController : TextureRect{
	[Export]
	private string type;

    public override Variant _GetDragData(Vector2 atPosition){
        //return base._GetDragData(atPosition);

		var previewTexture = new TextureRect();
		previewTexture.Texture = Texture;
		previewTexture.Size = Size/2;
		// var preview = new Control();
		// preview.AddChild()
		SetDragPreview(previewTexture);
		return type;
    }

}
