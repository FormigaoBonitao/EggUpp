using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour {
    
	//visible in inspector
	public Animator title;
	public Text diamondCount;
	
	public Transform finishLine;
	public Transform player;
	
	public GameObject progressIndicator;
	public GameObject holdToRotate;
	public Image progressBar;
	public Image backgroundOverlay;
	public ParticleSystem backgroundParticles;
	
	public float backgroundAlphaSpeed;
	
	public ParticleSystem confetti;
	
	public Image leftCircle;
	public Image rightCircle;
	public Text leftText;
	public Text rightText;
	public GameObject rightBackground;
	
	public GameObject plusEffect;
	
	public Animator levelDoneText;
	
	public AudioSource jumpAudio;
	public AudioSource victory;
	public AudioSource eggFall;
	public AudioSource eggCrash;
	public AudioSource solut;
	

	public GameObject tapToRestart;
	public GameObject tapToContinue;

	public GameObject playerParent;
	public GameObject cameraParent;
	public Transform firstPlatform;

	private int playerObject, collideObject;

	public GameObject BrokePlatform;
	
	public GameObject BigPlatform;
	

	

	LevelGenerator generator;
	CameraController cameraScript;





	Player playerJump;
	
	
	//not in inspector
	bool titleHidden;
	
	public bool gameOver;
	
	float backgroundAlpha;
	
	[HideInInspector]
	public int totalHeight;
	
	int currentHeight;
	 public float lastHeight;
	
	public int checkLvl;
	void Awake(){
		//get the level index and set the size of this level accordingly
	
		int level = PlayerPrefs.GetInt("Level");
		totalHeight = 25;
		
		//update the level labels at the bottom of the screen
		leftText.text = (level + 1) + "";
		rightText.text = (level + 2) + "";

	}
	
	void Start(){
		//update the diamond label without adding any diamonds
		AddDiamonds(0, false);
		
		//hide some UI
		tapToRestart.SetActive(false);
		tapToContinue.SetActive(false);
		playerObject = LayerMask.NameToLayer("Player");
		collideObject = LayerMask.NameToLayer("Platform");
		playerParent.GetComponent<SphereCollider>().isTrigger = true;
		Physics.IgnoreLayerCollision(playerObject, collideObject, false);



	}
	
	void Update(){
		
		//check if the game is still going
		if(!gameOver){
			
			//if the player crosses the finish line, he wins
			if(player.position.y > finishLine.position.y){
				gameOver = true;

				WinScreen();
			
				GameOver(true);
				
			}
			
			//if the player falls down too far, the player loses
			if(player.position.y < lastHeight - 4.5f){
				gameOver = true;			
				GameOver(false);
				
			}

		}
		else if(Input.GetMouseButtonDown(0)){
			//if game is over and player taps, reload the current scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			
		}
		
		//fade background transparency
		if(backgroundOverlay.color.a < backgroundAlpha)
			backgroundOverlay.color = new Color(backgroundOverlay.color.r, 
												backgroundOverlay.color.g, 
												backgroundOverlay.color.b, 
												backgroundOverlay.color.a + (backgroundAlphaSpeed * Time.deltaTime));
	}
	
	//deactivate the player
	void DisablePlayer(){
		player.gameObject.SetActive(false);
	}
	
	//called when player just jumped
	public void Jumped(Vector3 playerPos){
		//if the player didn't get to the next platform, reset audio pitch and return
		if(playerPos.y < lastHeight + 0.2f){
			jumpAudio.pitch = 1f;
			jumpAudio.Play();
			

			return;


		}
		
		
		//increase pitch for rewarding sound effect
		jumpAudio.pitch *= 1.02f;
		jumpAudio.Play();
		
		currentHeight++;
		
		lastHeight = playerPos.y;
		

		//update progress UI
		UpdateProgress();
		
		//show the +1 effect
		Instantiate(plusEffect, playerPos + Vector3.right * 0.7f, plusEffect.transform.rotation);

		

	}
	
	void UpdateProgress()
	{
		//get current progress and update progress bar, background, and particles
		float percentage = (float)currentHeight/(float)totalHeight;
		
		backgroundAlpha = percentage * 0.7f;
		progressBar.fillAmount = percentage;
		
		var emission = backgroundParticles.emission;
        emission.rateOverTime = percentage * 4;
	}
	
	//hide title UI and show progress UI
	public void HideTitle(){
		if(titleHidden)
			return;
		
		titleHidden = true;
		
		title.SetTrigger("Hide");
		
		progressIndicator.SetActive(true);
		holdToRotate.SetActive(false);
	}
	
	//add diamonds and update the playerprefs as well as the diamond UI
	public void AddDiamonds(int count, bool showEmoji){
		int diamonds = PlayerPrefs.GetInt("Diamonds");
		diamonds += count;
		
		PlayerPrefs.SetInt("Diamonds", diamonds);
		
		diamondCount.text = "" + diamonds;
	}
	
	//called when game ends
	public void GameOver(bool success){
		//hide progress indicator
		progressIndicator.SetActive(false);
		
		if(success)
		{
			//play confetti and continue to next level
			confetti.Play();
			solut.Play();

			rightCircle.color = leftCircle.color;
			rightBackground.SetActive(true);
			
			levelDoneText.SetTrigger("Show");
			
			victory.Play();
			
			PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
			
			

			tapToContinue.SetActive(true);
		}
		else{
			//show the restart text
			tapToRestart.SetActive(true);
			holdToRotate.SetActive(false);
			AddGameOver();


		}
    }

	public void AddGameOver()
    {
		cameraParent.GetComponent<CameraController>().enabled = false;
		cameraParent.transform.parent = playerParent.transform;
		Physics.IgnoreLayerCollision(playerObject, collideObject, true);
		playerParent.GetComponent<SphereCollider>().isTrigger = false;
		BrokePlatform.SetActive(true);
		PlayerPrefs.SetInt("BoolGameOver", 0);

		


		if (playerParent.GetComponent<Rigidbody>().velocity.y < 0)
        {
			eggFall.Play();
		}


	

		BigPlatform.GetComponent<Platform>().enabled = false;
	
	}

	public void WinScreen()
    {
		playerParent.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 1), ForceMode.Impulse);
		//playerParent.transform.Translate(new Vector3(0,  3, 1.5f));
		playerParent.GetComponent<SphereCollider>().isTrigger = false;
		playerParent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		playerParent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;	
		PlayerPrefs.SetInt("BoolGameOver", 1);
		


		if (playerParent.GetComponent<Rigidbody>().velocity.y > 0)
        {
			Physics.IgnoreLayerCollision(playerObject, collideObject, true);
			
		}
	}

	


}
