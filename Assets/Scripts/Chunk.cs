using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;

    float chunkDistToPlayer;
    float npcDistToPlayer;
    float sceneDistToPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = player.transform.position;
        if (gameObject.tag == "World")
        {
            foreach (Transform child in transform)
            {
                chunkDistToPlayer = Vector3.Distance(playerPos, child.transform.position);
                if (chunkDistToPlayer < 100)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        if (gameObject.tag == "NPC")
        {
            npcDistToPlayer = Vector3.Distance(playerPos, transform.position);
            if (npcDistToPlayer < 100)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        if (gameObject.tag == "Scenery")
        {

        }

    }
}
