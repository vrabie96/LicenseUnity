using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{

    CubeState cubeState;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;

    public Material Orange;
    public Material White;
    public Material Red;
    public Material Green;
    public Material Blue;
    public Material Yellow;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        cubeState = FindObjectOfType<CubeState>();
        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);
        UpdateMap(cubeState.left, left);
        UpdateMap(cubeState.right, right);

    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            //print(face[0].name[0]);
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = Orange.color;
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Red.color;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Yellow.color;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = White.color;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Green.color;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Blue.color;
            }
            i++;
        }
        
    }
}
