class_name PlayerModel

var buffs: Buffs = null:
	get: return buffs
	set(instance): buffs = instance

var debuffs

var combat: Buffs = null:
	get: return combat
	set(instance): combat = instance

var melee_damage: int = 0:
	get: return combat.melee_damage + buffs.melee_buff 

var ranged_damage: int = 0:
	get: return combat.ranged_damage + buffs.ranged_buff 

var spell_damage: int = 0:
	get: return combat.spell_damage + buffs.spell_buff 

var defense: int = 0:
	get: return combat.damage_block + buffs.defense_buff 

func _init(buffs: Buffs, debuffs, combat: CombatBehavior.Combatability):
	buffs = buffs
	debuffs = debuffs
	combat = combat
