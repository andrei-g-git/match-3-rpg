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
            private Vector2I position;

            public void TakeDamage(int damage){
                Defender.TakeDamage(damage);
            }

            public void ConnectTookDamage(Action<Vector2I> action){
                (defender as Defensive.Model).ConnectTookDamage(action);
            }

        }        
    }

}

