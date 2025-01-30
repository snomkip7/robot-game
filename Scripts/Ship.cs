using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    private bool canEnter = false;
	private Player player;

    void Start()
    {
		player = GameObject.Find("Player").GetComponent<Player>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(canEnter && player.fixedShip && Input.GetKey(KeyCode.Q))
		{
			// GO TO END SCREEN
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("EndScreen");
		}
        if(canEnter && Input.GetKey(KeyCode.E))
        {
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("MechBuilding");
		} 
    }

	public void OnTriggerEnter(Collider other)
	{
		player.hint.SetActive(true);
		if (player.fixedShip)
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "Press [Q] to return home";
		}
		else
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "Press [E] to customize your character";
		}
		canEnter = true;
	}

	public void OnTriggerExit(Collider other)
	{
		Player player = GameObject.Find("Player").GetComponent<Player>();
		player.hint.SetActive(false);
		player.hintWasActive = false;
		canEnter = false;
	}
}
