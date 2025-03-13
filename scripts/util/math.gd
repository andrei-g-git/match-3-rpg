class_name MathUtilities

static func invert_vecotr(vec : Vector2) -> Vector2:
	var x := vec.x
	vec.x = vec.y
	vec.y = x
	return vec
