using UnityEngine;
using System;
using System.Collections;


public class Obstacle : MonoBehaviour
{
	public Vector2 velocity = new Vector2(-4, 0);
	public float range = 4;
	
	// Use this for initialization
	void Start()
	{
		rigidbody2D.velocity = velocity;
		transform.position = new Vector3(transform.position.x, transform.position.y - range * UnityEngine.Random.value, transform.position.z);

		Destroy(gameObject,5);

	}


}