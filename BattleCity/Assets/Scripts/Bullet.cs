using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	#region Properties

	[SerializeField] private float speed = 10;
	[SerializeField] private float liveTime = 10;

	public float Speed => speed;

	#endregion

	private void Update()
	{
		liveTime -= Time.deltaTime;
		if (liveTime <= 0) Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.transform != transform.parent.parent)
			Destroy(gameObject);
		if (collision.gameObject.GetComponent<Wall>() != null) Destroy(collision.gameObject);
	}
}
