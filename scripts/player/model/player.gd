class_name PlayerModel

#var buffs: Buffs = null:
	#get: return buffs
	#set(instance): buffs = instance
#
#var debuffs
#
#var combat: CombatBehavior.Combatability = null:
	#get: return combat
	#set(instance): combat = instance
	
var buffs: Buffs
var debuffs
var combat: CombatBehavior.Combatability

var melee_damage: int = 0:
	get: return combat.melee_damage + buffs.melee_buff 

var ranged_damage: int = 0:
	get: return combat.ranged_damage + buffs.ranged_buff 

var spell_damage: int = 0:
	get: return combat.spell_damage + buffs.spell_buff 

var defense: int = 0:
	get: return combat.damage_block + buffs.defense_buff 


func _init(buffs_: Buffs, debuffs_, combat_: CombatBehavior.Combatability):
	buffs = buffs_
	debuffs = debuffs_
	combat = combat_
	var abc = 123


#func get_melee_damage():
	#return combat.melee_damage + buffs.melee_buff
#
#
#func get_ranged_damage():
	#return combat.ranged_damage + buffs.ranged_buff
#
#
#func get_defense():
	#return combat.damage_block + buffs.defense_buff


func end_turn():
	_decrement_effect_durations()


func _decrement_effect_durations():
	buffs.decrement_effect_durations()


func buff_melee(amount: int):
	buffs.replace_melee_buff(amount)


func buff_ranged(amount: int):
	buffs.replace_ranged_buff(amount)


func buff_spell(amount: int):
	buffs.replace_spell_buff(amount)


func buff_defense(amount: int):
	buffs.replace_defense_buff(amount)
