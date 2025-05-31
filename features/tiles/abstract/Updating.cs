using Godot;
using System;

namespace Updating{
	public interface Model{
		public void ConnectUpdate(Action action);
	}
}
