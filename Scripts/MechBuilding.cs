using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechBuilding : MonoBehaviour
{
	public string currentlySelected = "laserArmR";
	public GameObject leftArm;
	public string leftArmName = "swordArm";
	public GameObject rightArm;
	public string rightArmName = "swordArm";
	public GameObject body;
	public string bodyName = "chargeLaser";
	public GameObject legs;
	public string legsName = "mechLegs";
	public Camera cam;
	public GameObject shownObj;
	public string shownObjName = "swordArm";
	public TextMeshProUGUI description;
	public GameObject robotParts;

	public string swordArmText = "this is a sword arm. it like swings & stuff";
	public GameObject swordArmObj;
	public GameObject swordArmLeft;
	public GameObject swordArmRight;
	public string laserArmText = "this is a laser arm. it like shoots lasers & stuff";
	public GameObject laserArmObj;
	public GameObject laserArmLeft;
	public GameObject laserArmRight;
	public string rocketArmText = "this is a rocket arm. it like shoots rockets & stuff but needs to be actually done";
	public GameObject rocketArmObj;
	public GameObject rocketArmLeft;
	public GameObject rocketArmRight;

	public string chargeLaserText = "this is a charge laser. it like charges & lasers";
	public GameObject chargeLaserObj;
	public GameObject chargeLaserChest;
	public string rocketDashText = "this is a rocket dash. it like dashes & stuff";
	public GameObject rocketDashObj;
	public GameObject rocketDashChest;
	public string empBodyText = "this is an emp body. it like emp and stunns & stuff";
	public GameObject empBodyObj;
	public GameObject empBodyChest;

	public string mechLegsText = "these are mech legs. they like walk & stuff";
	public GameObject mechLegsObj;
	public GameObject mechLegsMovement;
	public string mechWheelText = "this is a mech wheel. it like wheels & stuff";
	public GameObject mechWheelObj;
	public GameObject mechWheelMovement;
	public string rocketLegText = "this is a rocket leg. it like rockets & stuff";
	public GameObject rocketLegObj;
	public GameObject rocketLegMovement;

	void Start()
	{
		// define all the game objects! **************************************************************
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

			if(hitObj == "RswordArmSprite")
			{
				currentlySelected = "swordArmRight";
			} 
			else if (hitObj == "RlaserArmSprite")
			{
				currentlySelected = "laserArmRight";
			} 
			else if (hitObj == "RrocketArmSprite")
			{
				currentlySelected = "rocketArmRight";
			} 
			else if (hitObj == "LswordArmSprite")
			{
				currentlySelected = "swordArmLeft";
			}
			else if (hitObj == "LlaserArmSprite")
			{
				currentlySelected = "laserArmLeft";
			}
			else if (hitObj == "LrocketArmSprite")
			{
				currentlySelected = "rocketArmLeft";
			}
			else if (hitObj == "rocketDashSprite")
			{
				currentlySelected = "rocketDashChest";
			}
			else if (hitObj == "chargeLaserSprite")
			{
				currentlySelected = "chargeLaserChest";
			}
			else if (hitObj == "empBodySprite")
			{
				currentlySelected = "empBodyChest";
			}
			else if (hitObj == "mechLegsSprite")
			{
				currentlySelected = "mechLegsMovement";
			}
			else if (hitObj == "wheelSprite")
			{
				currentlySelected = "mechWheelMovement";
			}
			else if (hitObj == "rocketLegSprite")
			{
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
			string json = JsonUtility.ToJson(save);
			string savePath = Application.persistentDataPath + "/saveFile.json";
			print("saving to: " + savePath);
			System.IO.File.WriteAllText(savePath, json);

			SceneManager.LoadScene("Demo");
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
