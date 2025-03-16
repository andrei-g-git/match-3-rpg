class_name Collections

static func resize_2d_array(grid: Array[Array], rows: int, columns: int, default_value):
	grid.resize(rows)
	for x in rows:
		grid[x].resize(columns)
		for y in columns:
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


static func change_string_2d_array_to_int(string_array):#: Array[Array]):
	var width = string_array.size()
	var height = string_array[0].size()
	var int_array: Array[Array] = []
	int_array.resize(width)
	for x in width:
		int_array[x].resize(height)
		for y in height:
			int_array[x][y] = string_array[x][y].to_int()
	return int_array
