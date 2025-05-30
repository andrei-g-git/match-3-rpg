using System;
using Constants;
using Godot;
using Godot.Collections;
using Grid;

public partial class TacticalGrid : Node //GridContainer
{
	[Export] private Array<PackedScene> tileScenes; //should all be private
	[Export]
	public Node View;
	private Grid.Model model;
	public Grid.Model Model => model;

	// [Export] public int cols;
	// [Export] public int rows;	
	//[Export]
	private GridContainer gridContainer;
	public override void _Ready(){
		// ///////////test
		// var foo = new Ztest.Impl();
		// var bar = new Ztest.Sender();
		// (bar as Ztest.Sender).ConnectMySignal((foo as Ztest.Inter).SomeMethod);
		// AddChild(foo);
		// AddChild(bar);
		// /////////////////////


		var table = FileUtilities.LoadCsv("D:\\projects\\match3\\mapping\\New folder\\a-7.2.csv");
		var intTable = Collections.Change2dStringArrayToInt(table);
		var tileNameGrid = GridUtilities.AssignTileNamesToIntGrid(intTable);	
		GridUtilities.PrintGridInitialsFromStringMatrix(tileNameGrid, "GRID INITIALS");

		gridContainer = GetNode<GridContainer>("%View");
		(gridContainer as IGrid).Cols = tileNameGrid.Count;
		(gridContainer as IGrid).Rows = tileNameGrid[0].Count;


		var tileNodeFactory = new Grid.Factory(tileScenes);

		model = new Model(
			tileScenes,
			tileNameGrid,
			new Tiles.Factory(),
			tileNodeFactory,
			//this
			gridContainer
		);
		model.Initialize();

		var controller = new Controller(
			model,
			//this,
			gridContainer,
			tileNodeFactory
		);

		//THIS SHOULD PROBABLY HAPPEN AFTER controller.Initialize() since that's where tiles are added to the parent grid
		(model as IGrid).ConnectRandomizedTile((string tileName, Vector2I position) => {
			var tileNode = tileNodeFactory.Create(
				(TileNames) Enum.Parse(typeof(TileNames), char.ToUpper(tileName[0]) + tileName.Substring(1)),
				//this,
				gridContainer,
				position
			);
			model.SetTile(tileNode, position.X, position.Y); //probably won't work, Create() aleardy adds the node to the tree willy nilly and that's not enough initialization I think, still need to register etc...
		});

		


		//delete, temporary
		controller.TemporaryTileNameGrid = tileNameGrid;	
		
			
		controller.Initialize();

		model.ConnectReplaceableTiles(); //sould be in initializer

		var bp = 123;
	}
}
