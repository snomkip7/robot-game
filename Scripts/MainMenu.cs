using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TreeEditor.TreeEditorHelper;
using static UnityEditor.PlayerSettings.Switch;

public class MainMenu : MonoBehaviour
{
	public Camera cam;

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!Input.GetMouseButtonDown((int) MouseButton.Left))
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				string savePath = Application.persistentDataPath + "/saveFile.json";
				SaveFile save = new("boxingArm", "boxingArm", "genericBody", "mechLegs");
				string json = JsonUtility.ToJson(save);
				print("saving to: " + savePath);
				System.IO.File.WriteAllText(savePath, json);
				GameObject.Find("RestartText").GetComponent<TextMeshProUGUI>().text = "Erased all save Data";
			}
			return;
		}

		Vector3 mousePos = Input.mousePosition;

		Ray ray = cam.ScreenPointToRay(mousePos);
		if(Physics.Raycast(ray))
		{
			SceneManager.LoadScene("StoryScreen");
		}
	}
}
