using Godot;

namespace Tiles {
    namespace Fighter{
        public partial class Model : Tiles.Model, Defensive.Model {
            [Export]
            private Node defender;
            private Vector2I position;
            public override string Name => "fighter";

            public int Health => Defender.Health;

            public int Defense =>  Defender.Defense;
            public Defensive.Model Defender => defender as Defensive.Model;

            public void TakeDamage(int damage){
                Defender.TakeDamage(damage);
            }
        }        
    }

}


