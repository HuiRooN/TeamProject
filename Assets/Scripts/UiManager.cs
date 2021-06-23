using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
	public Player player;
	public float playTime;
	public GameObject gamePanel;
	public GameObject SuccessPanel;
	public GameObject FailPanel;
	public Text playTimeTxt;
	public Text PlayerHPText;
	public Text CoinText;

	void Update()
	{
		playTime += Time.deltaTime;
	}

	void LateUpdate()
	{
		int hour = (int)(playTime / 3600);
		int min = (int)((playTime - hour * 3600) / 60);
		int second = (int)(playTime % 60);

		playTimeTxt.text = string.Format("{0:00}",hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
		PlayerHPText.text = player.health + " / " + player.maxHealth;
		CoinText.text = player.coin + " / " + player.maxCoin;
	}

	public void GameRestart()
    {
		SceneManager.LoadScene("MainScene");
    }

	public void GoToTitle()
    {
		SceneManager.LoadScene("TitleScene");
	}
}
