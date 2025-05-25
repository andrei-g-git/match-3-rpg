using Constants;
using Godot;

namespace Tiles {
    namespace Health{
        public partial class Model : Tiles.Model, Healable.Model, Consumable.Model, Listenable {
            [Export]
            private Node healing;
            private Vector2I position;
            public override string Name => "health";
            public Healable.Model Healing => healing as Healable.Model;
            public int HealAmount => Healing.HealAmount;
            public int HealAmountOnMatch => (healing as Healable.Model).HealAmountOnMatch;

            public void Connect(Node emitter){
                (healing as Listenable).Connect(emitter);
            }

            public void ConsumeFor(Tiles.Model actor)
            {
                throw new System.NotImplementedException();
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


