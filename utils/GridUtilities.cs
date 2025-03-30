using System;
using System.Linq;
using Godot.Collections;


public partial class GridUtilities {
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
				Console.WriteLine(initialsGrid[i]);				
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
				newBoard[a][b] = Enum.GetName(typeof(Tiles.Constants.TileNames), tileInt).ToLower();
			}
		}
		return newBoard;		
	}


}


