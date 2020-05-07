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
    void FixedUpdate()
    {
        for (int i = 0; i < segments.Count; i++)
         {
            segmentDistToPlayer = Vector3.Distance(track.playerPos, segments[i].transform.position);
            if (segmentDistToPlayer > 2000)
            {
                segments[i].GetComponentInChildren<ChunkDataControl>().enabled = false;
            }
            if (segmentDistToPlayer < 2000)
            {
                segments[i].GetComponentInChildren<ChunkDataControl>().enabled = true;
            }
         }
    }
}
