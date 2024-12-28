using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class laserArm : Arm
{
    public float timer;
    private float cooldown = .5f;
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        timer = cooldown;
    }

    public void Update()
    {
        if (attacking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                attacking = false;
                timer = cooldown;
            }
        }
    }
    public override void attack()
    {
        attacking = true;
        print("pew");
        animator.Play("armThing");
        GameObject laser = Instantiate(Resources.Load("laser", typeof(GameObject))) as GameObject;
        laser.transform.rotation = transform.rotation;
        laser.transform.position = transform.position;

    }
}
