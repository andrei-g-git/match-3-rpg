using Constants;
using Godot;

namespace Tiles {
    namespace Health{
        public partial class Model : Tiles.Model, Healable.Model, Obtainable.Model, Listenable {
            [Export]
            private Node healing;
            private Vector2I position;
            public override string Name => "health";
            public Healable.Model Healing => healing as Healable.Model;
            public int HealAmount => Healing.HealAmount;
            public int HealAmountOnMatch => (healing as Healable.Model).HealAmountOnMatch;
            public bool IsObtainable => true; //should get from child behavior but it's always true, screw it

            public void Connect(Node emitter){
                (healing as Listenable).Connect(emitter);
            }

            public void HealPlayer(Defensive.Model player){
                Healing.HealPlayer(player);
            }

            public void HealPlayerOnMatch(){
                Healing.HealPlayerOnMatch();
            }

        }        
    }

}


