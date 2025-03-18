class_name GridUtilities #lol

static func print_array_initials(tiles, header: String):
	var rows = tiles.size()
	var cols = tiles[0].size()
	var intialsGrid: Array[Array] = []

	intialsGrid.resize(rows)

	print(header, "\n")
	for i in rows:
		if cols != 0:
			intialsGrid[i] = tiles[i].map(func (item): return "" if item == "" else item[0]) #get first letter		
			print(intialsGrid[i])
	print("\n")


static func convert_int_tile_grid_to_actual_names(int_grid: Array[Array]):
	var new_board: Array[Array] = []
	new_board.resize(int_grid.size())
	for a in int_grid.size():
		new_board[a].resize(int_grid[a].size())
		for b in int_grid[a].size():
			var tile_int = int_grid[a][b]
			new_board[a][b] = TileConstants.Tiles \
				.keys()[tile_int] \
				.to_lower() #\
				#.capitalize()
	#GridUtilities.print_array_initials(new_board, "CSV CSV CSV:")	
	return new_board


static func convert_enum_key_to_lower_string(enum_, enum_value):
	return enum_.keys()[enum_value].to_lower()
