using System;
using Constants;
using Godot;
using Godot.Collections;

namespace Tiles { 
    namespace Player{
        public partial class Model : Tiles.Model, Movable.Model, 
            /* Transportable.Model, */ BuffableDamage.Model, 
            Offensive.Model, 
            Defensive.Model, 
            Haulable.Model,
            Hostility.Model
        { //should not have the buffable damage interface, only damage buff tiles can .. well, buff damage
            [Export]
            private Node transporter;
            [Export]
            private Node damageBuffer;
            [Export]
            private Node offender; //reeee
            [Export]
            private Node defender;
            [Export]
            private Node hauler;
            [Export]
            private Node movement;
            [Export]
            private Node disposition;
            [Export]
            private Node damageNumber;
            private Vector2I position;
            public override string Name => "player";
            //public override NamableTile Type => TileName.Player;
            // private Movable moveBehavior = null;
            // public Movable MoveBehavior {get => moveBehavior; set => moveBehavior = value;}
            //private Transportable.Model transporter = null;
            public Transportable.Model Transporter{get => (Transportable.Model) transporter; set => transporter = (Node)value; }
            private Array<string> shortMoveTiles = [];
            public Array<string> ShortMoveTiles {get => shortMoveTiles;}
            //private BuffableDamage.Model damageBuffer = null;
            public BuffableDamage.Model DamageBuffer {get => (BuffableDamage.Model)damageBuffer; set =>  damageBuffer = (Node) value; }
            public int MeleeBuff { get => (damageBuffer as BuffableDamage.Model).MeleeBuff; }     
            public int RangedBuff { get => (damageBuffer as BuffableDamage.Model).RangedBuff; }    
            public int SpellBuff { get => (damageBuffer as BuffableDamage.Model).SpellBuff; }
            public Offensive.Model Offender {get => (Offensive.Model) offender; set => offender = (Node) value;}
            public int Damage { get => (offender as Offensive.Model).Damage; }
            public Defensive.Model Defender {get => (defender as Defensive.Model); set => defender = (Node) value;}
            public Haulable.Model Hauler => hauler as Haulable.Model;
            public Movable.Model Movement => movement as Movable.Model;
            public int Stamina { get => (movement as Movable.Model).Stamina; set => (movement as Movable.Model).Stamina = value; }
            public int Health => Defender.Health;
            public int MaxHealth => Defender.MaxHealth;
            public int Defense => Defender.Defense;
            public Hostility.Model Disposition => disposition as Hostility.Model;
            public bool IsAggressive { get => Disposition.IsAggressive; set => Disposition.IsAggressive = value; }
            public bool IsEnemy { get => Disposition.IsEnemy; set => Disposition.IsEnemy = value; }

            // public bool VerifyShortMoveEligibility(string tileName){
            //     return moveBehavior.VerifyShortMoveEligibility(tileName);
            // }

            // public void AddTile(string tileName){
            //     moveBehavior.AddTile(tileName);
            // }
            // public void AddTiles(string[] tiles){
            //     moveBehavior.AddTiles(tiles);
            // }
            // public void RemoveTile(string tileName){
            //     moveBehavior.RemoveTile(tileName);
            // }



            public override void _Ready()
            {
                base._Ready();
            }

            // public void NotifyTransport(Vector2I target){
            //     (transporter as Transportable.Model).NotifyTransport(target);
            // }

            public void IncreaseDamageOfMelee(int damageIncrement){
                (damageBuffer as BuffableDamage.Model).IncreaseDamageOfMelee(damageIncrement);
            }

            public void IncreaseDamageOfRanged(int damageIncrement){
                (damageBuffer as BuffableDamage.Model).IncreaseDamageOfRanged(damageIncrement);
            }

            public void IncreaseDamageOfSpell(int damageIncrement){
                (damageBuffer as BuffableDamage.Model).IncreaseDamageOfSpell(damageIncrement);
            }

            public void Attack(Tiles.Model actor){
                Offender.Attack(actor);
            }

            public void ConnectAttacked(Action<Vector2I, Vector2I> action){
                (offender as Offensive.Model).ConnectAttacked(action);
            }

            public void AssessSurroundings(Vector2I position){
                Hauler.AssessSurroundings(position);
            }

            public void ConnectTryFighting(Action<Tiles.Model> action){
                Hauler.ConnectTryFighting(action);
            }

            public void Move(Vector2I target)
            {
                Movement.Move(target);
            }

            public void TakeAStep(Vector2I target)
            {
                Movement.TakeAStep(target);
            }

            public void ConnectTookStep(Action<Vector2I> action)
            {
                throw new NotImplementedException();
            }

            public void TakeDamage(int damage)
            {
                Defender.TakeDamage(damage);
            }

            public void ReceiveHealing(int amount)
            {
                Defender.ReceiveHealing(amount);
            }

            public void ConnectTookDamage(Action<Vector2I, int> action){
                Defender.ConnectTookDamage(action);
            }

            public void ConnectGotConsumable(Action<Tiles.Model> action)
            {
                throw new NotImplementedException();
            }

            public void ConnectGotHealth(Action<Node> action){
                Movement.ConnectGotHealth(action);
            }

            public void ReceiveHealingFrom(Node tile)
            {
                Defender.ReceiveHealingFrom(tile);
            }

            public void ConnectLeftEmptyTile(Action<Vector2I> action)
            {
                Movement.ConnectLeftEmptyTile(action);
            }
        }
    }

}


