using Godot;
using Godot.Collections;
using System;

public partial class View : GridContainer, Grid.Viewable, IGrid{ //IGrid ans Grid.Viewable kind of coincide..., should merge
	private int cols, rows = 0;
    public int Cols { get => cols; set => cols = value; }
    public int Rows { get => rows; set => rows = value; }


    public override void _Ready(){
        Columns = cols;
    }


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

    public void ConnectRandomizedTile(Action<string, Vector2I> action)
    {
        throw new NotImplementedException();
    }

}
