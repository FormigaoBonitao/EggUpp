using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Platform : MonoBehaviour {
    
	//visible in inspector
	public Animator bounceAnim;
	public Animator circle;
	public Image circleImage;
	public ParticleSystem shellParticles;
	
	public GameObject diamondHolder;
	public ParticleSystem diamondParticles;
	
	public Renderer[] renderers;
	public GameObject bouncePad;
	
	public int bouncePadChance;
	
	public AudioSource bouncePadSound;
	public AudioSource diamondSound;
	
	//not visible in inspector
	bool diamond;
	
	[HideInInspector]
	public bool hasBouncePad;
	
	GameManager manager;
	
	void Start(){
		//get game manager
		manager = FindObjectOfType<GameManager>();
	}
	
	//when player hits this platform, it bounces
	public void Bounce(Vector3 particlePosition, Material mat){
		
		//play animation and show circle effect
		bounceAnim.SetTrigger("Bounce");
		circle.SetTrigger("Play");
		
		if(hasBouncePad)
			bouncePadSound.Play();
		
		//play particles that look like the egg shell
		shellParticles.transform.position = particlePosition;
		shellParticles.Play();
		
		//get diamond if there is one
		if(diamond){
			PickupDiamond();
			
			diamond = false;
		}
		
		//change platform color
		SetMaterials(mat);
	}
	
	//change platform colors to egg color
	void SetMaterials(Material mat){
		foreach(Renderer rend in renderers){
			rend.material = mat;
		}
	}
	
	//assign the diamond and bounce pad
	//if canHaveBouncePad holds, there will be a random chance that it appears on this platform
	public void SetDiamond(bool hasDiamond, bool canHaveBouncePad){
		diamond = hasDiamond;
		
		bouncePad.SetActive(false);
		
		if(!hasDiamond){
			diamondHolder.SetActive(false);
			
			if(Random.Range(0, bouncePadChance) == 0 && canHaveBouncePad){
				hasBouncePad = true;
				bouncePad.SetActive(true);
				
				circleImage.color = Color.red;
			}
		}
	}
	
	//play sound and add 1 diamond
	void PickupDiamond(){
		diamondSound.Play();
		
		diamondHolder.SetActive(false);
		
		diamondParticles.Play();
		
		manager.AddDiamonds(1, true);
	}
}
