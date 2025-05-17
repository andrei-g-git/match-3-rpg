using Godot;
using Godot.Collections;

namespace Tiles { 
    namespace Player{
        public partial class Model : Tiles.Model, Movable, Transportable.Model, BuffableDamage.Model{ //should not have the buffable damage interface, only damage buff tiles can .. well, buff damage
            private Vector2I position;
            public override string Name => "player";
            private Movable moveBehavior = null;
            public Movable MoveBehavior {get => moveBehavior; set => moveBehavior = value;}
            private Transportable.Model transporter/* Behavior */ = null;
            public Transportable.Model Transporter/* Behavior */{get => transporter/* Behavior */; set => transporter/* Behavior */ = value;}
            private Array<string> shortMoveTiles = [];
            public Array<string> ShortMoveTiles {get => shortMoveTiles;}
            private BuffableDamage.Model damageBuffer/* Behavior */ = null;
            public BuffableDamage.Model DamageBuffer/* Behavior */ {get => damageBuffer/* Behavior */; set => damageBuffer/* Behavior */ = value;}
            //private int meleeBuff = 0;
            public int MeleeBuff { get => damageBuffer/* Behavior */.MeleeBuff; /* set => damageBuffer.MeleeBuff = value; */ }
            //private int rangedBuff = 0;        
            public int RangedBuff { get => damageBuffer/* Behavior */.RangedBuff; /* set => damageBuffer.RangedBuff = value; */ }
            //private int spellBuff = 0;        
            public int SpellBuff { get => damageBuffer/* Behavior */.SpellBuff; /* set => damageBuffer.SpellBuff = value; */ }

            // public Model(Vector2I position) : base(position) {

            // }

            public bool VerifyShortMoveEligibility(string tileName){
                return moveBehavior.VerifyShortMoveEligibility(tileName);
            }

            public void AddTile(string tileName){
                moveBehavior.AddTile(tileName);
            }
            public void AddTiles(string[] tiles){
                moveBehavior.AddTiles(tiles);
            }
            public void RemoveTile(string tileName){
                moveBehavior.RemoveTile(tileName);
            }

            public void NotifyTransport(Vector2I target){
                transporter/* Behavior */.NotifyTransport(target);
            }

            public void IncreaseDamageOfMelee(int damageIncrement){
                damageBuffer/* Behavior */.IncreaseDamageOfMelee(damageIncrement);
            }

            public void IncreaseDamageOfRanged(int damageIncrement){
                damageBuffer/* Behavior */.IncreaseDamageOfRanged(damageIncrement);
            }

            public void IncreaseDamageOfSpell(int damageIncrement){
                damageBuffer/* Behavior */.IncreaseDamageOfSpell(damageIncrement);
            }
        }
    }

}


