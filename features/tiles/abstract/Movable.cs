using System;
using Godot;

namespace Movable{
    public interface Model{
        public int Stamina {get; set;}
        public void Move(Vector2I target/* source, Vector2I direction */);
        public void TakeAStep(Vector2I target);
        public void ConnectTookStep(Action<Vector2I> action);
        public void ConnectGotConsumable(Action<Tiles.Model> action);
        public void ConnectGotHealth(Action<Node> action);
    }
}