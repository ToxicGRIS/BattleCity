using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
	protected virtual void Activate(Player player)
	{
		Score.AddScore(50);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Player>())
		{
			Activate(collision.gameObject.GetComponent<Player>());
			Destroy(gameObject);
		}
	}
}
