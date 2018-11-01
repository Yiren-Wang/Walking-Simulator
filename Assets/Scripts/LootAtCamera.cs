using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAtCamera : MonoBehaviour
{
	public Transform Player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vDir = Player.position - transform.position;
		vDir.Normalize();
		transform.rotation = Quaternion.LookRotation(-vDir);
		


	}
}
