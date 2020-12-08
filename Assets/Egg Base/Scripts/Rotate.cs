﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    
	//visible in inspector
	public Vector3 rotation;
	public float speedRotation;
	
	void Update(){
		//rotate
		transform.Rotate(rotation * Time.deltaTime * speedRotation);
	}
}
