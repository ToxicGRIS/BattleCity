using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBonus : Bonus
{
	protected override void Activate(Player player)
	{
		player.ActivateAttackSpeed(10);
		base.Activate(player);
	}
}
