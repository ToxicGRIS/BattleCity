using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerCD))]

public class Enemy : Tank
{
    #region Properties

    

    #endregion
    #region Start

    private void Awake()
    {
        shootTimer = GetComponent<TimerCD>();
        rb = GetComponent<Rigidbody2D>();
    }

	#endregion

	private void Update()
	{
        var ray = Physics2D.RaycastAll(transform.position, transform.right, 25);
        if (ray[1].collider.gameObject.GetComponent<Player>()) ray[1].collider.GetComponent<Tank>().Health -= 1;
	}

	private void OnDrawGizmos()
	{
        var ray = Physics2D.RaycastAll(transform.position, transform.right, 25);
        Gizmos.color = new Color(1, 1, 1, 0.2f);
        Gizmos.DrawLine(transform.position, ray[1].point);
    }

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
