using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //Components
    public Rigidbody2D rbody { get; private set; }
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        Invoke(nameof(RandomStart), 1f);
    }

    void Update()
    {
        
    }


    //Shoots the ball in a random direction
    private void RandomStart()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = 1f;

        rbody.AddForce(force.normalized * 500f);
    }
}
