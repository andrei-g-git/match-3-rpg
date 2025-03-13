class_name GridModel

#var last_grid: Array[Array] = []
#var current_grid: Array[Array] = []
#var source: Vector2i = Vector2i.ZERO #might not need
#var destination: Vector2i = Vector2i.ZERO
var tile_nodes: Array[Control] = []

func _init():
	pass
	
func register(tile: Control): #should be typed as whatever class the tile is; later I should give the tile node an interface
	tile_nodes.append(tile)
	
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
	for tile in tile_nodes:
		tile.update(source, destination)
		tile.update(destination, source)
	
