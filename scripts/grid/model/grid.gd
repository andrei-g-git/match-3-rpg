class_name GridModel

var last_grid: Array[Array] = []
var current_grid: Array[Array] = []
#var source: Vector2i = Vector2i.ZERO #might not need
#var destination: Vector2i = Vector2i.ZERO
#var tile_nodes: Array[Control] = []
var tile_nodes: Array[Array] = [] #it's not strictly following the observer pattern but a 2d array is useful for picking specific observers

var possible_tiles: Array[String] = []
var columns := 0
var rows := 0


func _init(
	possible_tiles_: Array[String],
	columns_: int,
	rows_: int
):
	possible_tiles = possible_tiles_
	columns = columns_
	rows = rows_

func create_grid():
	current_grid.resize(rows)
	for a in rows:
		current_grid[a].resize(columns)
		for b in columns:
			current_grid[a][b] = ""		
	for x in rows:
		for y in columns:
			var loops = 0
			var next_tile = _generate_non_matching_tile(
				_choose_random_tile(possible_tiles), 
				x, 
				y, 
				current_grid, 
				possible_tiles, 
				loops
			)
			#next_tile.replace("&", "")
			current_grid[x][y] = next_tile
	last_grid = current_grid.duplicate(true)
	return current_grid

func _is_match_at(x: int, y: int, tiles: Array[Array], tile_name: String):
	if(			
		x > 1 #and          #I DON"T THINK I NEED THESE CHECKS, matching empty tiles doesn't change anything, they still get replaced by empty tiles after swapping
		#tiles[x-1][y] != "" and 
		#tiles[x-2][y] != "" 				
	):
		if(
			tiles[x-1][y] == tile_name and 
			tiles[x-2][y] == tile_name			
		):
			return true
	if(
		y > 1 #and 
		#tiles[x][y-1] != "" and 
		#tiles[x][y-2] != ""
	):
		if(
			tiles[x][y-1] == tile_name and 
			tiles[x][y-2] == tile_name			
		):
			return true
	return false


func _choose_random_tile(pissible_tiles):
	var random = floor(randi_range(0, possible_tiles.size() - 1))
	return possible_tiles[random]

func _generate_non_matching_tile(default_or_chosen_tile: String, x: int, y: int, grid: Array[Array], possible_tiles: Array[String], loops: int):
	while(_is_match_at(x, y, grid, default_or_chosen_tile) and loops <= rows * columns):
		var chosen_tile =  _choose_random_tile(possible_tiles)
		loops += 1
		_generate_non_matching_tile(chosen_tile, x, y, grid, possible_tiles, loops)
		return chosen_tile
	return default_or_chosen_tile #i have no idea what I'm doing lol


func initialize_observers():
	Collections.resize_2d_array(tile_nodes, rows, columns, null)

func register(tile: Control, x: int, y: int): #should be typed as whatever class the tile is; later I should give the tile node an interface
	#tile_nodes.append(tile)
	tile_nodes[x][y] = tile
	
func notify(x: int, y: int):
	#this is inefficient but it decouples the observer's interface
	for tile in tile_nodes:
		pass


func swap_2(
	last_grid: Array[Array],
	source: Vector2i,
	direction: Vector2i,
	source_name: String
):
	var destination : Vector2i = source + direction 	
	if(destination.x >= 0 and destination.y >= 0):
		var current_grid: Array[Array] = last_grid.duplicate(true)
		var tile_to_swap = current_grid[destination.x][destination.y]
		current_grid[destination.x][destination.y] = source_name
		current_grid[source.x][source.y] = tile_to_swap	
		
		#shouldn't be here
		_swap_tile_nodes(source, destination)
		
		return current_grid
	return []	
	
func _swap_tile_nodes(source: Vector2i, destination: Vector2i):
	var source_node : Control = tile_nodes[source.x][source.y]
	var destination_node : Control = tile_nodes[destination.x][destination.y]
	_print_grid_coords("@@@@@@@@@@@")
	source_node.update(destination)
	destination_node.update(source)
	_print_grid_coords("%%%%%%%%%%%%")	
		
	#var coord_grid = Array[Array]	
	#coord_array.resize(rows)


	
	#for tile in tile_nodes:
		#tile.update(source, destination)
		#tile.update(destination, source)
	
func _print_grid_coords(header: String):

	print(header, "\n")
	for i in rows:
		var coordinate_column = tile_nodes[i].map(func (item): return ( str(item.get_grid_index().x) + str(item.get_grid_index().y) )  ) #get first letter		
		print(coordinate_column)
	print("\n")		
