using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMushrooms : MonoBehaviour
{
	public GameObject MushroomsAround;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void showTheMushrooms()
	{
		MushroomsAround.SetActive(true);
		
	}
		
}
