using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonus : Enemy
{
	[SerializeField] private GameObject[] bonuses;
	public override void Death()
	{
		if (bonuses.Length > 0)
			Instantiate(bonuses[Random.Range(0, bonuses.Length)], transform.position, Quaternion.identity);
		base.Death();
	}
}
