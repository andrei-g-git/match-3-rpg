using System;
using Constants;
using Godot;

namespace Tiles {
    namespace Fighter{
        public partial class Model : Tiles.Model, Defensive.Model, Hostility.Model  {
            [Export]
            private Node defender;
            [Export]
            private Node damageNumber;
            private Vector2I position;
            public override string Name => "fighter";
            //public override NamableTile Type => TileName.Fighter;
            public int Health => Defender.Health;
            public int Defense =>  Defender.Defense;
            public Defensive.Model Defender => defender as Defensive.Model;
            private Hostility.Model disposition;
            public bool IsAggressive { get => disposition.IsAggressive; set => disposition.IsAggressive = value; }
            public bool IsEnemy { get => disposition.IsEnemy; set => disposition.IsEnemy = value; }

            public int MaxHealth => Defender.MaxHealth;


            public override void _Ready(){
                base._Ready();
                disposition = GetNode<Node>("%Disposition") as Hostility.Model;
            }
            
            public void TakeDamage(int damage){
                Defender.TakeDamage(damage);
            }

            public void ConnectTookDamage(Action<Vector2I, int> action){
                (defender as Defensive.Model).ConnectTookDamage(action);
            }

            public void ReceiveHealing(int amount){
                Defender.ReceiveHealing(amount);
            }
        }        
    }

}


