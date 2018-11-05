using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
	public PlayerInteraction playerInteraction;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.tag.Equals("Player"))
		{
			print("You Dead");
			playerInteraction.isDeathEffect = true;
			playerInteraction.deathCollider = transform;
		}
		
	}
}
