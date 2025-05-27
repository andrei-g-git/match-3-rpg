using Constants;
using Godot;
using System;

public partial class ReplaceableController : TextureButton{
	[Export]
	private TileNames type;

    public override Variant _GetDragData(Vector2 atPosition){
        //return base._GetDragData(atPosition);

		var previewTexture = new TextureRect();
		previewTexture.Texture = TextureNormal;
		previewTexture.Size = Size/2;
		// var preview = new Control();
		// preview.AddChild()
		SetDragPreview(previewTexture);
		GD.Print("drag data:   " + type);
		return (int) type;
    }

	// public void _on_pressed(){
	// 	GD.Print("pressssssssssing");
	// }

}
