using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
	[SerializeField] protected GameObject shootPoint;
	[SerializeField] protected GameObject bullet;

	protected TimerCD shootTimer;
	protected Rigidbody2D rb;

	protected int health = 1;
	protected float moveSpeed = 2;

	public int Health { get => health; set => health = value; }

	public abstract void Shoot();
	public abstract void Death();
}
