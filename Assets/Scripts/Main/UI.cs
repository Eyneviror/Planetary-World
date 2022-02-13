using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI : MonoBehaviour
{
	[SerializeField] private GameObject menuUI;
	[SerializeField] private GameObject gameUI;
	[SerializeField] private GameObject gameOverUI;

	//Game UI
	[SerializeField] private Text timeElapsed;
	[SerializeField] private Slider planetHealthBar;

	//Game Over UI
	[SerializeField] private Text goDefendTime;
	[SerializeField] private Text goHighscoreTime;

	[SerializeField] private Image fillArea;

	public event Action OnPlay;

	public void Init (float planetHeath)
	{
		planetHealthBar.maxValue = planetHeath;
		planetHealthBar.value = planetHeath;
		SetMenuUI(true);
		SetGameUI(false);

	}

	public void OnPlayButton ()
	{
		OnPlay?.Invoke();
		menuUI.SetActive(false);
	}
	public void OnQuitButton()
	{
		Application.Quit();
	}

	public void OnMenuButton ()
	{
		Application.LoadLevel(0);
	}

	public void SetMenuUI (bool unlock)
	{
		menuUI.SetActive(unlock);
	}

	public void SetGameUI (bool unlock)
	{
		gameUI.SetActive(unlock);
	}
	public void SetGameOverUI (float timeHightScore,float gameTime)
	{
		gameOverUI.SetActive(true);
		gameUI.SetActive(false);
		goDefendTime.text = "You defended the planet for...\n<size=50>" + GetTimeAsString(gameTime) + "</size>  minutes";

		if(timeHightScore == 0)
		{
			goHighscoreTime.text = "Your highscore is...\n<size=50>" + GetTimeAsString(gameTime) + "</size>  minutes";
		}
		else
		{
			goHighscoreTime.text = "Your highscore is...\n<size=50>" + GetTimeAsString(timeHightScore) + "</size>  minutes";
		}
	}

	public void SetPlanetHealthBarValue (int value)
	{
		planetHealthBar.value = value;
		StartCoroutine(PlanetHealthBarFlash());
	}


	public void SetTimeElapsed (float gameTime)
	{
		timeElapsed.text = "TIME ELAPSED\n<size=55>" + GetTimeAsString(gameTime) + "</size>";
	}
	private IEnumerator PlanetHealthBarFlash ()
	{

		if(fillArea.color != Color.red)
		{
			Color dc = fillArea.color;
			fillArea.color = Color.red;

			yield return new WaitForSeconds(0.05f);

			fillArea.color = dc;
		}
	}
	private string GetTimeAsString (float t)
	{
		string mins = Mathf.FloorToInt(t / 60).ToString();

		if(int.Parse(mins) < 10)
			mins = "0" + mins;

		string secs = ((int)(t % 60)).ToString();

		if(int.Parse(secs) < 10)
			secs = "0" + secs;

		return mins + ":" + secs;
	}
}
