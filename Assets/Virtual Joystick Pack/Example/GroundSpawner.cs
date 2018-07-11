using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TankSTW
{
    public class GroundSpawner : NetworkBehaviour
    {
        public GameObject GroundPrefab;
        Vector3 pos = new Vector3(-8,-3,0);
        public override void OnStartServer()
        {
            base.OnStartServer();
            GameObject ground = (GameObject)Instantiate(GroundPrefab, pos, Quaternion.identity);
        }
    }
}
