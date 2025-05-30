using System;
using Godot;

public interface IGrid{ //they can't all be gold...
    public int Cols{get; set;}
    public int Rows{get; set;}
    public void ConnectRandomizedTile(Action<string, Vector2I> action);
}