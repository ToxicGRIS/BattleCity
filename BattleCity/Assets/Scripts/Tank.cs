using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour, IShotable
{
	[SerializeField] protected GameObject shootPoint;
	[SerializeField] protected GameObject bullet;
	[SerializeField] protected int health = 1;
	[SerializeField] protected float moveSpeed = 2;

	protected TimerCD shootTimer;
	protected Rigidbody2D rb;

	public int Health { get => health; set { health = value; if (health <= 0) Death(); } }

	public abstract void Shoot();
	public abstract void Death();
	public abstract void GotShot(Bullet bullet);
}
