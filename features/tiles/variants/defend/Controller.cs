using Godot;
using System;

namespace Defend{
	public partial class Controller : Node, ControllableTile, Replaceable.Controller{
		[Export]
		public Node dragController;
		[Export]
		public Node replaceController;
		public Draggable.Controller DragController { get => dragController as Draggable.Controller; /* set => dragController = (Node) value; */ }
        //public Replaceable.Controller ReplaceController { get => replaceController as Replaceable.Controller; }


        public bool _CanDropData(Vector2 atPosition, Variant data)
        {
            throw new NotImplementedException();
        }

        public void _DropData(Vector2 atPosition, Variant data){
            GetReplaceController()._DropData(atPosition, data);
        }

		public Replaceable.Controller GetReplaceController(){
			return replaceController as Replaceable.Controller;
		}
    }	
}

