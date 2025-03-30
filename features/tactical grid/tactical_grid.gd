extends GridContainer

@export var tile_scenes : Array[PackedScene]
@export var cols : int ;
@export var rows : int ;

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var table = FileUtilities.load_csv("D:\\projects\\match3\\mapping\\New folder\\a-3.csv")
	var int_table = Collections.change_string_2d_array_to_int(table)
	print(int_table)		
	var tile_name_grid = GridUtilities.convert_int_tile_grid_to_actual_names(int_table)
	GridUtilities.print_array_initials(tile_name_grid, "grid on load from csv")
	
	
	var Model = load("res://features/tactical grid/Model.cs")
	var Controller = load("res://features/tactical grid/Controller.cs")
	var TileFactory = load("res://features/tiles/variants/Factory.cs")
	var NodeFactory = load("res://features/tactical grid/Factory.cs")
	
	var model = Model.new(tile_scenes, tile_name_grid, TileFactory.new())
	var controller = Controller.new(model, self, NodeFactory.new())
	
	model.CreateGrid()
	
	controller.Initialize()


func _process(delta: float) -> void:
	pass
