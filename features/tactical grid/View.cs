using Godot;
using Godot.Collections;
using System;

public partial class View : GridContainer, Grid.Viewable{
	private int cols, rows = 0; 
	// public int Cols {set => cols = value;} //these are redundant..
	// public int Rows {set => rows= value;}

	public void Update(Array<Array<Node>> tiles){
		ClearTiles();
		cols = tiles.Count;
		rows = tiles[0].Count;
		for(int x=0; x<cols; x++){
			for(int y=0; y<cols; y++){
				AddChild(tiles[x][y]);
			}
		}
	}

	public void ClearTiles(){
		foreach(Node tile in GetChildren()){
			tile.QueueFree();
		}
	}
}
