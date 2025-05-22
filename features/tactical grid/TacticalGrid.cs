using System;
using Godot;
using Godot.Collections;
using Grid;
//using System;

public partial class TacticalGrid : GridContainer
{
	[Export] public Array<PackedScene> tileScenes;
	[Export] public int cols;
	[Export] public int rows;	
	public override void _Ready(){
		var table = FileUtilities.LoadCsv("D:\\projects\\match3\\mapping\\New folder\\a-6.csv");
		var intTable = Collections.Change2dStringArrayToInt(table);
		var tileNameGrid = GridUtilities.AssignTileNamesToIntGrid(intTable);	
		GridUtilities.PrintGridInitials(tileNameGrid, "GRID INITIALS");

		var model = new Model(
			tileScenes,
			tileNameGrid,
			new Tiles.Factory()
		);
		model.Initialize();
		var controller = new Controller(
			model,
			this,
			new Grid.Factory(tileScenes)
		);

		//delete, temporary
		controller.TemporaryTileNameGrid = tileNameGrid;	
		
			
		controller.Initialize();

		controller.CheckSomethingTHenDelete();

	}
}
