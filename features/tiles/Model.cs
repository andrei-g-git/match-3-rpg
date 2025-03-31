using Godot;
using System;

namespace Tiles{
	public partial class Model : Node, Swapable{ //swapable should be compositional interface, not implemented directly by base model...
		[Signal] public delegate void TriedSwappingEventHandler(Vector2I source, Vector2I direction);

		private int x = 0;
		private int y = 0;
		public Model(){

		}

		public void NotifySwap(Vector2I direction){
			//Console.WriteLine("direction:   ", direction.X, "   ", direction.Y);
			EmitSignal(SignalName.TriedSwapping, new Vector2I(x, y), direction);
		}

    }	
}

