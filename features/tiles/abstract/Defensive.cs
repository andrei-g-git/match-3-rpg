using System;
using Godot;

namespace Defensive{
    public interface Model{
        public int Health{get;}
        public int Defense{get;}
        public void TakeDamage(int damage);
        public void ConnectTookDamage(Action<Vector2I> action);
    }  

    public interface View{
        public Action<Vector2I> AnimateMethod {get;}
    }  
}