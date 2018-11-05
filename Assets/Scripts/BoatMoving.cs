using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class BoatMoving : MonoBehaviour {

	public Transform target;
	public PathType pathType = PathType.CatmullRom;
	public Transform[] waypoints;
	private Vector3[] points;
	public bool isBoatMoving;
	public Transform playerPosition;
	public GameObject player;
	public NPC_Boatman boatMan;
	public PlayerManagement playerManagement;
	public PlayerInteraction playerInteraction;
	public bool isPlayerOnBoat = false;
	public bool isCameraFade = false;
	void Start()
	{
		points = new Vector3[waypoints.Length];
		for (int i = 0; i < waypoints.Length; i++)
		{
			points[i] = waypoints[i].position;
		}
		
	}

	public void MoveBoat()
	{
		// Create a path tween using the given pathType, Linear or CatmullRom (curved).
		// Use SetOptions to close the path
		// and SetLookAt to make the target orient to the path itself
		if (!isBoatMoving)
		{
			isBoatMoving = true;
			Tween rotate = target.DOShakeRotation(5, 1.5f, 2, 120f, false);
			rotate.SetLoops(-1);
			Tween t = target.DOPath(points, 28, pathType)
				.SetOptions(false)
				.SetLookAt(1f);
			// Then set the ease to InOutQuad and use infinite loops
			t.SetEase(Ease.InOutQuad);
			StartCoroutine(AddFogDensity());
		}
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (playerManagement.isMoveBoat)
		{
			if (other.gameObject.tag.Equals("Player"))
			{
				gameObject.GetComponent<BoxCollider>().enabled = false;
				isPlayerOnBoat = true;
				player.GetComponent<CharacterController>().enabled = false;
				player.GetComponent<FirstPersonController>().SetHeadHob(false);
				boatMan.BoatmanMove();
			}
		}
	}
	IEnumerator AddFogDensity()
	{		
		yield return new WaitForSeconds(10f);
		isCameraFade = true;
		playerInteraction.fadeSpeed = 0.5f;
		playerInteraction.startFadeOut = true;

	}
	private void Update()
	{
		if (isPlayerOnBoat)
		{
			player.transform.position = playerPosition.position;
			
		}
		if (isCameraFade)
		{
			RenderSettings.fogDensity += 0.00009f;
			
		}

		if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].position) < 0.2)
		{
			SceneManager.LoadScene(1);
		}
	}
}
