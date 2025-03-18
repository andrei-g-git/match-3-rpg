extends GridContainer

"""
			row
	o------------------------------->
  c |
  o |  
  l |
	v
"""
@export var cols : int
@export var rows : int

@export var piecessss : Array[Resource] = []
@export var empty_tile_scene : Resource
@export var player: Resource

var model : GridModel = null
var pieces: Array[Resource] = []

var _pieces_in_grid : Array[Array] = []
var player_tile: Control

# Called when the node enters the scene tree for the first time.
func _ready() -> void:

	var table = FileUtilities.load_csv("C:/Users/me/Documents/j1.csv")
	var int_table = Collections.change_string_2d_array_to_int(table)
	print(int_table)		
	GridUtilities.convert_int_tile_grid_to_actual_names(int_table)


	player_tile = player.instantiate()

	
	pieces = piecessss#.slice(0, 3)	
	#pieces.assign(Collections.merge_arrays_shallow(piecessss, [player]))

	var possible_tile_names : Array[String] = []
	for tile_resource in pieces:
		var tile_name = (tile_resource as Resource) \
			.instantiate() \
			.name \
			.replace("&", "")
		possible_tile_names.append(tile_name)
		
	model = GridModel.new(
		possible_tile_names,
		cols,
		rows,
		PlayerModel.new(
			Buffs.new(
				Effect.new(),
				Effect.new(),
				Effect.new(),
				Effect.new(),
			),
			null,
			CombatBehavior.Combatability.new(),
			MoveBehavior.Movability.new(
				player_tile,
				[
					TileConstants.Tiles.keys()[TileConstants.Tiles.WALK].to_lower(),
					TileConstants.Tiles.keys()[TileConstants.Tiles.UNLOCK].to_lower()					
				]

			)
		)			
	)
	
	columns = cols
	_spawn_pieces()#_pieces_in_grid)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _spawn_pieces():#pieces_in_grid: Array[Array]):	
	var pieces_in_grid = model.create_grid()
	GridUtilities.print_array_initials(pieces_in_grid, "TEST NEW MODEL SPAWN")
	_populate_and_register_observers(pieces_in_grid, pieces, cols, rows)


func _populate_and_register_observers(grid : Array[Array], possible_pieces : Array[Resource], cols: int, rows: int):
	var grid_children = _populate_grid_with_nodes(grid, pieces, empty_tile_scene, cols, rows)
	model.initialize_observers()
	for x in rows:
		for y in cols:
			var tile_node = grid_children[x][y]	
			if tile_node != null:
				tile_node.set_grid_index(x, y)
				model.register(tile_node, x, y)	
				(tile_node as BaseButton).pressed.connect(_handle_possible_swap.bind(tile_node, grid, pieces, grid[x][y])) # this is basically observer pattern shit so it can stay...
				(tile_node as BaseButton).updated.connect(_populate_and_register_observers.bind(grid, pieces, cols, rows))			
	var bp = 123
	#print("POPULATED, NOW COLLAPSING")
	#now check if there are empty spots and collapse tiles above on those spots
	
	await get_tree().create_timer(0.5).timeout #obviously this is a very bad patch work, need to allow for collapse movement through proper signaling, not this timeout crap
	print("timed out for 0.5")	
	
	model.test_collapse()
	model.fill_empty_tiles()
	


func _populate_grid_with_nodes(grid : Array[Array], possible_pieces : Array[Resource], empty_node_resource: Resource, cols: int, rows: int):
	var possible_instance_names : Array[String] = []
	var instance_grid : Array[Array] = []
	instance_grid = Collections.resize_2d_array(instance_grid, rows, cols, null)
	for tile_resource in possible_pieces:
		possible_instance_names.append(tile_resource.instantiate().name.replace("&", ""))
	for N in get_children():
		N.queue_free()
	for x in rows:
		for y in cols:
			for z in possible_instance_names.size():
				var instance_name = possible_instance_names[z]
				var looped_tile = grid[x][y]
				if instance_name == grid[x][y]:
					var instance = possible_pieces[z].instantiate() #
					instance_grid[x][y] = instance
					add_child(instance) 
			
			if grid[x][y] == "zero": #THE GRID CONTAINER COORDINATES ARE INVERTED COMPARED TO THE MODEL GRID'S, THIS WILL ROTATE THE NODE DELETION 90DEG\
				var blank_tile = empty_node_resource.instantiate()				
				instance_grid[x][y] = blank_tile
				add_child(blank_tile)
			if grid[x][y] == GridUtilities.convert_enum_key_to_lower_string(
				TileConstants.Tiles,
				TileConstants.Tiles.PLAYER
			):#TileConstants.Tiles.keys()[TileConstants.Tiles.PLAYER].to_lower():
				instance_grid[x][y] = player_tile
				add_child(player_tile)
				
		var columnnnnn_ = grid[x]
		var bp = 123
	return instance_grid


func _handle_possible_swap(tile: BaseButton, tiles: Array[Array], tile_resources: Array[Resource], tile_name: String):
	var direction : Vector2i = tile.get_drag_direction()
	var source : Vector2i = tile.get_grid_index()
	model.swap_2(
		tiles,
		source,
		direction,
		tile_name
	)


		
