using Godot;
using System;

namespace Abstractions{
	public interface Controllable{
		public Node Controller{get; set;}
		public Node Model{get;  set;}
		public void SetController(Node controller);
	}
	public interface Modelable{
		
		public void Register(/* Control */ Node tileNode, int x, int y);
		public void Notify();
	}	

	public interface Viewable{
		public void Update(Vector2I destination);
		//public Node SignalEmitter {set;}
	}

	public interface Animatable{
		public Node Animators {get; set;}
	}
}

