using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour {
	
	//visible in inspector
	public GameObject platform;
	public Material connectionPart;
	public Material Net;
	public Material BountyPad;
	public Material Ring;
	public Material Pole;
	public Camera cameraColor;

	public Transform parent;
	public Transform pole;
	public Transform finishLine;
	
	public float yOffset;
	public int rotationMin;
	public int rotationMax;
	public float startHeight;
	public int startRotation;
	
	public int diamondChance;

	public int currentlvl;
	public int nextLvl;
	public int BoolGameOver;
	

	
	//not in inspector
	int size;
	GameManager manager;
  
    private void Start(){
		//get the total level size from the game manager
		size = FindObjectOfType<GameManager>().totalHeight;
		BoolGameOver = PlayerPrefs.GetInt("BoolGameOver");
		//build level
		MakeLevel();
		if (BoolGameOver == 1)
        {
			ChangeColor();
		}
		



	}
	
	private void MakeLevel(){
		float height = startHeight;
		float rot = startRotation;
		
		bool lastHadDiamond = false;
		
		//loop over the size of the level
		for(int i = 0; i < size; i++){
			//make new platform and position it above the last one, also rotate it
			GameObject newPlatform = Instantiate(platform);
			newPlatform.transform.position = Vector3.up * height;
			newPlatform.transform.Rotate(Vector3.up * rot);
			
			
			//parent platform so it rotates with the other platforms
			newPlatform.transform.SetParent(parent, false);
			
			//randomly show diamonds on some platforms
			bool diamond = i > 0 && !lastHadDiamond && Random.Range(0, diamondChance) == 0;
			newPlatform.GetComponent<Platform>().SetDiamond(diamond, i < size - 3 && i > 0);
			
			lastHadDiamond = diamond;
			
			//get random rotation for the next platform
			float randomRotation = Random.Range(rotationMin, rotationMax);
			
			if(i == 0)
				randomRotation = Random.Range((rotationMin + rotationMax)/2, rotationMax);
			
			bool rotateLeft = Random.Range(0, 2) == 0;
			
			//rotate either left or right
			rot += rotateLeft ? -randomRotation : randomRotation;
			
			//increase height so the next platform gets spawned above the current one
			height += yOffset;
		}
		



		//position the finish line at the top and scale the center pole
		finishLine.position = Vector3.up * height;
		finishLine.SetParent(parent, false);
		
		Vector3 poleScale = pole.localScale;
		pole.localScale = new Vector3(poleScale.x, height, poleScale.z);

		
	}

	public void ChangeColor() 
	{
		connectionPart.color = new Color(Random.value, Random.value, Random.value, 1);
		Net.color = new Color(Random.value, Random.value, Random.value, 1);
		BountyPad.color = new Color(Random.value, Random.value, Random.value, 1);
		Ring.color = new Color(Random.value, Random.value, Random.value, 1);	
		Pole.color = new Color(Random.value, Random.value, Random.value, 1);
		cameraColor.backgroundColor = new Color(Random.value, Random.value, Random.value, 1);
	}
	
}
