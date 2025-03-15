class_name GridUtilities

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
