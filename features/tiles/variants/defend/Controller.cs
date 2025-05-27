using Godot;
using System;

namespace Defend{
	public partial class Controller : Control, ControllableTile/* , Replaceable.Controller */{
		[Export]
		public Node dragController;
		[Export]
		public Control replaceController;
		public Draggable.Controller DragController { get => dragController as Draggable.Controller; /* set => dragController = (Node) value; */ }
        //public Replaceable.Controller ReplaceController { get => replaceController as Replaceable.Controller; }


        // public override bool _CanDropData(Vector2 atPosition, Variant data)
        // {
        //     return true;
        // }

        // public override void _DropData(Vector2 atPosition, Variant data){
        //     //GetReplaceController()._DropData(atPosition, data);
        //     GD.Print("DROPPED TO PARENT CONTRILLER:   " + (string) data);
        // }

		public Replaceable.Controller GetReplaceController(){
			return replaceController as Replaceable.Controller;
		}
    }	
}

