using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemHints : MonoBehaviour
{
	public ItemBox button;

	public void Start()
	{
		button = GetComponentInParent<ItemBox>();
	}

	public void OnTriggerEnter(Collider other)
	{
		Player player = GameObject.Find("Player").GetComponent<Player>();
		player.hint.SetActive(true);
		if (button.opened)
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "You have already opened this storage box";
		}
		else if (button.type == "charge")
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "This Storage Box has a big battery. Perhaps it needs a long, strong charge?";
		}
		else if (button.type == "punch")
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "This Storage Box has a large button. Maybe try pressing it with a hard blow?";
		}
		else if (button.type == "sword")
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "This Storage Box has a few wires on it. Maybe try cutting them with something sharp?";
		}
		else if (button.type == "laser")
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "This Storage Box has a few sensitive components. Maybe try cutting into them with a short pulsed laser";
		}
		else if (button.type == "rocket")
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "This Storage Box has a well protected components. Maybe try blowing it up to destroy them?";
		}
		else if (button.type == "emp")
		{
			player.hint.GetComponent<TextMeshProUGUI>().text = "This Storage Box has a door that requires a constant electrical charge. Maybe try frying the circuits?";
		}
		player.hintWasActive = true;
	}

	public void OnTriggerExit(Collider other)
	{
		Player player = GameObject.Find("Player").GetComponent<Player>();
		player.hint.SetActive(false);
		player.hintWasActive = false;
	}
}
