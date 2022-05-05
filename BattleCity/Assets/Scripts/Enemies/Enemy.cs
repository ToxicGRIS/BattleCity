using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerCD))]

public class Enemy : Tank
{
    #region Properties

    [SerializeField] protected bool alive;
    [SerializeField] protected LayerMask walls;

	protected Vector2 move;

    public bool Activated { get => alive; set { alive = value; } }

    #endregion
    #region Start

    private void Awake()
    {
        shootTimer = GetComponent<TimerCD>();
        rb = GetComponent<Rigidbody2D>();
        Activated = true;
    }

	#endregion

	private void FixedUpdate()
	{
        if (Activated)
        {
            var ray = Physics2D.RaycastAll(transform.position, transform.right, 25);
            if (ray[1].collider.gameObject.GetComponent<Player>() || ray[1].collider.gameObject.GetComponent<Wall>() || ray[1].collider.gameObject.GetComponent<Base>()) Shoot();
            if (transform.position.x % 1f < 0.04 && transform.position.y % 1f < 0.04)
            {
                float seeingDist = 0.51f;
                var rayUp = Physics2D.RaycastAll(transform.position, Vector2.up, seeingDist, walls);
                var rayDown = Physics2D.RaycastAll(transform.position, Vector2.down, seeingDist, walls);
                var rayLeft = Physics2D.RaycastAll(transform.position, Vector2.left, seeingDist, walls);
                var rayRight = Physics2D.RaycastAll(transform.position, Vector2.right, seeingDist, walls);

                if (rayDown.Length < 2)
                    move = Vector2.down;
                else if (rayLeft.Length < 2 && (move != Vector2.right || (rayRight.Length >= 2 && move == Vector2.right)))
                    move = Vector2.left;
                else if (rayRight.Length < 2 && (move != Vector2.left || (rayLeft.Length >= 2 && move == Vector2.left)))
                    move = Vector2.right;
                else if (rayUp.Length < 2 && (move != Vector2.down || (rayDown.Length >= 2 && move == Vector2.down)))
                    move = Vector2.up;
            }
            rb.velocity = move * moveSpeed;
            if (move.magnitude > 0) rb.SetRotation(Vector2.SignedAngle(Vector2.right, move));
            if (move.x == 0) transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
            if (move.y == 0) transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
        }
    }

	public override void Shoot()
	{
        if (shootTimer?.Ready ?? true)
        {
            GameObject spawnedBullet = Instantiate(bullet, shootPoint.transform);
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = spawnedBullet.transform.right * spawnedBullet.GetComponent<Bullet>().Speed;
            shootTimer?.ActivateCD();
            spawnedBullet.transform.parent = null;
        }
    }
	public override void Death()
	{
        Score.EnemyDied();
        Score.AddScore(250);
        Destroy(gameObject);
	}

    public override void GotShot(Bullet bullet)
	{
        Health -= 1;
	}
}
