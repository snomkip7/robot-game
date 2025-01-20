using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float lifespan = 2f;
    private float speed = 300f;
    private Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if(lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Enemy>().takeDmg(10);
        //print("omg hit smth");
        Destroy(gameObject);
    }
}
