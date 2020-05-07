using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private GameObject player;
    public Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
