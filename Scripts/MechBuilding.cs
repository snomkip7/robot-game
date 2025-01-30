using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TreeEditor.TreeEditorHelper;
using static UnityEditor.PlayerSettings.Switch;

public class MechBuilding : MonoBehaviour
{
	public string currentlySelected = "boxingArmRight";
	public GameObject leftArm;
	public string leftArmName = "boxingArm";
	public GameObject rightArm;
	public string rightArmName = "boxingArm";
	public GameObject body;
	public string bodyName = "chargeLaser";
	public GameObject legs;
	public string legsName = "mechLegs";
	public Camera cam;
	public GameObject shownObj;
	public string shownObjName = "swordArm";
	public TextMeshProUGUI description;
	public GameObject robotParts;

	public string boxingArmText = "On the surface it seems like a regular boxing glove, in reality it has a larger range than it would seem. It uses strong magnets to extend the boxing glove at very high speeds. ";
	public GameObject boxingArmObj;
	public GameObject boxingArmLeft;
	public GameObject boxingArmRight;
	public string swordArmText = "A titanium blade heated with a laser to 1000F, hot enough to melt through robots made of 2024 aluminum alloy which melts at 930F. Both materials have been used in aerospace engineering for around 200 years. ";
	public GameObject swordArmObj;
	public GameObject swordArmLeft;
	public GameObject swordArmRight;
	public string laserArmText = "A laser made with neodymium-doped Yttrium aluminum garnet crystal. This crystal has been used for cutting metal in the past but recent technological improvements have improved its strength.";
	public GameObject laserArmObj;
	public GameObject laserArmLeft;
	public GameObject laserArmRight;
	public string rocketArmText = "This arm fires a rocket that homes in on hostile robots. It uses LIDAR (Light Detection and Ranging) to detect enemies and uses the collected data to chart a path towards the enemy. ";
	public GameObject rocketArmObj;
	public GameObject rocketArmLeft;
	public GameObject rocketArmRight;

	public string genericBodyText = "A simple body made with 2024 aluminum alloy, used in aerospace engineering since 1931, and built to withstand minor collisions. ";
	public GameObject genericBodyObj;
	public GameObject genericBodyChest;
	public string chargeLaserText = "A body containing a strong, large laser made of neodymium glass. It favors high energy but low repetition and has been used to bore through metal. This leads to its powerful strength but long cooldown.";
	public GameObject chargeLaserObj;
	public GameObject chargeLaserChest;
	public string rocketDashText = "A body consisting of a strong but low fueled rocket. It allows for quick movement but can’t be sustained for long periods of time. It is fueled by liquid oxygen and methane, similar to older rockets like the Vulcan Centaur.";
	public GameObject rocketDashObj;
	public GameObject rocketDashChest;
	public string empBodyText = "A body consisting of an EMP wave generator, specifically an explosively pumped flux compression generator which uses explosives to compress a magnetic flux. It has a long cooldown as each use requires remaking the EMP.";
	public GameObject empBodyObj;
	public GameObject empBodyChest;

	public string mechLegsText = "A pair of mechanical legs that allow for simple walking and can be turned easily. They are slow but easily controllable and are the product of over a hundred years of innovation by companies like Boston Dynamics.";
	public GameObject mechLegsObj;
	public GameObject mechLegsMovement;
	public string mechWheelText = "A mechanical wheel that can achieve relatively high speeds, though struggles going uphill and with turning. It is made of carbon fiber, similar to early rovers used on mars such as perseverance.";
	public GameObject mechWheelObj;
	public GameObject mechWheelMovement;
	public string rocketLegText = "A stronger rocket capable of achieving high speeds. It also uses liquid oxygen and methane though it requires a lot more. Recent technological advancements have allowed for it to move in multiple directions.";
	public GameObject rocketLegObj;
	public GameObject rocketLegMovement;

