using System;
using Godot;

public interface IGrid{ //they can't all be gold...
    public void ConnectRandomizedTile(Action<string, Vector2I> action);
}