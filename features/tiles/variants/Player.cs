using Godot;
using Godot.Collections;

namespace Tiles {
    public partial class Player : Tiles.Model, Movable, Transportable.Model, BuffableDamage.Model{
        private Vector2I position;
        public override string Name => "player";
        private Movable moveBehavior = null;
        public Movable MoveBehavior {get => moveBehavior; set => moveBehavior = value;}
        private Transportable.Model transportBehavior = null;
        public Transportable.Model TransportBehavior{get => transportBehavior; set => transportBehavior = value;}
        private Array<string> shortMoveTiles = [];
        public Array<string> ShortMoveTiles {get => shortMoveTiles;}
        private BuffableDamage.Model damageBuffBehavior = null;
        public BuffableDamage.Model DamageBuffBehavior {get => damageBuffBehavior; set => damageBuffBehavior = value;}
        private int meleeBuff = 0;
        public int MeleeBuff { get => damageBuffBehavior.MeleeBuff; set => damageBuffBehavior.MeleeBuff = value; }
        private int rangedBuff = 0;        
        public int RangedBuff { get => damageBuffBehavior.RangedBuff; set => damageBuffBehavior.RangedBuff = value; }
        private int spellBuff = 0;        
        public int SpellBuff { get => damageBuffBehavior.SpellBuff; set => damageBuffBehavior.SpellBuff = value; }

        public Player(Vector2I position) : base(position) {

        }

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
            transportBehavior.NotifyTransport(target);
        }

        public void IncreaseDamageOfMelee(int damageIncrement){
            damageBuffBehavior.MeleeBuff += damageIncrement;
        }

        public void IncreaseDamageOfRanged(int damageIncrement){
            damageBuffBehavior.RangedBuff += damageIncrement;
        }

        public void IncreaseDamageOfSpell(int damageIncrement){
            damageBuffBehavior.SpellBuff += damageIncrement;
        }
    }
}


