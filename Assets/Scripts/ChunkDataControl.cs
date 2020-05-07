using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ChunkDataControl : MonoBehaviour
{
    TrackPlayer track;

    float chunkDistToPlayer;
    float unloadedChunkDistToPlayer;

    public List<GameObject> chunksArray = new List<GameObject>();
    public List<Vector3> zones = new List<Vector3>();
    private GameObject selectedPrefab;

    GameObject clone;

    string findChunkData;

    string dataPath;


    private string json;
    // Start is called before the first frame update
    void Start()
    {
        track = GetComponentInParent<TrackPlayer>();
        dataPath = System.IO.Directory.GetCurrentDirectory() + "/Assets/WorldData/World/" + this.transform.parent.name + "/" + this.transform.name + "/";
        foreach (Transform child in transform)
        {
            zones.Add(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(SaveTerrain(5f));
        StartCoroutine(LoadTerrain(3f));
    }

    private IEnumerator SaveTerrain(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Transform child in transform)
        {
            chunkDistToPlayer = Vector3.Distance(track.playerPos, child.transform.position);
            if (chunkDistToPlayer > 500)
            {
                //if out of range, save gameobject name and position, then destroy - SAVE
                WorldObjectData saveObject = new WorldObjectData
                {
                    worldChunkName = child.transform.name,
                    position = new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z),
                };
                json = JsonUtility.ToJson(saveObject, true);
                File.WriteAllText(dataPath + child.transform.name + ".json", json);
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator LoadTerrain(float delay)
    {
        yield return new WaitForSeconds(delay);
        //check if player is in range of any saved positions - LOAD
        for (int i = 1; i < chunksArray.Count; i++)
        {
            if (File.Exists(dataPath + "Zone " + i + ".json") && Vector3.Distance(track.playerPos, zones[i - 1]) < 500)
            {
                findChunkData = File.ReadAllText(dataPath + "Zone " + i + ".json");
                WorldObjectData retrievedChunkData = JsonUtility.FromJson<WorldObjectData>(findChunkData);
                selectedPrefab = chunksArray[i - 1];
                clone = Instantiate(selectedPrefab, retrievedChunkData.position, Quaternion.identity);
                clone.gameObject.name = "Zone " + i;
                clone.transform.SetParent(this.gameObject.transform);
                File.Delete(dataPath + "Zone " + i + ".json");
            }
            //Debug.Log(this.transform.parent.name + " " + this.transform.name + " " + Vector3.Distance(track.playerPos, zones[i]));
        }
    }

    public class WorldObjectData
    {
        public Vector3 position;
        public string worldChunkName;
    }
}
