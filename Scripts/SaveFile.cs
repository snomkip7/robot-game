using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
	public string leftArmType;
	public string rightArmType;
	public string bodyType;
	public string legType;
	public bool swordArmUnlocked = false;
	public bool laserArmUnlocked = false;
	public bool rocketArmUnlocked = false;
	public bool empBodyUnlocked = false;
	public bool chargeLaserUnlocked = false;
	public bool rocketDashUnlocked = false;
	public bool mechWheelUnlocked = false;
	public bool rocketLegUnlocked = false;

	public SaveFile(string left,string right, string body, string leg)
	{
		leftArmType = left;
		rightArmType = right;
		bodyType = body;
		legType = leg;
	}
}
