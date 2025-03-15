class_name Collections

static func resize_2d_array(grid: Array[Array], columns: int, rows: int, default_value):
	grid.resize(columns)
	for x in columns:
		grid[x].resize(rows)
		for y in rows:
			grid[x][y] = default_value
	return grid

static func remove_array_duplicates(array_: Array) -> Array:
	var dict := {}
	for item in array_:
		dict[item] = 1
	return dict.keys()
	
static func merge_arrays_shallow(array1: Array, array2: Array):
	var dict1 := {}
	var dict2 := {}
	for item in array1:
		dict1[item] = 1
	for item in array2:
		dict2[item] = 1	
	dict1.merge(dict2)
	return dict1.keys()	