	void Start()
	{
		// define all the game objects! **************************************************************
		string savePath = Application.persistentDataPath + "/saveFile.json";
		if (System.IO.File.Exists(savePath))
		{
			string json = System.IO.File.ReadAllText(savePath);
			SaveFile save = JsonUtility.FromJson<SaveFile>(json);
			if (save.swordArmUnlocked)
			{
				GameObject.Find("RswordArmLocked").SetActive(false);
				GameObject.Find("LswordArmLocked").SetActive(false);
			}
			if (save.laserArmUnlocked)
			{
				GameObject.Find("RlaserArmLocked").SetActive(false);
				GameObject.Find("LlaserArmLocked").SetActive(false);
			}
			if (save.rocketArmUnlocked)
			{
				GameObject.Find("RrocketArmLocked").SetActive(false);
				GameObject.Find("LrocketArmLocked").SetActive(false);
			}
			if (save.empBodyUnlocked)
			{
				GameObject.Find("empBodyLocked").SetActive(false);
			}
			if (save.chargeLaserUnlocked)
			{
				GameObject.Find("chargeLaserLocked").SetActive(false);
			}
			if (save.rocketDashUnlocked)
			{
				GameObject.Find("rocketDashLocked").SetActive(false);
			}
			if (save.mechWheelUnlocked)
			{
				GameObject.Find("mechWheelLocked").SetActive(false);
			}
			if (save.rocketLegUnlocked)
			{
				GameObject.Find("rocketLegLocked").SetActive(false);
			}
		}
	}
	
	void Update()
	{
		robotParts.transform.Rotate(Vector3.up, 360/7.5f * Time.deltaTime);

		if (!Input.GetMouseButtonDown((int)MouseButton.Left)){
			return; // ends if you aint clickin cuz i aint indentin all that :/
		}

		Vector3 mousePos = Input.mousePosition;

		Ray ray = cam.ScreenPointToRay(mousePos);
		RaycastHit hit = new();
		bool hitSmth = Physics.Raycast(ray, out hit);
		if (hitSmth)
		{
			string hitObj = hit.transform.name;

			if (hitObj == "RboxingArmSprite")
			{
				currentlySelected = "boxingArmRight";
			}
			else if(hitObj == "RswordArmSprite")
			{
				if (GameObject.Find("RswordArmLocked") != null) {
					return;
				}
				currentlySelected = "swordArmRight";
			} 
			else if (hitObj == "RlaserArmSprite")
			{
				if (GameObject.Find("RlaserArmLocked") != null)
				{
					return;
				}
				currentlySelected = "laserArmRight";
			} 
			else if (hitObj == "RrocketArmSprite")
			{
				if (GameObject.Find("RrocketArmLocked") != null)
				{
					return;
				}
				currentlySelected = "rocketArmRight";
			} 
			else if (hitObj == "LboxingArmSprite")
			{
				currentlySelected = "boxingArmLeft";
			}
			else if (hitObj == "LswordArmSprite")
			{
				if (GameObject.Find("LswordArmLocked") != null)
				{
					return;
				}
				currentlySelected = "swordArmLeft";
			}
			else if (hitObj == "LlaserArmSprite")
			{
				if (GameObject.Find("LlaserArmLocked") != null)
				{
					return;
				}
				currentlySelected = "laserArmLeft";
			}
			else if (hitObj == "LrocketArmSprite")
			{
				if (GameObject.Find("LrocketArmLocked") != null)
				{
					return;
				}
				currentlySelected = "rocketArmLeft";
			}
			else if (hitObj == "genericBodySprite")
			{
				currentlySelected = "genericBodyChest";
			}
			else if (hitObj == "rocketDashSprite")
			{
				if (GameObject.Find("rocketDashLocked") != null)
				{
					return;
				}
				currentlySelected = "rocketDashChest";
			}
			else if (hitObj == "chargeLaserSprite")
			{
				if (GameObject.Find("chargeLaserLocked") != null)
				{
					return;
				}
				currentlySelected = "chargeLaserChest";
			}
			else if (hitObj == "empBodySprite")
			{
				if (GameObject.Find("empBodyLocked") != null)
				{
					return;
				}
				currentlySelected = "empBodyChest";
			}
			else if (hitObj == "mechLegsSprite")
			{
				currentlySelected = "mechLegsMovement";
			}
			else if (hitObj == "wheelSprite")
			{
				if (GameObject.Find("mechWheelLocked") != null)
				{
					return;
				}
				currentlySelected = "mechWheelMovement";
			}
			else if (hitObj == "rocketLegSprite")
			{
				if (GameObject.Find("rocketLegLocked") != null)
				{
					return;
				}
				currentlySelected = "rocketLegMovement";
			}
			
			else if (hitObj == "ConfirmButton")
			{
				currentlySelected = "confirmButton";
			}

			updateSelected();
		}
	}

