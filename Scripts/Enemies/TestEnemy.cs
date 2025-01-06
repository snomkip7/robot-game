using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
	public override void takeDmg(float amount)
	{
		print("oof got hit for " + amount);
	}

	public override void stun()
	{
		print("do be stunned tho");
	}
}
