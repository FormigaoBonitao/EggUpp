using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxLvl : MonoBehaviour
{
	public GameObject platform;	
	public Material connectionPart;
	public Material Net;
	public Material BountyPad;
	public Material Ring;
	public Material Pole;
	public Material cameraColor;
	public Material Egg;
	public Material Diamond;

	public Transform parent;
	public Transform pole;
	public Transform finishLine;

	public float yOffset;
	public int rotationMin;
	public int rotationMax;
	public float startHeight;
	public int startRotation;
	public int diamondChance;
	private int BoolGameOver;
	public int doubleChance;
	//not in inspector
	int size;
	GameManager manager;

	private void Start()
	{
		//get the total level size from the game manager
		size = FindObjectOfType<GameManager>().totalHeight;
		BoolGameOver = PlayerPrefs.GetInt("BoolGameOver");

		if ((PlayerPrefs.GetInt("Level") % 4 == 0) && (PlayerPrefs.GetInt("Level") != 0))
		{
			MakeLevel();
			if (BoolGameOver == 1)
				ChangeColor(Random.Range(0, 3));
			

		}
	}

	private void MakeLevel()
	{
		float height = startHeight;
		float rot = startRotation;

		bool lastHadDiamond = false;

		//loop over the size of the level
		for (int i = 0; i < size; i++)
		{



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
			float randomRotation = rot += 10;

			//if (i == 0)
				//randomRotation = Random.Range((rotationMin + rotationMax) / 2, rotationMax);

			//bool rotateLeft = Random.Range(0, 2) == 0;

			//rotate either left or right
			//rot += rotateLeft ? -randomRotation : randomRotation;

			//increase height so the next platform gets spawned above the current one

			height += yOffset;

			

		}




		//position the finish line at the top and scale the center pole
		finishLine.position = Vector3.up * height;
		finishLine.SetParent(parent, false);

		Vector3 poleScale = pole.localScale;
		pole.localScale = new Vector3(poleScale.x, height, poleScale.z);


	}






	public void ChangeColor(int colorSet)
	{
		if (colorSet == 0)
		{
			connectionPart.color = new Color(0.007843138f, 0.4392157f, 0.3137255f, 1);
			Net.color = new Color(0.08235294f, 0.8627451f, 0.2627451f, 1);
			BountyPad.color = new Color(1f, 0.12f, 0.25f, 1);
			Ring.color = new Color(1f, 0.1254902f, 0.2509804f, 1);
			Pole.color = new Color(0.1882353f, 1f, 0.7529412f, 1);
			cameraColor.color = new Color(0.4196078f, 0.6470588f, 0.627451f, 1);
			Egg.color = new Color(1f, 0f, 0.3764706f, 1);
			Diamond.color = new Color(0.4196078f, 0.6470588f, 0.627451f, 1);
		}
		else if (colorSet == 1)
		{
			connectionPart.color = new Color(0.4745098f, 0.2156863f, 0.3803922f, 1);
			Net.color = new Color(0.9607843f, 0.5058824f, 0.5843138f, 1);
			BountyPad.color = new Color(0.4352941f, 0.05490196f, 1f, 1);
			Ring.color = new Color(0.5333334f, 0.07843138f, 0.7647059f, 1);
			Pole.color = new Color(0.9137255f, 0.6156863f, 0.5529412f, 1);
			cameraColor.color = new Color(0.8705882f, 0.7058824f, 1f, 1);
			Egg.color = new Color(1f, 0.1607843f, 0f, 1);
			Diamond.color = new Color(0.2980392f, 1f, 0.772549f, 1);
		}
		else if (colorSet == 2)
		{
			connectionPart.color = new Color(0.7372549f, 0.572549f, 0.1647059f, 1);
			Net.color = new Color(0.9607843f, 0.6980392f, 0.5058824f, 1);
			BountyPad.color = new Color(1f, 0.7568628f, 0f, 1);
			Ring.color = new Color(0.9764706f, 0.1686275f, 0.1686275f, 1);
			Pole.color = new Color(0.9843137f, 0.9254902f, 0.6196079f, 1);
			cameraColor.color = new Color(0.3882353f, 0.6901961f, 0.5882353f, 1);
			Egg.color = new Color(0.6980392f, 0.7647059f, 0.003921569f, 1);
			Diamond.color = new Color(1f, 0.5843138f, 0f, 1);
		}
		else if (colorSet == 3)
		{
			connectionPart.color = new Color(0.7803922f, 0.6352941f, 0.9529412f, 1);
			Net.color = new Color(0.1960784f, 0.6156863f, 1f, 1);
			BountyPad.color = new Color(0.9607843f, 0.04313726f, 0.282353f, 1);
			Ring.color = new Color(0f, 0.9568627f, 1f, 1);
			Pole.color = new Color(0.282353f, 0.1137255f, 0.282353f, 1);
			cameraColor.color = new Color(0.2352941f, 0.03529412f, 0.2941177f, 1);
			Egg.color = new Color(1f, 0.572549f, 0.01568628f, 1);
			Diamond.color = new Color(1f, 0f, 0.4f, 1);
		}


	}


}
