using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{

	public Text highestScore;
	public Text totalCoins;

	void Start()
	{

		if (highestScore != null)
		{
			
			highestScore.text = "Best Score : " + PlayerPrefs.GetInt("BestScore");
		}

		if (totalCoins != null)
		{
			totalCoins.text = "Total Coin : " + PlayerPrefs.GetInt("TotalCoin");
		}

		if (PlayerPrefs.GetString("GameMode") == "")
		{
			PlayerPrefs.SetString("GameMode", "NormalMode");
		}
	}

	public void StartGame()
	{

		SceneManager.LoadScene("GameTest");
	}
	public void LevelSelector()
	{

		SceneManager.LoadScene("LevelSelect");	}

	public void StartLevel1()
	{

		SceneManager.LoadScene("Level1");	}

	public void StartLevel2()
	{

		SceneManager.LoadScene("Level2");	}



	public void QuitGame()
	{
#if UNITY_STANDALONE
		//Quit the application
		Application.Quit(); 
#endif

		if (Application.platform == RuntimePlatform.Android)
		{
			Application.Quit();
		}

		//If we are running in the editor
#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
