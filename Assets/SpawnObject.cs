using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject ObjectSpawn;
    public Vector3 location;

    public void SpawnObj()
    {

        Instantiate(ObjectSpawn, new Vector3(0, 2, 1), Quaternion.identity);
    }

}
