using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
	protected new float moveSpeed = 4;

	public override void Death()
	{
		Score.AddScore(300);
		Destroy(gameObject);
	}
}
