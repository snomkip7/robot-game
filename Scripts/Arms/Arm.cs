using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arm : MonoBehaviour
{
    public bool attacking = false;
    public abstract void attack();
}
