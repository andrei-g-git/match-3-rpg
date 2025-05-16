using Godot;
using System;
//using System.Collections.Generic;
using Godot.Collections;
using Grid;
using Tiles;
using Abstractions;
using Constants;
//			row
//	  o------------------------------->
//  c |
//  o |  
//  l |
//	  v
namespace Grid {
	public partial class Controller : Node {
		//[Export]
		//public List<PackedScene> tileScenes;

		public int columns;
		public int rows;
		private Model model;
		private Node parent;
		//private List<List<Control>> tileNodes;
		private Array<Array<Control>> tileNodes = new Array<Array<Control>>();
		private Grid.Factory factory;

		public Array<Array<string>> TemporaryTileNameGrid {get; set;} //delete

        public Controller(Model model_, Node parent_, Grid.Factory factory_) {
			model = model_;
			parent = parent_;
			factory = factory_;
		}

		public void Initialize() {
			model.CreateGrid();
			factory.Initialize();
			rows = model.Grid.Count;
			columns = model.Grid[0].Count;
			PopulateGrid(columns, rows, model.Grid, parent, tileNodes);
			RegisterObservers(columns, rows, model, tileNodes);
			//RegisterEachObserverToEachTileModel(model.Grid, tileNodes);
		}


		private void PopulateGrid(int columns_, int rows_, Array<Array<Tiles.Model>> modelTiles_, Node parent_, Array<Array<Control>> tileNodes_) {
			tileNodes_.Resize(rows_);			
			for(int x = 0; x < rows_; x++) {
				tileNodes_[x].Resize(columns_);
				for(int y = 0; y < columns_; y++) {
					//var nameToCompare = char.ToUpper(modelTiles_[x][y].Name[0]) + modelTiles_[x][y].Name.Substring(1);
					var nameGrid = TemporaryTileNameGrid;
					var name = nameGrid[x][y];
					var nameToCompare = char.ToUpper(/* nameGrid[x][y] */name[0]) + /* nameGrid[x][y] */name.Substring(1);
					var enumName = (TileNames) Enum.Parse(typeof(TileNames), nameToCompare);
					Control instance = tileNodes_[x][y] = factory.Create(enumName, modelTiles_[x][y] , parent_, new Vector2I(x, y));
					//((Modelable) instance).Model = modelTiles_[x][y]; //this is a different responsability...

					//parent_.AddChild(instance);   //need to initialize some stuff and I can't do it in it's ready() method
				}
			}
		}
		private void PopulateGrid_old(int columns_, int rows_, /* List<List<Tile>> modelTiles, */ Array<Array<Tiles.Model>> modelTiles_, Node parent_, /* List<List<Control>> tileNodes_ */Array<Array<Control>> tileNodes_) {
			tileNodes_.Resize(rows_);			
			for(int x = 0; x < rows_; x++) {
				tileNodes_[x].Resize(columns_);
				for(int y = 0; y < columns_; y++) {
					var nameToCompare = char.ToUpper(modelTiles_[x][y].Name[0]) + modelTiles_[x][y].Name.Substring(1);
					var enumName = (TileNames) Enum.Parse(typeof(TileNames), nameToCompare);
					Control instance = tileNodes_[x][y] = factory.Create(enumName, modelTiles_[x][y] , parent_, new Vector2I(x, y));
					//((Modelable) instance).Model = modelTiles_[x][y]; //this is a different responsability...

					//parent_.AddChild(instance);   //need to initialize some stuff and I can't do it in it's ready() method
				}
			}
		}

		private void RegisterObservers(int columns_, int rows_, Model gridModel_, /* List<List<Control>> tileNodes_ */Array<Array<Control>> tileNodes_) {
            for (int x = 0; x < rows_; x++) {
                for (int y = 0; y < columns_; y++) {
					gridModel_.Register(tileNodes_[x][y], x, y);
                }
            }
        }

		// private void RegisterEachObserverToEachTileModel(Array<Array<Tiles.Model>> tileModels, Array<Array<Control>> tileNodes){
		// 	for(int x = 0; x < tileModels.Count; x++){
		// 		for(int y = 0; y < tileModels[0].Count; y++){
		// 			((Modelable) tileModels[x][y]).Register(tileNodes[x][y]); //ERROR out of range
		// 		}				
		// 	}
		// }

	}


}

