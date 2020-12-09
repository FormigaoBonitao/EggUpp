using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
	//visible in inspector
	public Transform player;
	public float offset;
	public float movespeed;
	public Transform cameraTransform;
	GameManager manager;
	
	//not in inspector
	Vector3 target;
	
	void Update(){
		//always move the target point to the highest player position
		//if (player.transform.position.y > target.y + offset)
  //      {
		//	target = Vector3.up * (player.transform.position.y - offset);
		//}

		

		
        //move camera towards the target point
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movespeed);


    }

    //show some screenshake based on the duration and amount parameters
    public IEnumerator Shake(float duration, float amount){
		Vector3 localPos = cameraTransform.localPosition;
		
		float elapsed = 0f;
		
		while(elapsed < duration){
			float x = Random.Range(-1f, 1f) * amount;
			float y = Random.Range(-1f, 1f) * amount;
			float z = Random.Range(-1f, 1f) * amount;
			
			cameraTransform.position += new Vector3(x, y, z);
			
			elapsed += Time.deltaTime;
			
			yield return 0;
		}
		
		cameraTransform.localPosition = localPos;
	}

	public void CheckJump()
    {
		target = Vector3.up * (player.transform.position.y);
		
		
	}
}
