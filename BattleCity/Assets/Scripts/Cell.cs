using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	#region Properties

	[SerializeField] private Vector2Int position;
	[SerializeField] private CellType type;

	public Vector2Int Position
	{
		get => position;
		set => position = value;
	}

	public CellType Type { get => type; set => type = value; }

	#endregion

	private void OnDrawGizmos()
	{
		if (type == CellType.Neutral)
			Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
		if (type == CellType.Border)
			Gizmos.color = new Color(0f, 0f, 0f, 0.2f);
		if (type == CellType.AllyBase)
			Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
		if (type == CellType.EnemyBase)
			Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
		if (type == CellType.Wall)
			Gizmos.color = new Color(0.82f, 0.41f, 0.11f, 0.2f);
		Gizmos.DrawCube(transform.position, Vector3.one); 
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
		Gizmos.DrawCube(transform.position, Vector3.one);
	}
}