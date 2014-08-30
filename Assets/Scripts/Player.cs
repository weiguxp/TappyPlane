using UnityEngine;

public class Player : MonoBehaviour
{
	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForcel = new Vector2(0, 300);
	public Vector2 jumpForcer = new Vector2(0, 300);
	private float rightVelocity = 0.1f ;
	private float leftVelocity = -0.1f ;
	private bool directionRight = true;

	// Update is called once per frame
	void Update ()
	{

		//Accelerator 

		if (directionRight == true)
		{
			rightVelocity = rightVelocity + 0.5f;
			leftVelocity = (float)leftVelocity * 0.95f;
			rigidbody2D.velocity = new Vector2(rightVelocity,0);
			Debug.Log (rightVelocity);
		}
		else
		{
			leftVelocity = leftVelocity - 0.5f;
			rightVelocity = (float)rightVelocity * 0.95f;
			rigidbody2D.velocity = new Vector2(leftVelocity,0);
			Debug.Log (rightVelocity);
		}


		// Left Movement
		if (Input.GetKeyUp("left"))
		{
			directionRight = false;
			if (leftVelocity > -2f){ leftVelocity = -2f ;}
		}


	
		// Right Movement
		if (Input.GetKeyUp("right"))
		{
			directionRight = true;
			if (rightVelocity < 2f) { rightVelocity = 2f ;}
		}

		// Die by being off screen
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0)
		{
			Die();
		}
	}
	
	// Die by collision
	void OnCollisionEnter2D(Collision2D other)
	{
		Die();
	}
	
	void Die()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
