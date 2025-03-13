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

var model : GridModel = null
var pieces = []

var _pieces_in_grid : Array[Array] = []

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pieces = piecessss#.slice(0, 3)	
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
		rows
	)
	
	columns = cols
	_spawn_pieces()#_pieces_in_grid)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _spawn_pieces():#pieces_in_grid: Array[Array]):	
	var pieces_in_grid = model.create_grid()
	print_array_initials(pieces_in_grid, "TEST NEW MODEL SPAWN")
			#piece.set_grid_index(x, y)
			#add_child(piece)  #MVC ALL PIECE VIEWS SHOULD BE RE_POPULATED EACH TIME AN ACTION IS COMPLETED
			#
			#model.register(piece)
	var grid_children =  _populate_grid_with_nodes(pieces_in_grid, piecessss, cols, rows)
	model.initialize_observers()
	for x in rows:
		for y in cols:
			var tile_node = grid_children[x][y]	
			tile_node.set_grid_index(x, y)
			model.register(tile_node, x, y)	
			(tile_node as BaseButton).pressed.connect(_handle_possible_swap.bind(tile_node, pieces_in_grid, piecessss, pieces_in_grid[x][y])) # this is basically observer pattern shit so it can stay...
	var bp = 123
	
	#return pieces_in_grid


func _populate_grid_with_nodes(grid : Array[Array], possible_pieces : Array[Resource], cols: int, rows: int):
	var possible_instance_names : Array[String] = []
	var instance_grid : Array[Array] = []
	instance_grid = Collections.resize_2d_array(instance_grid, rows, cols, "")
	for tile_resource in possible_pieces:
		possible_instance_names.append(tile_resource.instantiate().name.replace("&", ""))
	for x in rows:
		for y in cols:
			for z in possible_instance_names.size():
				var instance_name = possible_instance_names[z]
				var looped_tile = grid[x][y]
				if instance_name == grid[x][y]:
					var instance = possible_pieces[z].instantiate() #
					instance_grid[x][y] = instance

					add_child(instance)  #this doens't work for some reason despite instance_grid seemingly getting the righth items...
	#for a in rows: #godot is geting a bit flaky for me...
		#for b in cols:
			#add_child(instance_grid[a][b])
			#print(a, "    ", b, "   ", instance_grid[a][b].name)
	return instance_grid


func _choose_random_piece(pieces_: Array[Resource]):
	var random = floor(randi_range(0, pieces_.size() - 1))
	var piece : Control = (pieces_[random] as Resource).instantiate()
	#print(piece)
	return piece


func _is_match_at(x: int, y: int, pieces_in_grid: Array[Array], piece_name: String):
	#print("current: \n %s \n last: \n %s \n 2nd last: \n %s" % [piece_name, pieces_in_grid[x-1][y], pieces_in_grid[x-2][y]])
	if(
		#x > 1 and 
		#pieces_in_grid[x] \
			#.slice(-2, pieces_in_grid[x].size()) \
			#.has(piece)			
		y > 1 and 
		pieces_in_grid[x-1][y] != "" and #don't need this if I compare strings, the current piece will never be empty string
		pieces_in_grid[x-2][y] != "" 				
	):
		if(
			pieces_in_grid[x-1][y] == piece_name and 
			pieces_in_grid[x-2][y] == piece_name		
						
		):
			return true
	if(
		y > 1 and 
		pieces_in_grid[x][y-1] != "" and 
		pieces_in_grid[x][y-2] != ""
	):
		if(
			pieces_in_grid[x][y-1] == piece_name and 
			pieces_in_grid[x][y-2] == piece_name			
		):
			return true
	return false
	
func _handle_possible_swap(tile: BaseButton, tiles: Array[Array], tile_resources: Array[Resource], tile_name: String):
	var direction : Vector2i = tile.get_drag_direction()
	var source : Vector2i = tile.get_grid_index()
	#var destination : Vector2i = source + direction 	
	#var new_board = _swap_2_tiles(
		#tiles,
		#tile,
		#source,
		#destination,
		#direction,
		#tile_name
	#)
	
	
	#for N in get_children():
		#N.queue_free()
	#var bp = 123
	
	var new_board = model.swap_2(
		tiles,
		source,
		direction,
		tile_name
	)
	
	
	#_remove_matches(new_board)
	
	#print_array_initials(tiles, "original")
	#print_array_initials(new_board, "altered")
	
	#_populate_grid(new_board, tile_resources)


func _swap_2_tiles(
	tiles: Array[Array],
	pressed_tile: BaseButton,
	source: Vector2i,
	destination: Vector2i,
	direction: Vector2i,
	tile_name: String
):
	if(destination.x >= 0 and destination.y >= 0):
		var new_board: Array[Array] = tiles.duplicate(true)
		var tile_to_swap = new_board[destination.x][destination.y]
		new_board[destination.x][destination.y] = tile_name#pressed_tile.name.replace("&", "")
		new_board[source.x][source.y] = tile_to_swap	
		return new_board
	return []

#this only finds 1 matching tile, not 3 or more, because it uses _is_match_at which only registers a match once and so only 1 tile will be stored in the matching tile list even if there are 3+
func _remove_matches(tiles: Array[Array]) -> Array[Array]:
	var rows = tiles.size()
	var cols = tiles[0].size() 
	var matching_tile_indexes : Array[Vector2i] = []
	for x in rows:
		for y in cols:
			if _is_match_at(x, y, tiles, tiles[x][y]):
				matching_tile_indexes.append(Vector2i(x, y))
	for a in rows:
		for b in cols:
			for c in matching_tile_indexes.size():
				if(a == matching_tile_indexes[c].x and b == matching_tile_indexes[c].y):
					tiles[a][b] = ""
	return tiles

#might be better to work with instance array than name arrays? -- at least switch to enums or something...
func _populate_grid(tiles: Array[Array], tile_resources: Array[Resource]):
	var rows = tiles.size()
	var cols = tiles[0].size()
	var resources = tile_resources.size()
	
	print_array_initials(tiles, "altered-two")
	
	print("\n\n######################\n")
	#print("SIZE:   ", rows, "   ", cols)
	for x in rows: #gotta iterate grid first and resources last otherwise it populates with only 3 pieces
		for y in cols:
			for z in resources:
				var instance_ : Control = (tile_resources[z] as Resource).instantiate()
				var name_ = instance_.name.replace("&", "")
				if name_ == tiles[x][y]:
					print_array_initials(tiles, "x, y    " + str(x) + "  " + str(y))
					#await get_tree().create_timer(0.1).timeout
					add_child(instance_)



func print_array_initials(tiles, header: String):

	var intialsGrid: Array[Array] = []

	intialsGrid.resize(rows)

	print(header, "\n")
	for i in rows:
		if tiles[i].size():
			intialsGrid[i] = tiles[i].map(func (item): return item[0]) #get first letter		
			print(intialsGrid[i])
	print("\n")
		
