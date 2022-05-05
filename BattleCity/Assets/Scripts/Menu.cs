using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	#region Properties

	[SerializeField] private AudioMixerGroup master;
	[SerializeField] private Toggle muteToggle;
	[SerializeField] private Text record;

	private float volume;

	#endregion
	#region Start

	private void Start()
	{
		master.audioMixer.GetFloat("MasterVolume", out volume);
		muteToggle.isOn = volume < -79f;
		record.text = $"RECORD: {PlayerPrefs.GetInt("TanksRecord")}";
	}

	#endregion

	public void StartGame()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void ToogleSound()
	{
		if (!muteToggle.isOn)
			master.audioMixer.SetFloat("MasterVolume", 0);
		else
			master.audioMixer.SetFloat("MasterVolume", -80);
	}
}
