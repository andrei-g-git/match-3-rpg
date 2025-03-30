class_name FileUtilities

static func save_array_to_text(data: Array, path: String):
	var file_ := FileAccess.open(path, FileAccess.WRITE)
	var json_string = JSON.stringify(data)
	
	var json = JSON.new()
	var error = json.parse(json_string)
	if error == OK:
		var data_received = json.data
		if typeof(data_received) == TYPE_ARRAY:
			file_.store_string(json_string)
			file_ = null			
		else:
			print("data must be an array")
	else:
		print("JSON Parse Error: ", json.get_error_message(), " in ", json_string, " at line ", json.get_error_line())	


static func load_csv(path: String):
	var grid := []
	var file = FileAccess.open(path, FileAccess.READ)
	while not file.eof_reached():
		var row = Array(file.get_csv_line())
		if row.size() > 1: # looks like csvs have a 'secret' row with crap content like ['0'] or something
			grid.append(row)
	return grid
