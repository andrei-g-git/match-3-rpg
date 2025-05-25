using System;
using Godot;

namespace Defensive{
    public interface Model{
        public int Health{get;}
        public int MaxHealth{get;}
        public int Defense{get;}
        public void TakeDamage(int damage);
        public void ReceiveHealing(int amount);
        public void ReceiveHealingFrom(/* Healable.Model */Node tile);
        public void ConnectTookDamage(Action<Vector2I, int> action);
    }  

    public interface View{
        //public Action<Vector2I, int> AnimateMethod {get;}
        public void AnimateDefending(Vector2I attackDirection, int damage);
    }  
}