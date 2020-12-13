using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	//visible in inspector
	public Rigidbody rb;
	public float jumpForce;
	public float bouncePadJumpForce;
	public Transform mesh;
	public float meshEffect;
	public float meshEffectSpeed;
	public float meshEffectLimit;
	public float eggHeight;
	public Material eggMat;
	public Animator anim;
	public float fallForce;
	public bool crash;
	public GameObject Cam;
	public bool isWork;




	//not in inspector
	GameManager manager;
	Vibration vibro;
	
	

	private void Start()
	{
		//get game manager
		manager = FindObjectOfType<GameManager>();
		anim = GetComponent<Animator>();
		
		
	}
	private void Update()
	{
		
	}
	private void FixedUpdate()
	{
		if (rb.velocity.y <= 0)
		{
			rb.AddForce(new Vector3(0, fallForce, 0), ForceMode.Impulse);
		}
		//scale the mesh transform to get the bouncy effect
		float effect = rb.velocity.y * meshEffect;
		effect = Mathf.Clamp(effect, -meshEffectLimit, meshEffectLimit);
		mesh.transform.localScale = Vector3.MoveTowards(mesh.transform.localScale, Vector3.one + new Vector3(-effect, effect, -effect), Time.deltaTime * meshEffectSpeed);

		if (gameObject.transform.position.y < 1f && manager.gameOver == true)
		{
			manager.eggFall.Stop();
			anim.SetTrigger("Broke");
			
		}

		
	}



		void OnTriggerEnter(Collider other)
		{
		//return if collider isn't a platform or if player is below collider (meaning the player should jump through the platform)
		//if(!other.gameObject.CompareTag("Platform") || transform.position.y < other.gameObject.transform.position.y)
		//	return;

		//show platform bounce effect
		
        Platform platform = other.gameObject.transform.parent.GetComponent<Platform>();
        platform.Bounce(transform.position - (Vector3.up * eggHeight), eggMat);
		// jump up
		rb.velocity = Vector3.up * (platform.hasBouncePad ? bouncePadJumpForce : jumpForce);
        manager.Jumped(transform.position);
		

		//hide the intro title after the player jumps
		if (transform.position.y > 1f)
			manager.HideTitle();

		if (!other.gameObject.CompareTag("Platform"))
	    {
			vibro.isVibration = true;

		}






	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals("BigPlatform"))
		{
			
			manager.eggCrash.Play();
			Debug.Log("eeeeeeee"); 
			
		}

		


	}


	public void CameraDown()
    {
		Cam.GetComponent<CameraController>().CameraDown();
		isWork = false;
    }


	

	



}