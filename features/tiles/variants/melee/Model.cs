using Godot;

namespace Tiles {
    namespace Melee{
        public partial class Model : Tiles.Model, BuffableDamage.Model { 
            private Vector2I position;
            public override string Name => "melee";
            private int meleeBuff = 3;
            public int MeleeBuff { get => meleeBuff; set => meleeBuff = value; }
            private int rangedBuff = 0;        
            public int RangedBuff { get => rangedBuff; set => rangedBuff = value; }
            private int spellBuff = 0;        
            public int SpellBuff { get => spellBuff; set => spellBuff = value; }


            public Model(Vector2I position) : base(position) {

            }

            public void IncreaseDamageOfMelee(int damageIncrement){
                meleeBuff += damageIncrement;
            }

            public void IncreaseDamageOfRanged(int damageIncrement){
                rangedBuff += damageIncrement;
            }

            public void IncreaseDamageOfSpell(int damageIncrement){
                spellBuff += damageIncrement;
            }
        }        
    }

}


