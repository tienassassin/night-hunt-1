using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    /*public float minX = -7f;
    public float maxX = 7f;
    public float minY = -5f;
    public float maxY = 5f;*/

    public Transform[] spawnPos;
    private void Start()
    {
        int ranNum = Random.Range(0,spawnPos.Length); 
        //int ranNum = 1;
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos[ranNum].position, quaternion.identity);
        
        /*Vector2 ranPos = new Vector2(Random.Range(minX, maxX),
            Random.Range(minY, maxY));

        PhotonNetwork.Instantiate(playerPrefab.name, ranPos, Quaternion.identity);*/
    }
}
