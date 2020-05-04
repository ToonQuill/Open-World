using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optimiseChunkDataControl : MonoBehaviour
{
    TrackPlayer track;

    float segmentDistToPlayer;

    public List<GameObject> segments = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        track = GetComponentInParent<TrackPlayer>();
        foreach (Transform child in transform)
        {
            segments.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        segmentDistToPlayer = Vector3.Distance(track.playerPos, this.transform.position);
        for (int i = 0; i < segments.Count; i++)
         {
            if (segmentDistToPlayer > 1500)
            {
                segments[i].GetComponent<ChunkDataControl>().enabled = false;
            }
            if (segmentDistToPlayer < 1500)
            {
                segments[i].GetComponent<ChunkDataControl>().enabled = true;
            }
         }
    }
}
