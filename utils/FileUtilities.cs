using Godot.Collections;
using Godot;
using System.Linq;
public partial class FileUtilities
{
	public static Array<Array<string>>LoadCsv(string path){
		Array<Array<string>> grid = new Array<Array<string>>();
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		while(!file.EofReached()){
			var row = new Array<string>(file.GetCsvLine());//Array(file.get_csv_line())
			if(row.Count > 1){
				//grid.Append(row);	// looks like csvs have a 'secret' row with crap content like ['0'] or something
				grid.Add(row);
			} 
							
		}
		return grid;	
	}

}
