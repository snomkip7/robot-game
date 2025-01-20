using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Legs : MonoBehaviour
{
	public Player player;
	public abstract void move();

	public abstract void init(Player player);
}
