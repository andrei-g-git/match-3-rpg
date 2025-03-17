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


func _init(melee: Effect, ranged: Effect, spell: Effect, defense: Effect):
	melee = melee
	ranged = ranged
	spell = spell
	defense = defense
	
