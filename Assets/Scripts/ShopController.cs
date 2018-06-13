using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{

	private int moneyAmount;
	private int isPlane1Sold;
	private int isPlane2Sold;
	private int isPlane3Sold;

	public Text moneyAmountText;
	public Text plane1Price;
	public Text plane2Price;
	public Text plane3Price;

	public Button buyButton1;
	public Button buyButton2;
	public Button buyButton3;
	public Button selectBtn;

	void Start()
	{
		moneyAmount = PlayerPrefs.GetInt("TotalCoin");
		selectBtn.gameObject.SetActive(false);
	}

	void Update()
	{
		moneyAmountText.text = "COIN: " + moneyAmount.ToString() + "$";

		isPlane1Sold = PlayerPrefs.GetInt("IsPlane1Sold");
		if (moneyAmount >= 10 && isPlane1Sold == 0)
		{
			buyButton1.gameObject.SetActive(true);
		}
		else
			buyButton1.gameObject.SetActive(false);

		isPlane2Sold = PlayerPrefs.GetInt("IsPlane2Sold");
		if (moneyAmount >= 20 && isPlane2Sold == 0)
		{
			buyButton2.gameObject.SetActive(true);
		}
		else
			buyButton2.gameObject.SetActive(false);

		isPlane3Sold = PlayerPrefs.GetInt("IsPlane3Sold");
		if (moneyAmount >= 30 && isPlane3Sold == 0)
		{
			buyButton3.gameObject.SetActive(true);
		}
		else
			buyButton3.gameObject.SetActive(false);
	}

	public void buyPlane1()
	{
		
		moneyAmount -= 10;
		PlayerPrefs.SetInt("IsPlane1Sold", 1);
		plane1Price.text = "SOLD!!";
		buyButton1.gameObject.SetActive(false);
		selectBtn.gameObject.SetActive(true);
		PlayerPrefs.SetInt("TotalCoin", moneyAmount);
	}

	public void buyPlane2()
	{
		moneyAmount -= 20;
		PlayerPrefs.SetInt("IsPlane2Sold", 1);
		plane2Price.text = "SOLD!!";
		buyButton2.gameObject.SetActive(false);
		selectBtn.gameObject.SetActive(true);
		PlayerPrefs.SetInt("TotalCoin", moneyAmount);	}

	public void buyPlane3()
	{
		moneyAmount -= 30;
		PlayerPrefs.SetInt("IsPlane3Sold", 1);
		plane3Price.text = "SOLD!!";
		buyButton3.gameObject.SetActive(false);
		selectBtn.gameObject.SetActive(true);
		PlayerPrefs.SetInt("TotalCoin", moneyAmount);	}

	public void exitShop()
	{
		PlayerPrefs.SetInt("TotalCoin", moneyAmount);
		SceneManager.LoadScene("Home");
	}

	public void ResetPlayerPrefs()
	{
		//moneyAmount = 2000;
		//PlayerPrefs.SetInt("TotalCoin", moneyAmount);
		buyButton1.gameObject.SetActive(true);
		buyButton2.gameObject.SetActive(true);
		buyButton3.gameObject.SetActive(true);
		selectBtn.gameObject.SetActive(true);
		//plane1Price.text = "PRICE: 5$";
		PlayerPrefs.SetInt("IsPlane1Sold", 0);
		PlayerPrefs.SetInt("IsPlane2Sold", 0);
		PlayerPrefs.SetInt("IsPlane3Sold", 0);

	}

}
