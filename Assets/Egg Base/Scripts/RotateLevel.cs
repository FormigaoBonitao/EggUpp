using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour {
    
	//rotation sensitivity visible in inspector
	public float sensitivity;
	
	//not in inspector
	Vector2 startPos;
	float rotation;
	
	Vector3 startRotation;
	
	void Update(){
		//get current mouse position
		Vector3 mousePos = Input.mousePosition;
		
		//get start rotation and start mouse position
		if(Input.GetMouseButtonDown(0)){
			rotation = 0;
			startRotation = transform.eulerAngles;
			startPos = mousePos;
		}
		else if(Input.GetMouseButton(0)){
			//rotate based on difference between current mouse position and start position
			rotation = startPos.x - mousePos.x;
			
			Vector3 eulerAngles = startRotation + Vector3.up * rotation * sensitivity;
			transform.rotation = Quaternion.Euler(eulerAngles);
		}
	}
}
