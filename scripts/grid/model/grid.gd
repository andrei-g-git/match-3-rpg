"""
			row
	o------------------------------->
  c |
  o |  
  l |
	v
"""

class_name GridModel

var _current_grid: Array[Array] = [] #these should have _ prefix, they're private
var _new_grid: Array[Array] = [] #not using anymore
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
	_current_grid.resize(rows)
	for a in rows:
		_current_grid[a].resize(columns)
		for b in columns:
			_current_grid[a][b] = ""		
	for x in rows:
		for y in columns:
			var loops = 0
			var next_tile = _generate_non_matching_tile(
				_choose_random_tile(possible_tiles), 
				x, 
				y, 
				_current_grid, 
				possible_tiles, 
				loops
			)
			#next_tile.replace("&", "")
			_current_grid[x][y] = next_tile
	_new_grid = _current_grid.duplicate(true)
	return _current_grid


func _is_match_at(x: int, y: int, tiles: Array[Array], tile_name: String):
	if( 
		(
			x > 1 and
			tiles[x-1][y] == tile_name and 
			tiles[x-2][y] == tile_name
		) or (
			y > 1 and
			tiles[x][y-1] == tile_name and 
			tiles[x][y-2] == tile_name					
		)			
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
	_current_grid: Array[Array],
	source: Vector2i,
	direction: Vector2i,
	source_name: String
):
	var destination : Vector2i = source + direction 	
	if(destination.x >= 0 and destination.y >= 0):
		var tile_to_swap = _current_grid[destination.x][destination.y]
		_current_grid[destination.x][destination.y] = source_name
		_current_grid[source.x][source.y] = tile_to_swap	
		
		#shouldn't be here
		_swap_tile_nodes(source, destination)
		
		#this runs after delay from tween animation in the tile
		_remove_matches(source_name)
		_remove_matches(tile_to_swap)

		GridUtilities.print_array_initials(_current_grid, "AFTER REMOVEING MATCHES")
		
		return _current_grid 
	return []	


func _swap_tile_nodes(source: Vector2i, destination: Vector2i):
	#not good, this doesn't separate different match groups (there can be 2 if both the swapped pieces result in matches)
	var source_node : Control = tile_nodes[source.x][source.y]
	var destination_node : Control = tile_nodes[destination.x][destination.y]
	source_node.update(destination)
	destination_node.update(source)


func _collapse_columns():
	for x in columns: 
		for y in range(rows - 1, -1, -1):
			if _current_grid[y][x] == TileConstants.EMPTY: 
				if y > 0:
					for z in range(y - 1, -1, -1):
						if _current_grid[z][x] != TileConstants.EMPTY:
							#var node = tile_nodes[y][x]#[z][x] 	
							var node = tile_nodes[z][x]
							#node.update(Vector2i(y, z))
							#node.update(Vector2i(z, x))
							node.update(Vector2i(y, x))
							_current_grid[y][x] = _current_grid[z][x] 
							_current_grid[z][x] = TileConstants.EMPTY	
							break


func test_collapse():
	_collapse_columns()


func _remove_matches(tile_to_match: String): 
		var horizontal_matches = _find_horizontal_matches(tile_to_match)
		var vertical_matches = _find_vertical_matches(tile_to_match)
		var matches = Collections.merge_arrays_shallow(horizontal_matches, vertical_matches)
		
		for x in matches.size():
			var vec = matches[x]
			_current_grid[vec.x][vec.y] = "zero"


func _find_vertical_matches(tile_to_match: String):
	var grid_ = _current_grid
	var matches: Array[Vector2i] = []
	for x in columns:
		for y in rows:
			if(y > 0 and y < (rows -1)):   
				if(
					#tile_to_match == grid_[x][y - 1] and 
					#tile_to_match == grid_[x][y] and 
					#tile_to_match == grid_[x][y + 1] 
					tile_to_match == grid_[y - 1][x] and 
					tile_to_match == grid_[y][x] and 
					tile_to_match == grid_[y + 1][x] 					
				):
					#there can't be more rows with matches of the same type, only another row
					#matches.append(Vector2i(x, y - 1))
					#matches.append(Vector2i(x, y))
					#matches.append(Vector2i(x, y + 1))
					matches.append(Vector2i(y - 1, x))
					matches.append(Vector2i(y, x))
					matches.append(Vector2i(y + 1, x))					
	return Collections.remove_array_duplicates(matches)	


func _find_horizontal_matches(tile_to_match: String):
	var grid_ = _current_grid
	var matches: Array[Vector2i] = []
	for x in rows:
		for y in columns:
			if(y > 0 and y < (columns - 1)):   
				if(
					#tile_to_match == grid_[y - 1][x] and 
					#tile_to_match == grid_[y][x] and 
					#tile_to_match == grid_[y + 1][x] 
					tile_to_match == grid_[x][y - 1] and 
					tile_to_match == grid_[x][y] and 
					tile_to_match == grid_[x][y + 1] 					
				):
					#there can't be more rows with matches of the same type, only another row
					#matches.append(Vector2i(y - 1, x))
					#matches.append(Vector2i(y, x))
					#matches.append(Vector2i(y + 1, x))
					matches.append(Vector2i(x, y - 1))
					matches.append(Vector2i(x, y))
					matches.append(Vector2i(x, y + 1))					
	return Collections.remove_array_duplicates(matches)	


func _find_linear_matches(tile_to_match: String, horizontally: bool):
	var grid_ = _current_grid
	var matches: Array[Vector2i] = []
	var y_offset = int(horizontally) # 1 or 0 to look left or right, but only in 1 dimension
	var x_offset = int(not horizontally)
	for x in rows:
		for y in columns:
			if(y > 0 and y < (columns -1)):   
				if(
					tile_to_match == grid_[x - x_offset][y - y_offset] and 
					tile_to_match == grid_[x][y] and 
					tile_to_match == grid_[x + x_offset][y + y_offset] 
				):
					#there can't be more columns with matches of the same type, only another row
					matches.append(Vector2i(x, y - 1))
					matches.append(Vector2i(x, y))
					matches.append(Vector2i(x, y + 1))
	return Collections.remove_array_duplicates(matches)	


func get_grid():
	return _new_grid










func _print_grid_coords(header: String):

	print(header, "\n")
	for i in rows:
		var coordinate_column = tile_nodes[i].map(func (item): return ( str(item.get_grid_index().x) + str(item.get_grid_index().y) )  ) #get first letter		
		print(coordinate_column)
	print("\n")		
