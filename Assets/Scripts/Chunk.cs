using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;

    float chunkDistToPlayer;
    float unloadedChunkDistToPlayer;
    float npcDistToPlayer;
    float sceneDistToPlayer;

    public GameObject prefab;

    GameObject clone;

    private int numChunks = 81;
    public int currentCycle = 1;

    private string json;
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
                if (chunkDistToPlayer > 100)
                {
                    //if out of range, save gameobject name and position, then destroy - SAVE
                    SaveObjectData saveObject = new SaveObjectData
                    {
                        gameObjectName = child.transform.name,
                        position = new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z),
                    };
                    json = JsonUtility.ToJson(saveObject, true);
                    File.WriteAllText(Application.dataPath + "/WorldData/" + child.transform.name + ".txt", json);
                    Destroy(child.gameObject);
                }
            }
        }
        //check if player is in range of any saved positions - LOAD
        if (File.Exists(Application.dataPath + "/WorldData/Zone " + currentCycle + ".txt"))
        {
            string findObjectString = File.ReadAllText(Application.dataPath + "/WorldData/Zone " + currentCycle + ".txt");
            SaveObjectData saveObject = JsonUtility.FromJson<SaveObjectData>(findObjectString);
             unloadedChunkDistToPlayer = Vector3.Distance(playerPos, saveObject.position);
            if (unloadedChunkDistToPlayer < 100)
            {
                clone = Instantiate(prefab, saveObject.position, Quaternion.identity);
                clone.gameObject.name = "Zone " + currentCycle;
                clone.transform.parent = this.transform.parent;
                File.Delete(Application.dataPath + "/WorldData/Zone " + currentCycle + ".txt");
            }
        }
        currentCycle++;
        if (currentCycle > 82)
        {
            currentCycle = 1;
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

    [System.Serializable]
    public class WorldData
    {
        public SaveObjectData[] worldData;

    }

    public class SaveObjectData
    {
        public Vector3 position;
        public string gameObjectName;
    }

    public class LoadObject
    {

    }


}
