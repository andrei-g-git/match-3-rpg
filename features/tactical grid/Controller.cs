using Godot;
using System;
//using System.Collections.Generic;
using Godot.Collections;
using Grid;
using Tiles;
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
		}

		private void PopulateGrid(int columns_, int rows_, /* List<List<Tile>> modelTiles, */ Array<Array<Tile>> modelTiles_, Node parent_, /* List<List<Control>> tileNodes_ */Array<Array<Control>> tileNodes_) {
			tileNodes_.Resize(rows_);			
			for(int x = 0; x < rows_; x++) {
				tileNodes_[x].Resize(columns_);
				for(int y = 0; y < columns_; y++) {
					Control instance = tileNodes_[x][y] = factory.Create(modelTiles_[x][y].Name);
					parent_.AddChild(instance);
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

	}


}

