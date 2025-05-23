using System.Threading.Tasks;
using Constants;
using Godot;

namespace Tiles {
    namespace Melee{
        public partial class Model : Tiles.Model, BuffableDamage.Model, Transfering.Model, Listenable{ 
            private Vector2I position;
            public override string Name => "melee";
            //public override NamableTile Type => TileName.Melee;
            private BuffableDamage.Model damageBuffer = null;
            public BuffableDamage.Model DamageBuffer {get => damageBuffer; set => damageBuffer = value;}
            //private int meleeBuff = 3;
            public int MeleeBuff { get => damageBuffer.MeleeBuff; /* set => damageBuffer.MeleeBuff = value; */ }
            //private int rangedBuff = 0;        
            public int RangedBuff { get => damageBuffer.RangedBuff; /* set => damageBuffer.RangedBuff = value; */ }
            //private int spellBuff = 0;        
            public int SpellBuff { get => damageBuffer.SpellBuff; /* set => damageBuffer.SpellBuff = value; */ }
            private Transfering.Model transfer;
            public Transfering.Model Transfer {get => transfer; set => transfer = value;}
            public Collections.FixedSizeArray<TileNames> Transferables { get => transfer.Transferables; set => transfer.Transferables = value; }


            // public Model(Vector2I position) : base(position) {

            // }

            public override void _Ready(){
                base._Ready();
                damageBuffer = GetNode<Node>("%DamageBuffer") as BuffableDamage.Model;
                transfer = GetNode<Node>("%Transfer") as Transfering.Model;
            }
            public void Connect(Node emitter){
                (damageBuffer as Listenable).Connect(emitter);
                (transfer as Listenable).Connect(emitter);
            }

            public void IncreaseDamageOfMelee(int damageIncrement){
                damageBuffer.IncreaseDamageOfMelee(damageIncrement);
            }

            public void IncreaseDamageOfRanged(int damageIncrement){
                damageBuffer.IncreaseDamageOfRanged(damageIncrement);
            }

            public void IncreaseDamageOfSpell(int damageIncrement){
                damageBuffer.IncreaseDamageOfSpell(damageIncrement);
            }

            public async Task TransferTile(){
                await transfer.TransferTile();
            }


        }        
    }

}


