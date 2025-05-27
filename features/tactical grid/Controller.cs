using Godot;
using System;
using Godot.Collections;
using Grid;
using Constants;
//			row
//	  o------------------------------->
//  c |
//  o |  
//  l |
//	  v
namespace Grid {
	public partial class Controller : Node {
		public int columns;
		public int rows;
		private Model model;
		private Node parent;
		private Array<Array<TileNode>> tileNodes = new Array<Array<TileNode>>();
		private Grid.Factory factory;
		public Array<Array<string>> TemporaryTileNameGrid {get; set;} //delete

        public Controller(Model model_, Node parent_, Grid.Factory factory_) {
			model = model_;
			parent = parent_;
			factory = factory_;
		}

		public void Initialize() {
			factory.Initialize();
			rows = model.Grid.Count;
			columns = model.Grid[0].Count;
			PopulateGrid(columns, rows, parent, tileNodes);
			RegisterObservers(columns, rows, model, tileNodes);
			var grid = (model as Grid.Model).Grid;
			var bp = 546;
		}


		//this is more of a model...

		private void PopulateGrid(int columns_, int rows_, Node parent_, Array<Array<TileNode>> tileNodes_) {
			tileNodes_.Resize(rows_);			
			for(int x = 0; x < rows_; x++) {
				tileNodes_[x].Resize(columns_);
				for(int y = 0; y < columns_; y++) {
					var nameGrid = TemporaryTileNameGrid;
					var name = nameGrid[x][y];
					var nameToCompare = char.ToUpper(name[0]) + name.Substring(1);
					var enumName = (TileNames) Enum.Parse(typeof(TileNames), nameToCompare);
					tileNodes_[x][y] = factory.Create(enumName, parent_, new Vector2I(x, y));
					var bp = 34656;
				}
			}
		}

		// private void Repopulate()

		private void RegisterObservers(int columns_, int rows_, Model gridModel_, Array<Array<TileNode>> tileNodes_) {
            for (int x = 0; x < rows_; x++) {
                for (int y = 0; y < columns_; y++) {
					gridModel_.Register(tileNodes_[x][y], x, y);
                }
            }
        }
	}
}

