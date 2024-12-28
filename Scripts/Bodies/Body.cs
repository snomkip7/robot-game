using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Body : MonoBehaviour
{
    public bool onCooldown = false;

    public abstract void init(Player player);

    public abstract void special();
}
