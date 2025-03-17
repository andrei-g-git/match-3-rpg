class_name CombatBehavior

class Combatability:
	var melee_damage: int = 0:
		get: return melee_damage
		set(value): melee_damage = value

	var ranged_damage: int = 0:
		get: return ranged_damage
		set(value): ranged_damage = value

	var spell_damage: int = 0:
		get: return spell_damage
		set(value): spell_damage = value

	var damage_block: int = 0:
		get: return damage_block
		set(value): damage_block = value	
