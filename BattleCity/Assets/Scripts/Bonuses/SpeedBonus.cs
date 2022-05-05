using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : Bonus
{
	protected override void Activate(Player player)
	{
		player.ActivateSpeed(10);
		base.Activate(player);
	}
}
