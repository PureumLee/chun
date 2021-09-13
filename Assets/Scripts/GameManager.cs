using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isConnect = false;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CreatePlayer();
    }

    void Update()
    {


    }

    void CreatePlayer()
    {
        Transform spawnPoint = GameObject.Find("SpawnPoint").transform;
        Vector3 pos = spawnPoint.position;
        Quaternion rot = spawnPoint.rotation;

        GameObject playerTemp = PhotonNetwork.Instantiate("Player", pos, rot, 0);
    }
}