class_name Buffs

var melee: Effect = null:
	get: return melee
	set(instance): melee = instance

var ranged: Effect = null:
	get: return ranged
	set(instance): ranged = instance

var spell: Effect = null:
	get: return spell
	set(instance): spell = instance

var defense: Effect = null:
	get: return defense
	set(instance): defense = instance

var melee_buff: int = 0:
	get: return melee.magnitude
	set(instance): melee.magnitude = instance
	
var melee_duration: int = 0:
	get: return melee.duration
	set(instance): melee.duration = instance
	
var ranged_buff: int = 0:
	get: return ranged.magnitude
	set(instance): ranged.magnitude = instance	

var ranged_duration: int = 0:
	get: return ranged.duration
	set(instance): ranged.duration = instance

var spell_buff: int = 0:
	get: return spell.magnitude
	set(instance): spell.magnitude = instance	

var spell_duration: int = 0:
	get: return spell.duration
	set(instance): spell.duration = instance

var defense_buff: int = 0:
	get: return defense.magnitude
	set(instance): defense.magnitude = instance	

var defense_duration: int = 0:
	get: return defense.duration
	set(instance): defense.duration = instance


func _init(melee_: Effect, ranged_: Effect, spell_: Effect, defense_: Effect):
	melee = melee_
	ranged = ranged_
	spell = spell_
	defense = defense_


func decrement_effect_durations():
	_decrement_melee_duration()
	_decrement_ranged_duration()
	_decrement_spell_duration()
	_decrement_defense_duration()
	
func _decrement_melee_duration():
	if melee_duration > 0:
		melee_duration -= 1
	else:
		melee_duration = 0
		melee_buff = 0


func _decrement_ranged_duration():		
	if ranged_duration > 0:
		ranged_duration -= 1
	else:
		ranged_duration = 0
		ranged_buff = 0


func _decrement_spell_duration():		
	if spell_duration > 0:
		spell_duration -= 1
	else:
		spell_duration = 0
		spell_buff = 0


func _decrement_defense_duration():		
	if defense_duration > 0:
		defense_duration -= 1
	else:
		defense_duration = 0
		defense_buff = 0						


func replace_melee_buff(amount: int):
	melee_buff = amount


func replace_ranged_buff(amount: int):
	ranged_buff = amount


func replace_spell_buff(amount: int):
	spell_buff = amount


func replace_defense_buff(amount: int):
	defense_buff = amount
