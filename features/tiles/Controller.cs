using Godot;
using System;

namespace Tiles{
	public partial class Controller : Node, ControllableTile{
		//[Export]
		public Node dragController;
		public Draggable.Controller DragController { get => dragController as Draggable.Controller; /* set => dragController = (Node) value; */ }

		public override void _Ready(){
			dragController = GetNode<Node>("%DragController");
		}
	}	
}

