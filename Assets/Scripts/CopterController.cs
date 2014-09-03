using UnityEngine;

public class CopterController : MonoBehaviour
{
	public  enum GameState
	{
		initiate,
		play,
		playerdeath,
	}
	
	public GameState currentState;
	public static CopterController CS;
	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForcel = new Vector2(0, 300);
	public Vector2 jumpForcer = new Vector2(0, 300);
	private float rightVelocity = 0.1f ;
	private float leftVelocity = -0.1f ;
	private bool directionRight = true;
	private float playerAcceleration = 0.4f;
	private float playerMinVelocity = 1.5f;
	public GameObject GameOverPanel;
	public GameObject rocks;
	public GameObject currentScore;
	public int score = 0;
	public GameObject [] AllRockPairs;
	public GameObject ChopperObject;


	// Direction enumerator


	void Start()
	{
		CS = this;
		DontDestroyOnLoad(this);
		currentState = GameState.play;

		InvokeRepeating("CreateObstacle", 1f, 1.5f);
	}


	// Update is called once per frame
	void Update ()
	{

		if (currentState == GameState.play){
		CopterMomentum (directionRight);

		//Spacebar controls direction
		if (Input.GetMouseButtonDown (0)) {
				if (directionRight == true) {
						directionRight = false;
						if (leftVelocity > -playerMinVelocity) {
								leftVelocity = -2f;
						}
				} else {
						directionRight = true;
						if (rightVelocity < playerMinVelocity) {
								rightVelocity = 2f;
						}
				}

		}

		// Die by being off screen
//		Vector2 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
//		if (screenPosition.y > Screen.height || screenPosition.y < 0) {
//						Die ();
//				}
			if (ChopperObject.rigidbody2D.position.y > 6f || ChopperObject.rigidbody2D.position.y < -6f) {
							Die ();
					}
		}
	}

	void CopterMomentum (bool copterDirection)
	{
		//This part of the script gives the impression of fake momentum of the object
		//We break down the velocity of the object into two separate components and keep them in floats.

		if (copterDirection == true)
		{
			rightVelocity = rightVelocity + playerAcceleration;
			leftVelocity = (float)leftVelocity * 0.9f;
			ChopperObject.rigidbody2D.velocity = new Vector2(0, rightVelocity);
//			Debug.Log (rightVelocity);
		}
		else
		{
			leftVelocity = leftVelocity - playerAcceleration;
			rightVelocity = (float)rightVelocity * 0.9f;
			ChopperObject.rigidbody2D.velocity = new Vector2(0, leftVelocity);
//			Debug.Log (rightVelocity);
		}
		}

	
	public void Die()
	{

//		ChopperObject.rigidbody2D.isKinematic = true;
		ChopperObject.rigidbody2D.velocity = new Vector2 (0, -20f);

		AllRockPairs = GameObject.FindGameObjectsWithTag ("RockPair");
		foreach (GameObject allRockPair in AllRockPairs)
		{
			allRockPair.rigidbody2D.velocity = new Vector2(0,0);
		}


		currentState = GameState.playerdeath;
		NGUITools.SetActive (GameOverPanel, true);
	}

	public void GameRestart()
	{
		Application.LoadLevel (Application.loadedLevel);
//		rigidbody2D.position = new Vector2 (-3.5f, 0.06f);
//		currentState = GameState.play;
//		rightVelocity = 0.1f ;
//	    leftVelocity = -0.1f ;
//		NGUITools.SetActive (GameOverPanel, false);

	}

	void CreateObstacle()
	{
		if (currentState == GameState.play) {
						Instantiate (rocks);

				}
	}

	public void IncreaseScore()
		{	
			score ++;
			DisplayScore ();
		}

	void DisplayScore()
	{
		UILabel c = currentScore.GetComponent<UILabel>();
		c.text = score.ToString();
	}


}

