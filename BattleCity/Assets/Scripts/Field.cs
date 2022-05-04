using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
	#region Properties

	[SerializeField] private Vector2Int size;
	[SerializeField] private GameObject wall;
	[SerializeField] private GameObject cell;
	[SerializeField] private GameObject border;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject[] cells;
	[SerializeField] [Range(0, 1)] float wallSpawnChance;

	#endregion
	#region Start

	[ContextMenu("Reset field")]
	private void Reset()
	{
		foreach (var c in FindObjectsOfType<Cell>())
		{
			DestroyImmediate(c.gameObject);
		}
		cells = new GameObject[size.x * size.y];
		for (int i = 0; i < size.x; i++)
		{
			for (int j = 0; j < size.y; j++)
			{
				cells[i + j * size.x] = Instantiate(cell, transform);
				cells[i + j * size.x].transform.position = transform.position + new Vector3(i - size.x / 2, j - size.y / 2);
				cells[i + j * size.x].GetComponent<Cell>().Position = new Vector2Int(i, j);
				if (i == 0 || j == 0 || i == size.x - 1 || j == size.y - 1)
				{
					cells[i + j * size.x].GetComponent<Cell>().Type = CellType.Border;
				}
			}
		}
	}

	private void Start()
	{
		for (int i = 0; i < size.x; i++)
		{
			for (int j = 0; j < size.y; j++)
			{
				Cell currentCell = cells[i + j * size.x].GetComponent<Cell>();
				if (currentCell.Type == CellType.Border)
				{
					Instantiate(border, cells[i + j * size.x].transform);
					cells[i + j * size.x].GetComponent<Cell>().Type = CellType.Border;
				}
				if (currentCell.Type == CellType.Wall)
				{
					Instantiate(wall, cells[i + j * size.x].transform);
					cells[i + j * size.x].GetComponent<Cell>().Type = CellType.Wall;
				}
				if (currentCell.Type == CellType.SpawnPoint)
				{
					Instantiate(player, cells[i + j * size.x].transform.position, player.transform.rotation);
					cells[i + j * size.x].GetComponent<Cell>().Type = CellType.Wall;
				}
				if (currentCell.Type == CellType.Neutral)
				{
					if (Random.Range(0f, 1f) < wallSpawnChance)
					{
						Instantiate(wall, cells[i + j * size.x].transform);
						cells[i + j * size.x].GetComponent<Cell>().Type = CellType.Wall;
					}
				}
			}
		}
	}

	#endregion
}
