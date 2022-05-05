using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : Bonus
{
	protected override void Activate(Player player)
	{
		Score.AddScore(500);
	}
}
