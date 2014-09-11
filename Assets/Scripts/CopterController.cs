using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class CopterController : MonoBehaviour
{
	// Direction enumerator
	public  enum GameState
	{
		initiate,
		play,
		playerdeath,
	}
	public GameState currentState;
	public static CopterController CS;
	public GameObject GameOverPanel;
	public GameObject AudioSourceClip;
	public GameObject StartPanel;
	public GameObject rocks;
	public GameObject currentScore;
	public GameObject [] AllRockPairs;
	public GameObject ChopperObject;
	public GameObject FinalScoreLabel;
	public GameObject PlayStateScorePanel;
	public GameObject BGsoundObject;
	public AudioClip ImpactSound;
	public AudioClip ScoreSoundObject;
	public GameObject BestScoreLabel;



	private int bestScore=0;
	public int score = 0;
	private float playerAcceleration = 0.4f;
	private float playerMinVelocity = 1.5f;
	private float rightVelocity = 0.1f ;
	private float leftVelocity = -0.1f ;
	public Vector2 jumpForcel = new Vector2(0, 300);
	public Vector2 jumpForcer = new Vector2(0, 300);
	private bool directionRight = true;



	void Start()
	{

		CS = this;
//		DontDestroyOnLoad(this);
		currentState = GameState.initiate;

		//Grabs the high score from PlayerPref
		bestScore = PlayerPrefs.GetInt ("bestScore");
		Debug.Log ("BestScore:" + bestScore.ToString ());

		InvokeRepeating("CreateObstacle", 1f, 1.5f);
	}


	// Update is called once per frame
	void Update ()
	{
		if (currentState == GameState.initiate) {
						if (Input.GetMouseButtonDown (0)) {
								ChangeGameState (GameState.play);
						}
				}

		//Code for the Play state of game
		if (currentState == GameState.play){
			CopterMomentum (directionRight);

			//Spacebar controls direction
			if (Input.GetMouseButtonDown (0)) {
				if (directionRight == true) 
				{

						//rotate the object 
						ChopperObject.rigidbody2D.rotation = -20f;	
						directionRight = false;
						if (leftVelocity > -playerMinVelocity) { leftVelocity = -2f; }
				} else {
				ChopperObject.rigidbody2D.rotation = -10f;	
						directionRight = true;
						if (rightVelocity < playerMinVelocity) {rightVelocity = 2f;}
				}

			}

				// Die by being off screen
		//		Vector2 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
		//		if (screenPosition.y > Screen.height || screenPosition.y < 0) {
		//						Die ();
	//				}
				if (ChopperObject.rigidbody2D.position.y > 6f || ChopperObject.rigidbody2D.position.y < -6f) {
								ChangeGameState (GameState.playerdeath);
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

	
	public void ChangeGameState(GameState newState)
	{
		currentState = newState;

		if (newState == GameState.play) {
			NGUITools.SetActive(PlayStateScorePanel,true);
			NGUITools.SetActive(StartPanel,false);
			AudioSourceClip.SetActive(true);
				}

		if (newState == GameState.playerdeath) {
						BGsoundObject.SetActive (false);
						audio.PlayOneShot (ImpactSound);

//		ChopperObject.rigidbody2D.isKinematic = true;
						ChopperObject.rigidbody2D.velocity = new Vector2 (0, -20f);

// 		Stops all Rock Pairs	
						AllRockPairs = GameObject.FindGameObjectsWithTag ("RockPair");
						foreach (GameObject allRockPair in AllRockPairs) {
								allRockPair.rigidbody2D.velocity = new Vector2 (0, 0);
						}


//		Saves the best score
						if (score > bestScore) {
								Debug.Log ("New High Score");
								bestScore = score;
								PlayerPrefs.SetInt ("bestScore", bestScore);

						}

//Shows the Best score
						UILabel e = BestScoreLabel.GetComponent<UILabel> ();
						e.text = bestScore.ToString ();


//		Opens the Game Over panel
						NGUITools.SetActive (GameOverPanel, true);
						NGUITools.SetActive (PlayStateScorePanel, false);
						UILabel d = FinalScoreLabel.GetComponent<UILabel> ();
						d.text = score.ToString ();
				}
	}

	public void GameRestart()
	{
		Application.LoadLevel(0);
//		Application.LoadLevel (Application.loadedLevel);
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
		audio.PlayOneShot (ScoreSoundObject);
		}

	void DisplayScore()
	{
		UILabel c = currentScore.GetComponent<UILabel>();
		c.text = score.ToString();
	}


}

