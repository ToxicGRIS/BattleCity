using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonus : Bonus
{
	protected override void Activate(Player player)
	{
		player.ActivateShield();
		base.Activate(player);
	}
}
