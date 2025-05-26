using Godot;
using System;

namespace Tiles{
	public partial class Controller : Node, ControllableTile{
		[Export]
		public Node dragController;
		public Draggable.Controller DragController { get => dragController as Draggable.Controller; set => dragController = (Node) value; }
	}	
}

