using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && CubeState.autoRotating == false)
        {
            //take cube current state
            readCube.ReadState();

            //raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                //Make a list of gameObjects, with all sides
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };
                // If teh face hit exists within a side
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        print(face);
                        //make it childrens to the side
                        cubeState.PickUp(cubeSide);
                        //start the side rotation logic
                        cubeSide[4].transform.parent.GetComponent<PivitRotation>().Rotate(cubeSide);
                    }
                }

            }
        }
    }
}
