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

            return name_ switch { //apparently this is the new c# switch statement... I wonder how I can add more expressions per case...
                "archer" => CreateArcher(position_),
                "chest" => CreateChest(position_),
                "defend" => CreateDefend(position_),
                "energy" => CreateEnergy(position_),
                "fighter" => CreateFighter(position_),
                "health" => CreateHealth(position_),
                "melee" => CreateMelee(position_),
                "player" => CreatePlayer(position_),
                "ranged" => CreateRanged(position_),
                "stamina" => CreateStamina(position_),
                "unlock" => CreateUnlock(position_),
                "walk" => CreateWalk(position_),
                _ => null,
            };
        }

        private Tile CreateArcher(Vector2I position){
            var tile = new Archer(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateChest(Vector2I position){
            var tile = new Chest(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateDefend(Vector2I position){
            var tile = new Defend(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateEnergy(Vector2I position){
            var tile = new Energy(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateFighter(Vector2I position){
            var tile = new Fighter(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateHealth(Vector2I position){
            var tile = new Health(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateMelee(Vector2I position){
            var tile = new Melee(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreatePlayer(Vector2I position){
            var tile = new Player(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateRanged(Vector2I position){
            var tile = new Ranged(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateStamina(Vector2I position){
            var tile = new Stamina(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateUnlock(Vector2I position){
            var tile = new Unlock(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tile CreateWalk(Vector2I position){
            var tile = new Walk(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
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

