using Godot;
using System;

namespace Abstractions{
	public interface Controllable{
		//public Node Controller{get; /* set; */}
		public Node Model{get;  set;}
	}
	public interface Modelable{
		
		public void Register(Control tileNode);
	}	

	public interface Viewable{
		public void Update(Vector2I destination);
	}
}

