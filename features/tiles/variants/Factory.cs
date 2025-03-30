using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Tiles;



namespace Tiles {
	public partial class Factory : Node {
		private Dictionary<string, Tile> tileVariants = new Dictionary<string, Tile>();

        public Factory() {

        }

		public Tile Create(string name_, Vector2I position_) {
			Tile tile = null;

            return name_ switch { //apparently this is the new c# switch statement...
                "archer" => new Archer(position_),
                "chest" => new Chest(position_),
                "defend" => new Defend(position_),
                "energy" => new Energy(position_),
                "fighter" => new Fighter(position_),
                "health" => new Health(position_),
                "melee" => new Melee(position_),
                "player" => new Player(position_),
                "ranged" => new Ranged(position_),
                "stamina" => new Stamina(position_),
                "unlock" => new Unlock(position_),
                "walk" => new Walk(position_),
                _ => null,
            };
        }
	}
}


//tileVariants.Add("archer", Archer);
//tileVariants.Add("chest", Chest);
//tileVariants.Add("defend", Defend);
//tileVariants.Add("energy", Energy);
//tileVariants.Add("fighter", Fighter);
//tileVariants.Add("health", Health);
//tileVariants.Add("melee", Melee);
//tileVariants.Add("player", Player);
//tileVariants.Add("ranged", Ranged);
//tileVariants.Add("stamina", Stamina);
//tileVariants.Add("unlock", Unlock);
//tileVariants.Add("walk", Walk);

