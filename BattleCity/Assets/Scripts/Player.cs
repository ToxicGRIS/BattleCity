using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank
{
    #region Properties

    [SerializeField] private Controls controls;

    #endregion
    #region Start

    private void Awake()
    {
        shootTimer = GetComponent<TimerCD>();
        rb = GetComponent<Rigidbody2D>();

        controls = new Controls();
        controls.Gameplay.Shoot.performed += e => Shoot();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

	#endregion

	private void Update()
	{
        Move();
	}

	#region Movement

	private void Move()
    {
        Vector2 move = controls.Gameplay.Move.ReadValue<Vector2>();
        rb.velocity = move * moveSpeed;
        if (move.magnitude > 0) rb.SetRotation(Vector2.SignedAngle(Vector2.right, move));
    }

    #endregion

    public override void Shoot()
	{
        if (shootTimer?.Ready ?? true)
        {
            GameObject spawnedBullet = Instantiate(bullet, shootPoint.transform);
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = spawnedBullet.transform.right * spawnedBullet.GetComponent<Bullet>().Speed;
            shootTimer?.ActivateCD();
        }
    }

	public override void Death()
	{
        Destroy(gameObject);
	}
}
