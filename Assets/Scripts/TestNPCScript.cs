using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPCScript : MonoBehaviour
{

    Rigidbody npcBody;
    private int timer = -180;
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        npcBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            npcBody.velocity = transform.forward * speed;
        }
        else if (timer > 0 && timer < 180)
        {
            npcBody.velocity = -transform.forward * speed;
        }
        else if (timer > 180)
        {
            timer = -180;
        }
        timer++;
    }
}
