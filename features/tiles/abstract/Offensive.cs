using System;
using Godot;

namespace Offensive{
    public interface Model{
        public int Damage { get; }
        public void Attack(Tiles.Model actor);
        public void ConnectAttacked(Action<Vector2I, Vector2I> action);
    }    
}
