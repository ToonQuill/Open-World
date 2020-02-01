using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRoam : MonoBehaviour
{

    public string npcState = "Forwards";
    public int nextNPCStateSwitch = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (npcState)
        {
            case "Forwards":
                transform.position -= Vector3.forward * Time.deltaTime;
                if (nextNPCStateSwitch == 0)
                {
                    npcState = "Stop1";
                    nextNPCStateSwitch = 30;
                }
                break;
            case "Stop1":
                if (nextNPCStateSwitch == 0)
                {
                    npcState = "Backwards";
                    nextNPCStateSwitch = 500;
                }
                break;
            case "Backwards":
                transform.position += Vector3.forward * Time.deltaTime;
                {
                    npcState = "Stop2";
                    nextNPCStateSwitch = 30;
                }
                break;
            case "Stop2":
                if (nextNPCStateSwitch == 0)
                {
                    npcState = "Forwards";
                    nextNPCStateSwitch = 500;
                }
                break;
        }
        nextNPCStateSwitch--;

    }
}
