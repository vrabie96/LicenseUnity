using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class HandMagnet : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform parentHand;
    public Transform LeapRightHandRay;
    public GameObject emptyObj;
    private Controller controller;
    private GameObject hitFace;
    public GameObject rayStart;
    public GameObject handsObject;
    public static bool cubeLock = false;

    public GameObject cubeRubick;
    private Hand right_leap_hand;
    private float speed = 200f;
    private float rotateSpeed = 100f;
    private int layerMask = 1 << 8;
    Vector3[] palmPossition;
    private static bool initHand;
    private Mesh _sphereMesh;
    private Transform initialCubePos;
    private PrintMessage printMessage;
    float distance = 2.5f;
    private Vector3 previosFramePosition;
    private Quaternion previosRotation;

    void Start()
    {

        controller = new Controller();
        initHand = false;
        palmPossition = new Vector3[2];
        //initialCubePos = cubeRubick.transform;
        printMessage = FindObjectOfType<PrintMessage>();
    }

    // Update is called once per frame
    void Update()
    {
        //Frame frame = controller.Frame(); // controller is a Controller object
        //if (frame.Hands.Count > 0)
        //{

        //List<Hand> hands = frame.Hands;
        //Hand firstHand = hands[0];

        //print(firstHand.Fingers[1].TipPosition.ToVector3());
        //if (initHand == false)
        //{
        //    hand_model = GetComponent("HandModels") as HandModel;
        //    print(hand_model);
        //    //right_leap_hand = hand_model.GetLeapHand();
        //    //if (right_leap_hand == null) Debug.LogError("No right_leap_hand founded");
        //    //initHand = true;

        //}

        //if (initHand == true)
        //{

        //    for (int i = 0; i < HandModel.NUM_FINGERS; i++)
        //    {
        //        FingerModel finger = hand_model.fingers[i];
        //        // draw ray from finger tips (enable Gizmos in Game window to see)
        //        Debug.DrawRay(finger.GetTipPosition(), finger.GetRay().direction, Color.red);
        //    }
        //}
        //}
        

        //LeapRightHand.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //readObject();
        
        Frame frame = controller.Frame(); // controller is a Controller object

        if (frame.Hands.Count > 0)
        {
            //if (initHand == true)
            //{
            //print(frame.Hands.Count);
            List<Hand> hands = frame.Hands;
            right_leap_hand = hands[0];
            if (right_leap_hand.IsRight)
            {
            
                //Leap.Unity.Pose pose = new Leap.Unity.Pose(right_leap_hand.PalmPosition.ToVector3(), right_leap_hand.Rotation.ToQuaternion());
                LeapTransform leapMatrix = transform.GetLeapMatrix();
                //palmPossition[0] = right_leap_hand.PalmPosition.ToVector3();
                //palmPossition[1] = pose.position;

                //Vector3 handPosition = pose.position;
                //print(palmPossition[0]);
                //float handYBasis = right_leap_hand.PalmNormal.Roll;

                //float pitch = right_leap_hand.Direction.Pitch;
                //float yaw = right_leap_hand.Direction.Yaw;
                //float x = right_leap_hand.PalmNormal.Pitch;
                
                //float roll = right_leap_hand.PalmNormal.Roll;
                ////Vector normalPalm = new Vector(pitch, yaw, roll);
                //LeapQuaternion handRotation = right_leap_hand.Basis.rotation;
                
                //handRotation.z = roll;
                //handRotation.y = yaw;
                //handRotation.x = pitch;

                //Vector handZBasis = -right_leap_hand.Direction;
                Vector handOrigin = right_leap_hand.PalmPosition;
                Vector normalPalm = right_leap_hand.PalmNormal;
                //print(normalPalm);
                // print(handOrigin.Normalized);

                //print(xBasis);
                //print(Matrix4x4.TRS(handOrigin,
                //     Quaternion.identity,
                //     new Vector3(transform.lossyScale.x, transform.lossyScale.x, transform.lossyScale.z)));
                //print(rayStart.transform.worldToLocalMatrix);
                //Matrix handTransform = new Matrix(handXBasis, handYBasis, handZBasis, handOrigin);
                //handTransform = handTransform.RigidInverse();
                //Vector transformedPosition = handTransform.TransformPoint(right_leap_hand.PalmPosition);
                //float locationPart = normalPalm.x
                Vector3 normalPalmUnity = leapMatrix.TransformDirection(normalPalm).ToVector3();
                Vector3 leapHandPosition = leapMatrix.TransformPoint(handOrigin).ToVector3();
                //Quaternion leapHandDirection = leapMatrix.translation
                //Quaternion leapHandDirection = handRotation.ToQuaternion();

                leapHandPosition.y -= 1.8f;
                leapHandPosition.x -= 0.7f;
                LeapRightHandRay.position = leapHandPosition;

                
                //print(leapHandDirection);
                //Vector3 rotationToHand = new Vector3(leapHandDirection.eulerAngles.x, leapHandDirection.eulerAngles.y, leapHandDirection.eulerAngles.z);
                rayStart = Instantiate(emptyObj, LeapRightHandRay.position, Quaternion.identity, LeapRightHandRay);
                
                //print(leapHandDirection.eulerAngles);
                LeapRightHandRay.forward = normalPalmUnity;
                //LeapRightHandRay.localRotation = Quaternion.Euler(rotationToHand);
                //LeapRightHandRay.localRotation = Quaternion.FromToRotation(LeapRightHandRay.forward, normalPalmUnity);
                //print(LeapRightHandRay.forward);
                //float step = speed / 4 * Time.deltaTime;
                //readObject();
                
                //print(printMessage.isActivated);
                //cubeRubick.transform.position = Vector3.MoveTowards(cubeRubick.transform.position, LeapRightHandRay.position, step);

                //Finger leapFinger = firstHand.Fingers[1];
                //Vector transformedPosition = handTransform.TransformPoint(leapFinger.TipPosition);
                //Vector transformedDirection = handTransform.TransformDirection(leapFinger.Direction);
                //print(transformedPosition);
                // Do something with the transformed fingers


                //Leap.Vector palmPosition = firstHand.PalmPosition;
                //Leap.Vector palmDirection = firstHand.PalmNormal;
                //Vector3 vectpalmPosition = new Vector3(palmPosition.x,
                //                                        palmPosition.y,
                //                                        palmPosition.z);

                //print(vectpalmPosition);

                ////print("Trasform ray:");
                ////print(LeapRightHand.position);
                ////print("Trasform hand pos:");
                ////print(vectpalmPosition);

                //initHand = false;


                //GameObject rayStart = Instantiate(emptyObj, vectpalmPosition, Quaternion.identity, parentHand);


                ////Vector3 unityPosition = ToPositionVector3(palmDirection);

                ////LeapRightHand.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                //Vector3 ray = rayStart.transform.position;
                //RaycastHit hit;
                ////print(ray);
                ////Intersecteaza raza vreun obiect in layerMask ?
                //if (Physics.Raycast(ray, LeapRightHand.forward, out hit, Mathf.Infinity, layerMask))
                //{
                //    Debug.DrawRay(ray, LeapRightHand.forward * hit.distance, Color.yellow);
                //    print(hit.collider.gameObject);
                //    //print(hit.collider.gameObject.name);
                //}
                //else
                //{
                //    Debug.DrawRay(ray, LeapRightHand.forward * 1000, Color.green);
                //}

                //print(firstHand.PalmNormal);

                // }
                //}
                //}

            }
        }
    }

    public void GetHandPosition()
    {
        previosFramePosition = LeapRightHandRay.position;
    }

    public void readObject()
    {
        Vector3 ray = rayStart.transform.position;
        RaycastHit hit;
        float step = speed / 80 * Time.deltaTime;
        //print(ray);
        // Intersecteaza raza vreun obiect in layerMask?
        if (Physics.Raycast(ray, LeapRightHandRay.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray, LeapRightHandRay.forward * hit.distance, Color.yellow);
           
            hitFace = hit.collider.gameObject.transform.parent.gameObject;
            
            
            
            cubeRubick.transform.position = Vector3.MoveTowards(cubeRubick.transform.position, LeapRightHandRay.forward * distance + LeapRightHandRay.transform.position, step);
            previosRotation = LeapRightHandRay.rotation;
            //print(Vector3.Distance(cubeRubick.transform.position, LeapRightHandRay.position));
            //print(hit.collider.gameObject.name);

        }
        else  
        {
            Debug.DrawRay(ray, LeapRightHandRay.forward * 1000, Color.green);

            //cubeRubick.transform.position = Vector3.MoveTowards(cubeRubick.transform.position, initialCubePos.position, step);
        }

        
    }
    
    public void lockObject()
    {
        Vector3 ray = rayStart.transform.position;
        RaycastHit hit;
        float distanceCheck = 0;
        float aproxMin = -0.5f;
        float aproxMax = 0.5f;
        float step = speed / 40 * Time.deltaTime;
        float rotateStep = rotateSpeed * Time.deltaTime;

        if (Physics.Raycast(ray, LeapRightHandRay.forward, out hit, Mathf.Infinity, layerMask))
        {
            distanceCheck = Vector3.Distance(cubeRubick.transform.position, LeapRightHandRay.transform.position);
            if (distanceCheck > (distance + aproxMin) && distanceCheck < (distance + aproxMax))
            {
                Vector3 cubePosNew = LeapRightHandRay.position + (LeapRightHandRay.forward * distance);
                //cubeNextPosition = cubeRubick.transform.position + (LeapRightHandRay.transform.position - previosFramePosition);
                cubeRubick.transform.position = Vector3.MoveTowards(cubeRubick.transform.position, cubePosNew, step);
                cubeRubick.transform.rotation = Quaternion.Inverse((previosRotation * Quaternion.Inverse(LeapRightHandRay.transform.rotation))) * cubeRubick.transform.rotation;
                //previosFramePosition = LeapRightHandRay.transform.position;
                //previosRotation = LeapRightHandRay.transform.rotation ;
                //step = speed / 100 * Time.deltaTime;
                previosRotation = LeapRightHandRay.rotation;

                if (cubeLock == false)
                { 
                    cubeLock = true;
                }

            }
        }
        else
        {
            if(cubeLock == true)
            {
                distanceCheck = Vector3.Distance(cubeRubick.transform.position, LeapRightHandRay.position);
                Vector3 cubePosNew = LeapRightHandRay.position + (LeapRightHandRay.forward * distance);
                //cubeNextPosition = cubeRubick.transform.position + (LeapRightHandRay.transform.position - previosFramePosition);
                cubeRubick.transform.position = cubePosNew;
                //previosFramePosition = LeapRightHandRay.transform.position;
                previosRotation = LeapRightHandRay.rotation;
            }
            Debug.DrawRay(ray, LeapRightHandRay.forward * 1000, Color.green);
            //float step = speed / 100 * Time.deltaTime;
            


        }
    }



    //Leap.Vector differentialNormalizer(Leap.Vector leapPoint,
    //                       InteractionBox iBox,
    //                               bool isLeft,
    //                               bool clamp)
    //{
    //    Leap.Vector normalized = iBox.NormalizePoint(leapPoint, false);
    //    float offset = isLeft ? 0.25f : -0.25f;
    //    normalized.x += offset;

    //    //clamp after offsetting
    //    normalized.x = (clamp && normalized.x < 0) ? 0 : normalized.x;
    //    normalized.x = (clamp && normalized.x > 1) ? 1 : normalized.x;
    //    normalized.y = (clamp && normalized.y < 0) ? 0 : normalized.y;
    //    normalized.y = (clamp && normalized.y > 1) ? 1 : normalized.y;

    //    return normalized;
    //}
}
