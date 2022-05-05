using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IShotable
{
    [SerializeField] private int health = 3;

    public int Health { get => health; set { health = value; if (health <= 0) Score.EndGame(); } }

    public void GotShot(Bullet bullet)
	{
        Health -= 1;
	}
}
