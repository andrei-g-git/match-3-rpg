class_name MoveBehavior

class Movability:
	var easy_move_tiles: Array[String] = []:
		get: return easy_move_tiles
	
	var grid_position: Vector2i = Vector2i.ZERO:
		get: return grid_position
		set(coordinates): grid_position = coordinates
	
	var observer: Control = null #player tile
	
	
	func _init(player_tile: Control, easy_move_tiles_: Array[String]):
		easy_move_tiles = easy_move_tiles_
		observer = player_tile


	func move_in_grid(target: Vector2i):
		grid_position = target
		observer.update(target)


	func check_for_natural_move_swap(tile_name: String) -> bool:
		return easy_move_tiles.has(tile_name)
