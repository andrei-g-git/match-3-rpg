class_name GridUtilities

static func print_array_initials_old(tiles, header: String):
	var columns = tiles.size()
	var rows = tiles[0].size()
	var intialsGrid: Array[Array] = []

	intialsGrid.resize(columns)

	print(header, "\n")
	for i in columns:
		if rows != 0:
			intialsGrid[i] = tiles[i].map(func (item): return "" if item == "" else item[0]) #get first letter		
			print(intialsGrid[i])
	print("\n")
	
	
static func print_array_initials(tiles, header: String):
	var cols: int = tiles.size()
	var rows: int = tiles[0].size()
	for x in rows:
		var first_letters_row: Array[String] = []
		for y in cols:
			var tile_name = tiles[y][x]
			first_letters_row.append(tile_name[0])
		print(first_letters_row)
	#print("\n")	
