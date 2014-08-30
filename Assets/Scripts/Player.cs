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
	private float playerAcceleration = 0.4f;
	private float playerMinVelocity = 1.5f;


	// Update is called once per frame
	void Update ()
	{

		//Accelerator 

		if (directionRight == true)
		{
			rightVelocity = rightVelocity + playerAcceleration;
			leftVelocity = (float)leftVelocity * 0.9f;
			rigidbody2D.velocity = new Vector2(0, rightVelocity);
			Debug.Log (rightVelocity);
		}
		else
		{
			leftVelocity = leftVelocity - playerAcceleration;
			rightVelocity = (float)rightVelocity * 0.9f;
			rigidbody2D.velocity = new Vector2(0, leftVelocity);
			Debug.Log (rightVelocity);
		}


		//Spacebar controls direction
		if (Input.GetMouseButtonDown(0))
		{
			if (directionRight == true)
			{
				directionRight = false;
				if (leftVelocity > -playerMinVelocity){ leftVelocity = -2f ;}
			}
			else
			{
				directionRight = true;
				if (rightVelocity < playerMinVelocity) { rightVelocity = 2f ;}
			}

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
