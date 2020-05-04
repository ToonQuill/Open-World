using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class NPCDataControl : MonoBehaviour
{

    public GameObject player;
    private Vector3 playerPos;

    float npcDistToPlayer;
    float unloadedNPCDistToPlayer;

    private GameObject npcPrefab;
    GameObject clone;

    private List<string> unloadedNPCS = new List<string>();

    private string findNPCData;

    static string receivedNPCState;

    private string json;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (gameObject.tag == "NPC")
        {
            foreach (Transform child in transform)
            {
                npcDistToPlayer = Vector3.Distance(playerPos, child.transform.position);
                if (npcDistToPlayer > 100)
                {
                    NPCObjectData retrievedNPCData = new NPCObjectData
                    {
                        npcName = child.transform.name,
                        position = new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z),
                        npcState = child.GetComponent<NPCManager>().GetNPCState(),
                        npcPrefab = child.GetComponent<NPCManager>().GetNPCPrefab(),
                    };
                    json = JsonUtility.ToJson(retrievedNPCData, true);
                    File.WriteAllText(Application.dataPath + "/WorldData/NPCData/" + child.transform.name + ".json", json);
                    unloadedNPCS.Add(child.transform.name);
                    Destroy(child.gameObject);
                }
            }
        }
        for (int i = 0; i < unloadedNPCS.Count; i++)
        {
            if (File.Exists(Application.dataPath + "/WorldData/NPCData/" + unloadedNPCS[i] + ".json"))
            {
                findNPCData = File.ReadAllText(Application.dataPath + "/WorldData/NPCData/" + unloadedNPCS[i] + ".json");
                NPCObjectData retrievedNPCData = JsonUtility.FromJson<NPCObjectData>(findNPCData);
                unloadedNPCDistToPlayer = Vector3.Distance(playerPos, retrievedNPCData.position);
                if (unloadedNPCDistToPlayer < 100)
                {
                    clone = Instantiate(retrievedNPCData.npcPrefab, retrievedNPCData.position, Quaternion.identity);
                    clone.gameObject.name = retrievedNPCData.npcName;
                    clone.transform.SetParent(this.gameObject.transform);
                    clone.GetComponent<NPCManager>().SetNPCState(retrievedNPCData.npcState);
                    clone.GetComponent<NPCManager>().SetNPCPrefab(retrievedNPCData.npcPrefab);
                    File.Delete(Application.dataPath + "/WorldData/NPCData/" + unloadedNPCS[i] + ".json");
                    unloadedNPCS.Remove(retrievedNPCData.npcName);
                }
            }
        }

    }

    public class NPCObjectData
    {
        public Vector3 position;
        public string npcName;
        public string npcState;
        public GameObject npcPrefab;
    }
}
