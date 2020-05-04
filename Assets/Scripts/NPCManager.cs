using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    static string NPCState;
    public GameObject npcPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNPCState(string newNPCState)
    {
        NPCState = newNPCState;
    }

    public string GetNPCState()
    {
        return NPCState;
    }

    public void SetNPCPrefab(GameObject newNPCPrefab)
    {
        npcPrefab = newNPCPrefab;
    }

    public GameObject GetNPCPrefab()
    {
        return npcPrefab;
    }
}
