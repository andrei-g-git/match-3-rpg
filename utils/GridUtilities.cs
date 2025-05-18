using System;
using System.Linq;
using Constants;
using Godot;
using Godot.Collections;
using Tiles;


public static partial class GridUtilities { //these shouldn't be utilities, they should be in 'commmon'
	public static void PrintGridInitials(Array<Array<string>> tiles, string header){
		int rows = tiles.Count;
		int cols = tiles[0].Count;
		var initialsGrid = new Array<Array<string>>();
		initialsGrid.Resize(rows);
		Console.WriteLine(header, "\n");

		for(int i = 0; i < rows; i++){
			if(cols != 0){
				var cSharpArray = tiles[i].Select(item => item[..1]);
				initialsGrid[i] = [.. cSharpArray]; //new Array<string>(cSharpArray);
				//Console.WriteLine(initialsGrid[i]);		
				GD.Print(initialsGrid[i]);		
			}

		}
		Console.WriteLine("\n");
	}


	public static Array<Array<string>> AssignTileNamesToIntGrid(Array<Array<int>> intGrid){
		var newBoard = new Array<Array<string>>();
		newBoard.Resize(intGrid.Count);
		for(int a = 0; a < newBoard.Count; a++){
			newBoard[a].Resize(intGrid[a].Count);
			for(int b = 0; b < newBoard[a].Count; b++){
				var tileInt = intGrid[a][b];
				newBoard[a][b] = Enum.GetName(typeof(/* Tiles.Constants. */TileNames), tileInt).ToLower();
			}
		}
		return newBoard;		
	}


	public static bool CheckIfDirectionExists(Vector2I original, Vector2I direction){
		var _position = original + direction;
		return _position.X >= 0 && _position.Y >= 0;
	}

	//private static Vector2I FindMatchWithAdjacentTile(Tiles.Model tile_, Array<Array<Tiles.Model>> grid_, Vector2I direction){
	private static Vector2I FindMatchWithAdjacentTile(TileNode tile_, Array<Array<TileNode>> grid_, Vector2I direction){	
		if(GridUtilities.CheckIfDirectionExists(((Tiles.Model) tile_.Model).Position, direction)){
			var neighboringPosition = ((Tiles.Model) tile_.Model).Position + direction;
			if(tile_.Name == grid_[neighboringPosition.X][neighboringPosition.Y].Name){
				return neighboringPosition;
			}
		} 
		return new Vector2I(-1, -1);
	} 
	//public static Array<Vector2I> FindAllMatchingAdjacentTiles(Tiles.Model tile_, Array<Array<Tiles.Model>> grid_){
	public static Array<Vector2I> FindAllMatchingAdjacentTiles(TileNode tile_, Array<Array<TileNode>> grid_){
		Array<Vector2I> matches = [];
		matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Up)); 
		matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Right)); 
		matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Down)); 
		matches.Add(FindMatchWithAdjacentTile(tile_, grid_, Vector2I.Left)); 
		
		var cSharpValidMatches = matches.Where(match => match.X >= 0 && match.Y >= 0);
		return new Array<Vector2I>(cSharpValidMatches);
	}  

	public static Array<Array<string>> GetNamesGridFromTileGrid(Array<Array<Tiles.Model>> TileGrid){
		//return TileGrid.Select(tile => tile.Name)
		int width = TileGrid.Count;
		int height = TileGrid[0].Count;
		Array<Array<string>> NamesGrid = Collections.Create2DArray<string>(width, height);
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				NamesGrid[x][y] = TileGrid[x][y].Name;
			}
		}
		return NamesGrid;
	}	

	public static void PlaceTileOnBoard(TileNode tile, Array<Array<TileNode>> board, int x, int y){
		board[x][y] = tile;
		(tile.Model as Tiles.Model).Position = new Vector2I(x, y);
	}	
}


