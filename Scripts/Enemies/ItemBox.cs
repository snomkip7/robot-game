using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;
using static UnityEditor.PlayerSettings.Switch;

public class ItemBox : Enemy
{
	public string type = "punch";
	public bool opened = false;

	public override void stun()
	{
		if (opened)
		{
			return;
		}
		if (type == "emp")
		{
			//unlock smth
			print("laser unlocked!");
			opened = true;

			Player player = GameObject.Find("Player").GetComponent<Player>();

			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.laserArmUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}
		}
	}

	public override void takeDmg(float amount)
	{
		if (opened)
		{
			return;
		}
		if(type=="punch" && amount == 15)
		{
			print("sword unlocked!");
			print("wheels unlocked");
			opened = true;
			
			Player player = GameObject.Find("Player").GetComponent<Player>();

			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.swordArmUnlocked = true;
				save.mechWheelUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}
			
		} else if(type == "sword" && amount == 25)
		{
			print("emp unlocked!");
			opened = true;

			Player player = GameObject.Find("Player").GetComponent<Player>();

			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.empBodyUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}

		} else if(type == "laser" && amount == 10)
		{
			//unlock smth
			print("unlocked!");
			opened = true;

			Player player = GameObject.Find("Player").GetComponent<Player>();

			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.swordArmUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}
		} else if(type == "rocket" && (amount == 22 || amount == 12))
		{
			//unlock smth
			print("unlocked!");
			opened = true;
		} else if (type == "charge")
		{
			//unlock smth
			print("rockets unlocked!");
			opened = true;

			Player player = GameObject.Find("Player").GetComponent<Player>();

			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.rocketArmUnlocked = true;
				save.rocketLegUnlocked = true;
				save.rocketDashUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}
		}
	}
}
