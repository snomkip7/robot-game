using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
	public string leftArmType;
	public string rightArmType;
	public string bodyType;
	public string legType;

	public SaveFile(string left,string right, string body, string leg)
	{
		leftArmType = left;
		rightArmType = right;
		bodyType = body;
		legType = leg;
	}
}
