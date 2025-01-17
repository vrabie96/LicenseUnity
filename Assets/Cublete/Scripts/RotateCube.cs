﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector3 previoseMousePosition;
    Vector3 mouseDelta;

    public GameObject targetObject;
    float speed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
       
    }

    void Drag()
    {
        if(Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - previoseMousePosition;
            mouseDelta *= 0.1f;
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }
        else
        {

            if (transform.rotation != targetObject.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetObject.transform.rotation, step);
            }
        }
        previoseMousePosition = Input.mousePosition;
    }

    void Swipe()
    {
        if(Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
        }

        if (Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
           // print(currentSwipe);
            if (LeftSwipe(currentSwipe))
            {
                targetObject.transform.Rotate(0, 90, 0, Space.World);
            }
            else if(RightSwipe(currentSwipe))
            {
                targetObject.transform.Rotate(0, -90, 0, Space.World);
            }
            else if(UpLeftSwipe(currentSwipe))
            {
                targetObject.transform.Rotate(90, 0, 0, Space.World);
            }
            else if(UpRightSwipe(currentSwipe))
            {
                targetObject.transform.Rotate(0, 0, -90, Space.World);
            }
            else if(DownLeftSwipe(currentSwipe))
            {
                targetObject.transform.Rotate(0, 0, 90, Space.World);
            }
            else if(DownRightSwipe(currentSwipe))
            {
                targetObject.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f;
    }
    bool RightSwipe(Vector2 swipe)
    {
        return swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f;
    }
    bool UpLeftSwipe(Vector2 swipe)
    {
        return swipe.y > 0 && swipe.x < 0f;
    }
    bool UpRightSwipe(Vector2 swipe)
    {
        return swipe.y > 0 && swipe.x > 0f;
    }
    bool DownLeftSwipe(Vector2 swipe)
    {
        return swipe.y < 0 && swipe.x < 0f;
    }
    bool DownRightSwipe(Vector2 swipe)
    {
        return swipe.y < 0 && swipe.x > 0f;
    }
    
}
 