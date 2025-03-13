extends TextureButton
"""
			Y
	o--------------->
	|
  X |  
	|
	v
"""
var _pressing: bool = false
var _pressed: bool = false
var drag_direction = Vector2.ZERO
var _total_drag = Vector2.ZERO

@export var drag_treshold: int = 16

var _grid_index = Vector2i.ZERO
var tween : Tween
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	self.pressed.connect(_on_self_pressed)
	self.button_up.connect(_on_self_released)
	self.button_down.connect(_on_self_pressing)
	
	#tween = create_tween()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass	

func update(self_position, destination):
	if self_position == _grid_index:
		var target_pixel_position = destination * 45
		move(target_pixel_position)

func move(target: Vector2):
	var delta = target - position
	
	create_tween() \
		.set_trans(Tween.TRANS_ELASTIC) \
		.set_ease(Tween.EASE_OUT) \
		.tween_property(self, "position", target, 0.5)
	#tween.interpolate_value(
		#position,
		#delta,
		#0.15,
		#0.3,
		#Tween.TRANS_ELASTIC,
		#Tween.EASE_OUT
	#)

func get_grid_index() -> Vector2i:
	return _grid_index


func set_grid_index(x: int, y: int):
	_grid_index = Vector2i(x, y)
	#print("index in grid:  ", x, "  ", y)


func _on_self_pressed():
	_pressed = true
	_total_drag = Vector2.ZERO


func _on_self_released():
	_pressing = false
	_pressed = false
	_total_drag = Vector2.ZERO
	drag_direction = Vector2.ZERO


func _on_self_pressing():
	_pressing = true;
	

func _input(event: InputEvent) -> void:
	if event is InputEventScreenDrag:
		if _pressing:
			var drag_this_frame = event.relative
			_total_drag += drag_this_frame 
			#GRID NODE IS REVERSED IN GODOT SO I REPLACED X WITH Y IN THIS CHECK
			if(
				#_total_drag.x >= -drag_treshold and 
				#_total_drag.x <= drag_treshold
				_total_drag.y >= -drag_treshold and 
				_total_drag.y <= drag_treshold				
			):
				#if _total_drag.y >= drag_treshold:
				if _total_drag.x >= drag_treshold:
					drag_direction = Vector2.DOWN
				#if _total_drag.y <= -drag_treshold:
				if _total_drag.x <= -drag_treshold:
					drag_direction = Vector2.UP
			if(
				#_total_drag.y >= -drag_treshold and 
				#_total_drag.y <= drag_treshold
				_total_drag.x >= -drag_treshold and 
				_total_drag.x <= drag_treshold
			):
				#if _total_drag.x >= drag_treshold:
				if _total_drag.y >= drag_treshold:
					drag_direction = Vector2.RIGHT
				#if _total_drag.x <= -drag_treshold:
				if _total_drag.y <= -drag_treshold:
					drag_direction = Vector2.LEFT


func get_drag_direction() -> Vector2:
	return drag_direction
