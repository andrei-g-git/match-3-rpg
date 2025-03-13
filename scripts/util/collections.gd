class_name Collections

static func resize_2d_array(grid: Array[Array], rows: int, columns: int, default_value):
	grid.resize(rows)
	for x in rows:
		grid[x].resize(columns)
		for y in columns:
			grid[x][y] = default_value
	return grid
