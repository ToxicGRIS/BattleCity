using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IShotable
{
	[SerializeField] [Min(0)] private int health = 1;

    public void GotShot(Bullet bullet)
	{
		health -= 1;
		if (health <= 0) Destroy(gameObject);
	}
}
