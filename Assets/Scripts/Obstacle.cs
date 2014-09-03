using UnityEngine;
using System;
using System.Collections;


public class Obstacle : MonoBehaviour
{
	public Vector2 velocity = new Vector2(-4, 0);
	public float range = 4;
	public bool obstaclePassed = false;
	public static Obstacle OSC;
	
	// Use this for initialization
	void Start()
	{
		OSC = this;
		rigidbody2D.velocity = velocity;
		transform.position = new Vector3(transform.position.x, transform.position.y - range * UnityEngine.Random.value, transform.position.z);

	}

	void Update()
	{
		if (obstaclePassed == false) 
		{
			if (rigidbody2D.position.x < -15.63)
			{
//				Debug.Log(rigidbody2D.position.x);
				obstaclePassed = true;
				CopterController.CS.score ++;

			}
		}

		if (obstaclePassed == true) 
		{
			if (rigidbody2D.position.x < -30.63)
			{
				Destroy (gameObject);
				
			}
		}

	}

	public void StopRockPair()
	{ 
		rigidbody2D.velocity = new Vector2 (0, 0);
	}

}