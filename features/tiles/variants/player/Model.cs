using Constants;
using Godot;
using Godot.Collections;

namespace Tiles { 
    namespace Player{
        public partial class Model : Tiles.Model, /* Movable, */ Transportable.Model, BuffableDamage.Model, Offensive.Model{ //should not have the buffable damage interface, only damage buff tiles can .. well, buff damage
            [Export]
            private Node transporter;
            [Export]
            private Node damageBuffer;
            [Export]
            private Node offender; //reeee
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

            public void NotifyTransport(Vector2I target){
                (transporter as Transportable.Model).NotifyTransport(target);
            }

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

        }
    }

}


