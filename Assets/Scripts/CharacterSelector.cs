using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelector : MonoBehaviour
{

	private GameObject[] characterList;
	private int index;

	public Transform shotspawn1;
	public GameObject cleaner;
	public GameObject cleanerButton;


	private void Start()
	{
		index = PlayerPrefs.GetInt("CharacterSelected");

		characterList = new GameObject[transform.childCount];

		//fill the array with our models
		for (int i = 0; i < transform.childCount; i++)
		{
			characterList[i] = transform.GetChild(i).gameObject;
		}
		//we toggle off their renderer
		foreach (GameObject go in characterList)
		{
			go.SetActive(false);
		}
		//toggle the selected

		if (characterList[index])
		{
			characterList[index].SetActive(true);
		}

	}

	public void ToggleLeft()
	{

		//Toggle off the current model
		characterList[index].SetActive(false);
		index--; // index-=1;index = index-1;
		if (index < 0)
			index = characterList.Length - 1;

		//Toggle off the new model

		characterList[index].SetActive(true);
	}

	public void ToggleRight()
	{

		//Toggle off the current model

		characterList[index].SetActive(false);

		index++; // index-=1;index = index-1;

		if (index == characterList.Length)
			index = 0;

		//Toggle off the new model

		characterList[index].SetActive(true);	}

	public void ConfirmButton(int sceneIndex)
	{
		PlayerPrefs.SetInt("CharacterSelected", index);
		SceneManager.LoadScene(sceneIndex);
	}


	public void FireCleaner()
	{
		Vector3 cVector = Vector3.zero;
		cVector.z = shotspawn1.position.z;
		Instantiate(cleaner, cVector, shotspawn1.rotation);
		cleanerButton.SetActive(false);	}
}
