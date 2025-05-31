using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;

public partial class View : GridContainer, Grid.Viewable, IGrid{ //IGrid ans Grid.Viewable kind of coincide..., should merge
	private int cols, rows = 0;
    public int Cols { get => cols; set => cols = value; }
    public int Rows { get => rows; set => rows = value; }

	//delete
	private bool isUpdating = false;

    public override void _Ready(){
        Columns = cols;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
		if(isUpdating){
			var bp =234;	//right so the coroutine does nothing...		
		}

    }


	//THIS RECEIVES A NEW ARRAY OF NODES, DO NOT TRY TO SET THEM, ONLY READ --not anymore
    //public void Update<[MustBeVariant]T>(Array<Array<T>> tiles) where T : Node{
	//public async Task Update<[MustBeVariant]T>(Array<Array<T>> tiles) where T : Node{
	public async Task Update_old(Array<Array</* Tile */Node>> tiles){	
		isUpdating = true;
		await ClearTilesCoroutine(); //apparently queuefree messes with the 'tiles' grid, the items inside are no longer nodes...
		cols = tiles.Count;
		rows = tiles[0].Count;
		for(int x=0; x<cols; x++){
			for(int y=0; y<rows; y++){
				var tile = /* (Node) */tiles[x][y];
				AddChild(tile/* (Node) tiles[x][y] */);
			}
		}
		var chiiii = GetChildren();
		var bp = 123;
	}

	public void Update(Array<Array<Node>> tiles){
		for(int x=0; x<tiles.Count; x++){
			for(int y=0; y<tiles[0].Count; y++){
				var tile = /* (Node) */tiles[x][y];
				RemoveChild(tile);
				//AddChild(tile/* (Node) tiles[x][y] */);
			}
		}	
		for(int x=0; x<tiles.Count; x++){
			for(int y=0; y<tiles[0].Count; y++){
				var tile = /* (Node) */tiles[x][y];
				//RemoveChild(tile);
				AddChild(tile/* (Node) tiles[x][y] */);
			}
		}			
	}

	private async Task ClearTilesCoroutine(){
		foreach(Node tile in GetChildren()){
			tile.QueueFree();
		}
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
	}

    public void ConnectRandomizedTile(Action<string, Vector2I> action)
    {
        throw new NotImplementedException();
    }

}
