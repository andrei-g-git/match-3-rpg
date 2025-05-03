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
		var table = FileUtilities.LoadCsv("D:\\projects\\match3\\mapping\\New folder\\a-3.csv");
		var intTable = Collections.Change2dStringArrayToInt(table);
		var tileNameGrid = GridUtilities.AssignTileNamesToIntGrid(intTable);	
		GridUtilities.PrintGridInitials(tileNameGrid, "GRID INITIALS");

		Console.WriteLine("TACTICAL GRID READY EVENT");

		// var Model = GD.Load("res://features/tactical grid/Model.cs");
		// var Controller = GD.Load("res://features/tactical grid/Controller.cs");
		// var TileFactory = GD.Load("res://features/tiles/variants/Factory.cs");
		// var NodeFactory = GD.Load("res://features/tactical grid/Factory.cs");

		
		// var model = Model.New(tile_scenes, tile_name_grid, TileFactory.new())
		// var controller = Controller.new(model, self, NodeFactory.new())
		var model = new Model(
			tileScenes,
			tileNameGrid,
			new Tiles.Factory()
		);
		model.Initialize();
		var controller = new Controller(
			model,
			this,
			new Grid.Factory(tileScenes/* , new Tiles.Factory() */)
		);
		controller.Initialize();
	}

	public override void _Process(double delta)
	{
	}
}
