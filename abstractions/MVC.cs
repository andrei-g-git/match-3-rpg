using Godot;
using System;

namespace Abstractions{
	public interface Controllable{
		public Node Controller{get; /* set; */}
	}
	public interface Modelable{
		public Node Model{get;  set;}
	}	
}

