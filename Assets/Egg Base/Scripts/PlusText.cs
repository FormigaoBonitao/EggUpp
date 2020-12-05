using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusText : MonoBehaviour {
    
	//visible in inspector
	public float movespeed;
	public float lifetime;
	
	void Start(){
		//remove text after some time
		Destroy(gameObject, lifetime);
	}
	
	void Update(){
		//move up
		transform.Translate(Vector3.up * movespeed * Time.deltaTime, Space.World);
	}
	
	void LateUpdate(){
		//rotate towards camera
		transform.LookAt(Camera.main.transform);
	}
}
