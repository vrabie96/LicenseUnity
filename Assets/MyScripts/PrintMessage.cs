using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMessage : MonoBehaviour
{

    public int isActivated = 2;
    private int countFrame = 0;
    private HandMagnet handMagnet;

    private void Start()
    {
        handMagnet = FindObjectOfType<HandMagnet>();
    }
    private void Update()
    {
        if (isActivated == 1)
        {
            handMagnet.readObject();
            handMagnet.GetHandPosition();
            HandMagnet.cubeLock = false;
        }
        else if (isActivated == 0)
        {
            handMagnet.lockObject();
            //handMagnet.GetHandPosition();
        }
    }
    public void Activate()
    {

        print("Activated");
        isActivated = 1;

    }

    public void Decativate()
    {
        isActivated = 0;
    }

             

}
