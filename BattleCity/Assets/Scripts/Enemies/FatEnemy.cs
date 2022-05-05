using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemy : Enemy
{
	protected new int health = 2;

	public override void Death()
	{
		Score.AddScore(350);
		Destroy(gameObject);
	}
}
