using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class PlaneSelector : MonoBehaviour {

	private List<GameObject> models;

	private int selectionIndex = 0;

	private void Start () 
	{
		models = new List<GameObject>();
		foreach (Transform t in transform) 
		{
			models.Add(t.gameObject);
			t.gameObject.SetActive(false);
			
		}

		models[selectionIndex].SetActive(true);
		
	}

	private void Update()
	{
			}

	public void Select(int index)
	{
		if (index == selectionIndex)
		{
			return;
		}
		if (index < 0 || index >= models.Count)
		{
			return;
		}
		models[selectionIndex].SetActive(false);
		selectionIndex = index;
		models[selectionIndex].SetActive(true);	}



}
