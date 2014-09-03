using UnityEngine;
using System.Collections;

public class MissilePractice : MonoBehaviour {

	private float speedY = 7.5f;
	private float speedX = 10f;
	private float timeTaken = 0f;
	private float targetX = 4f;
	private float targetY = 4f;
	// Use this for initialization
	void Start () {


		transform.position = new Vector2 (-4f, -4f);
	}
	
	// Update is called once per frame
	void Update () {


		if (transform.position.x < targetX) 
		{
			timeTaken = timeTaken + Time.deltaTime*40f;
			Debug.Log(timeTaken);
			transform.position = new Vector2 (-4 + 0.0003f* Mathf.Pow (timeTaken,3)- 0.0203f *Mathf.Pow(timeTaken,2) + 0.5097f * timeTaken - 0.0874f, transform.position.y);
		}

		if (transform.position.y < targetY) 
		{
			transform.position = new Vector2 (transform.position.x , transform.position.y + speedY * Time.deltaTime);
		}

	}
}
