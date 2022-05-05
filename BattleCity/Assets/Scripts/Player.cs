using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Tank
{
    #region Properties

    [SerializeField] private Controls input;
    [SerializeField] private Image Speedleft;
    [SerializeField] private Image AttackSpeedleft;
    [SerializeField] private Image Shieldleft;
    [SerializeField] private AudioSource shot;
    [SerializeField] private AudioSource death;

    [SerializeField] private float speedTime;
    [SerializeField] private float speedTimeLeft;
    [SerializeField] private float attackSpeedTime;
    [SerializeField] private float attackSpeedTimeLeft;

    private bool ShieldOn;

    #endregion
    #region Start

    private void Awake()
    {
        shootTimer = GetComponent<TimerCD>();
        rb = GetComponent<Rigidbody2D>();

        input = new Controls();
        input.Gameplay.Shoot.performed += e => Shoot();

        speedTime = 10;
        speedTimeLeft = 0;
        attackSpeedTime = 10;
        attackSpeedTimeLeft = 0;
        ShieldOn = false;
        Shieldleft.fillAmount = 0;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

	#endregion

	private void Update()
	{
        Move();
        if (speedTimeLeft > 0) speedTimeLeft -= Time.deltaTime;
        else moveSpeed = 2;
        if (attackSpeedTimeLeft > 0) attackSpeedTimeLeft -= Time.deltaTime;
        else shootTimer.Cooldown = 1.5f;
        Speedleft.fillAmount = speedTimeLeft / speedTime;
        AttackSpeedleft.fillAmount = attackSpeedTimeLeft / attackSpeedTime;
    }

	#region Movement

	private void Move()
    {
        Vector2 move = input.Gameplay.Move.ReadValue<Vector2>();
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
            shot.pitch = Random.Range(0.9f, 1.1f);
            shot.Play();
        }
    }

	public override void Death()
	{
        death.Play();
        Score.EndGame();
        Destroy(gameObject);
	}

	public override void GotShot(Bullet bullet)
	{

        if (ShieldOn)
        {
            ShieldOn = false;
            Shieldleft.fillAmount = 0;
        }
        else Health -= 1;
	}

    public void ActivateSpeed(float time)
	{
        speedTime = time;
        speedTimeLeft = time;
        moveSpeed = 4;
	}

    public void ActivateAttackSpeed(float time)
	{
        attackSpeedTime = time;
        attackSpeedTimeLeft = time;
        shootTimer.Cooldown = 0.5f;
    }

    public void ActivateShield()
	{
        ShieldOn = true;
        Shieldleft.fillAmount = 1;
	}
}
