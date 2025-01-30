using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;
using static UnityEditor.PlayerSettings.Switch;

public class ItemBox : Enemy
{
	public string type = "punch";
	public bool opened = false;

	public void Start()
	{
		string savePath = Application.persistentDataPath + "/saveFile.json";
		if (System.IO.File.Exists(savePath))
		{
			string json = System.IO.File.ReadAllText(savePath);
			SaveFile save = JsonUtility.FromJson<SaveFile>(json);
			if (type == "emp" && save.laserArmUnlocked)
			{
				opened = true;
			}
			else if (type == "punch" && save.swordArmUnlocked)
			{
				opened = true;
			}
			else if (type == "sword" && save.empBodyUnlocked)
			{
				opened = true;
			}
			else if (type == "charge" && save.rocketArmUnlocked)
			{
				opened = true;
			}
			else if (type == "laser" && save.chargeLaserUnlocked)
			{
				opened = true;
			}
		}
	}

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
				save.rocketDashUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}

			player.messageBox.SetActive(true);
			player.messageText.SetActive(true);
			player.cursorLocked = false;
			Cursor.lockState = CursorLockMode.None;
			player.pauseScreen.SetActive(true);
			player.hint.SetActive(false);
			player.messageText.GetComponent<TextMeshProUGUI>().text = "You opened the Storage Box and found some neodymium-doped Yttrium aluminum garnet. It is very useful for making lasers and you create one. You also found a small amount of rocket fuel and decide to create a low fuel rocket. Consider finding a shortcut back to the ship.  \nPress [escape] to exit this screen";
			Time.timeScale = 0;
		}
	}

	public override void takeDmg(float amount)
	{
		if (opened)
		{
			return;
		}
		if (type == "punch" && amount == 15)
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

			player.messageBox.SetActive(true);
			player.messageText.SetActive(true);
			player.cursorLocked = false;
			Cursor.lockState = CursorLockMode.None;
			player.pauseScreen.SetActive(true);
			player.hint.SetActive(false);
			player.messageText.GetComponent<TextMeshProUGUI>().text = "You opened the Storage Box and found some aerospace-grade titanium. You fashion a blade out of the titanium and use lasers to heat it up. It will be useful for cutting things. You also find some carbon fiber and make a wheel out of it. You should return to your ship to try them. You fashion a blade out of the titanium and use lasers to heat it up. It will be useful for cutting things, especially other robots made of less heat-resistant materials. You should return to your ship to try it.  \nPress [escape] to exit this screen";
			Time.timeScale = 0;

		}
		else if (type == "sword" && amount == 25)
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

			player.messageBox.SetActive(true);
			player.messageText.SetActive(true);
			player.cursorLocked = false;
			Cursor.lockState = CursorLockMode.None;
			player.pauseScreen.SetActive(true);
			player.hint.SetActive(false);
			player.messageText.GetComponent<TextMeshProUGUI>().text = "You opened the Storage Box and found strong magnets. You decide to create an EMP from them. It will be useful for stunning robots & frying circuits. You should return to your ship to try it, then continue down this way.  \nPress [escape] to exit this screen";
			Time.timeScale = 0;

		}
		else if (type == "laser" && amount == 10)
		{
			//unlock smth
			Player player = GameObject.Find("Player").GetComponent<Player>();
			if (player.hint.activeInHierarchy == false)
			{
				return;
			}
			print("unlocked!");
			opened = true;


			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.chargeLaserUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}

			player.messageBox.SetActive(true);
			player.messageText.SetActive(true);
			player.cursorLocked = false;
			Cursor.lockState = CursorLockMode.None;
			player.pauseScreen.SetActive(true);
			player.hint.SetActive(false);
			player.messageText.GetComponent<TextMeshProUGUI>().text = "You opened the Storage Box and found some neodymium glass. It is useful in creating lasers that can bore through metal. Due to recent technological advancements, it can even power up a certain type of battery. Consider finding a shortcut back to the ship.  \nPress [escape] to exit this screen";
			Time.timeScale = 0;
		}
		else if (type == "rocket" && (amount == 22 || amount == 12))
		{
			//unlock smth
			Player player = GameObject.Find("Player").GetComponent<Player>();
			if (player.hint.activeInHierarchy == false)
			{
				return;
			}
			print("unlocked!");
			opened = true;
			player.fixedShip = true;
			player.messageBox.SetActive(true);
			player.messageText.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			player.cursorLocked = false;
			player.pauseScreen.SetActive(true);
			player.hint.SetActive(false);
			player.messageText.GetComponent<TextMeshProUGUI>().text = "You opened the Storage Box and found the last things you need for your ship! Now you can return home to the Utopia back at home. Continue exploring or go back to your ship and go home.  \nPress [escape] to exit this screen";
			Time.timeScale = 0;

		}
		else if ((type == "charge") && !(amount == 22 || amount == 12 || amount == 10 || amount == 25 || amount == 15))
		{
			Player player = GameObject.Find("Player").GetComponent<Player>();
			if(player.hint.activeInHierarchy == false)
			{
				return;
			}
			//unlock smth
			print("rockets unlocked!");
			opened = true;

			

			string savePath = Application.persistentDataPath + "/saveFile.json";
			if (System.IO.File.Exists(savePath))
			{
				string json = System.IO.File.ReadAllText(savePath);
				SaveFile save = JsonUtility.FromJson<SaveFile>(json);
				save.rocketArmUnlocked = true;
				save.rocketLegUnlocked = true;
				json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
			}

			player.messageBox.SetActive(true);
			player.messageText.SetActive(true);
			player.cursorLocked = false;
			player.pauseScreen.SetActive(true);
			player.hint.SetActive(false);
			Cursor.lockState = CursorLockMode.None;
			player.messageText.GetComponent<TextMeshProUGUI>().text = "You opened the Storage Box and found a lot of rocket fuel. You decide to create a large rocket that can travel sideways, up, and even downwards. You also find a LIDAR sensor. You should return back to your ship to equip the rockets and start searching for the last parts for your ship.  \nPress [escape] to exit this screen";
			Time.timeScale = 0;
		}
	}

}
