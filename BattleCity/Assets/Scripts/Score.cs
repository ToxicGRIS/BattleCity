using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TimerCD))]
public class Score : MonoBehaviour
{
	#region Properties
	[SerializeField] private int currentScore;
    [SerializeField] private int enemiesLeft;
	[SerializeField] private Text scoreText;
	[SerializeField] private Text enemiesLeftText;
	[SerializeField] private GameObject textGameOver;
	[SerializeField] private GameObject[] enemyList;
	[SerializeField] private List<GameObject> enemyBases;
	[SerializeField] private bool spawnerOn;
	[SerializeField] private float delayToMenu = 5;
	[SerializeField] private AudioSource audioOver;

    private static event Action<int> PlusScore;
	private static event Action EnemyDeath;
	private static event Action GameOver;
	private TimerCD spawnCD;
	private float toMenu;
	private bool gameEnded;

	public int CurrentScore => currentScore;

	#endregion
	#region Start

	private void Awake()
	{
		PlusScore += ChangeScore;
		EnemyDeath += EnemyDies;
		GameOver += gameOver;
	}

	private void Start()
	{
		enemiesLeftText.text = $"Enemies left:\n{enemiesLeft}";
		textGameOver.SetActive(false);
		spawnCD = GetComponent<TimerCD>();
		gameEnded = false;
		toMenu = delayToMenu;
	}

	#endregion
	#region GameOver

	private void gameOver()
	{
		textGameOver.SetActive(true);
		spawnerOn = false;
		foreach (var e in FindObjectsOfType<Enemy>())
		{
			e.Activated = false;
		}
		if (currentScore > PlayerPrefs.GetInt("TanksRecord")) PlayerPrefs.SetInt("TanksRecord", currentScore);
		gameEnded = true;
		audioOver.Play();
	}

	public static void EndGame()
	{
		GameOver.Invoke();
	}

	#endregion
	#region Enemy Death

	private void EnemyDies()
	{
		enemiesLeftText.text = $"Enemies left:\n{enemiesLeft}";
		if (enemiesLeft <= 0) EndGame();
	}

	public static void EnemyDied()
	{
		EnemyDeath.Invoke();
	}

	public static void SubscribeEnemyDeath(Action action)
	{
		EnemyDeath += action;
	}

	public static void UnSubscribeEnemyDeath(Action action)
	{
		EnemyDeath -= action;
	}

	#endregion
	#region Changing score

	private void ChangeScore(int amount)
	{
		currentScore += amount;
		scoreText.text = $"Score: {currentScore}";
	}

	public static void AddScore(int amount)
	{
		PlusScore.Invoke(amount);
	}

	public static void SubscribePlusScore(Action<int> action)
	{
		PlusScore += action;
	}

	public static void UnSubscribePlusScore(Action<int> action)
	{
		PlusScore -= action;
	}

	#endregion
	#region Enemy spawner

	private void SpawnEnemy()
	{
		if (enemiesLeft > 0 && spawnCD.Ready && spawnerOn)
		{
			Instantiate(enemyList[UnityEngine.Random.Range(0, enemyList.Length)], enemyBases[UnityEngine.Random.Range(0, enemyBases.Count)].transform.position, Quaternion.identity);
			enemiesLeft--;
			enemiesLeftText.text = $"Enemies left:\n{enemiesLeft}";
			spawnCD.ActivateCD();
		}
	}

	public void AddEnemyBase(GameObject enemyBase)
	{
		enemyBases.Add(enemyBase);
	}

	#endregion

	private void Update()
	{
		SpawnEnemy();
		if (gameEnded && toMenu > 0) toMenu -= Time.deltaTime;
		else if (toMenu < 0)
			SceneManager.LoadScene("Menu");
	}
}
