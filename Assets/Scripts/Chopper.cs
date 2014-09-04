using UnityEngine;
using System.Collections;

public class Chopper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		// Tell the controller that user is dead
		CopterController.CS.Die();
	}
}