	public void updateSelected()
	{
		print("you clicked on " + currentlySelected);
		// check if its confirm button
		if(currentlySelected == "confirmButton")
		{
			// do the confirm stuff
			// save it & dont forget to load it
			SaveFile save = new(leftArmName, rightArmName, bodyName, legsName);
			if (GameObject.Find("RswordArmLocked")==null)
			{
				save.swordArmUnlocked = true;
			}
			if (GameObject.Find("RlaserArmLocked") == null)
			{
				save.laserArmUnlocked = true;
			}
			if (GameObject.Find("RrocketArmLocked") == null)
			{
				save.rocketArmUnlocked = true;
			}
			if (GameObject.Find("chargeLaserLocked") == null)
			{
				save.chargeLaserUnlocked = true;
			}
			if (GameObject.Find("empBodyLocked") == null)
			{
				save.empBodyUnlocked = true;
			}
			if (GameObject.Find("rocketDashLocked") == null)
			{
				save.rocketDashUnlocked = true;
			}
			if (GameObject.Find("mechWheelLocked") == null)
			{
				save.mechWheelUnlocked = true;
			}
			if (GameObject.Find("rocketLegLocked") == null)
			{
				save.rocketLegUnlocked = true;
			}
			string json = JsonUtility.ToJson(save);
			string savePath = Application.persistentDataPath + "/saveFile.json";
			print("saving to: " + savePath);
			System.IO.File.WriteAllText(savePath, json);

			SceneManager.LoadScene("Level");
		}
		if (currentlySelected.Contains("Left"))
		{
			leftArm.SetActive(false);
			leftArm = findPart(currentlySelected, false);
			leftArm.SetActive(true);
			leftArmName = currentlySelected.Replace("Left", "");
			shownObj.SetActive(false);
			shownObj = findPart(leftArmName, true);
			shownObj.SetActive(true);
			description.text = findString(leftArmName);
		}
		else if (currentlySelected.Contains("Right"))
		{
			rightArm.SetActive(false);
			rightArm = findPart(currentlySelected, false);
			rightArm.SetActive(true);
			rightArmName = currentlySelected.Replace("Right", "");
			shownObj.SetActive(false);
			shownObj = findPart(rightArmName, true);
			shownObj.SetActive(true);
			description.text = findString(rightArmName);
		}
		else if (currentlySelected.Contains("Chest"))
		{
			body.SetActive(false);
			body = findPart(currentlySelected, false);
			body.SetActive(true);
			bodyName = currentlySelected.Replace("Chest", "");
			shownObj.SetActive(false);
			shownObj = findPart(bodyName, true);
			shownObj.SetActive(true);
			description.text = findString(bodyName);
		}
		else if (currentlySelected.Contains("Movement"))
		{
			legs.SetActive(false);
			legs = findPart(currentlySelected, false);
			legs.SetActive(true);
			legsName = currentlySelected.Replace("Movement", "");
			shownObj.SetActive(false);
			shownObj = findPart(legsName, true);
			shownObj.SetActive(true);
			description.text = findString(legsName);
		}
	}

	public GameObject findPart(string nameOfThing, bool addObj)
	{
		if (addObj)
		{
			return (GameObject) GetType().GetField(nameOfThing+"Obj").GetValue(this);
		}
		return (GameObject) GetType().GetField(nameOfThing).GetValue(this);
	}

	public string findString(string nameOfThing)
	{
		return (string) GetType().GetField(nameOfThing+"Text").GetValue(this);
	}
}
