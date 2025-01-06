using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public abstract void takeDmg(float amount);

	public abstract void stun();
}
