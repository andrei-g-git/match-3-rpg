using System;
using Constants;
using Godot;

namespace Tiles {
    namespace Archer{
        public partial class Model : Tiles.Model, Defensive.Model{
            [Export]
            private Node defender;
            public override string Name => "archer";
            public Defensive.Model Defender => defender as Defensive.Model;
            public int Health => Defender.Health;
            public int Defense => Defender.Defense;
            public int MaxHealth => Defender.MaxHealth;

            private Vector2I position;

            public void TakeDamage(int damage){
                Defender.TakeDamage(damage);
            }

            public void ConnectTookDamage(Action<Vector2I, int> action){
                (defender as Defensive.Model).ConnectTookDamage(action);
            }

            public void ReceiveHealing(int amount){
                Defender.ReceiveHealing(amount);
            }

            public void ReceiveHealingFrom(Node tile){
                Defender.ReceiveHealingFrom(tile);
            }
        }        
    }

}

