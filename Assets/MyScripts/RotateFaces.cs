using Leap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using System.Linq;

public class RotateFaces : MonoBehaviour
{
    private CubeState cubeState;
    private Controller controller;
    private Hand leapHand;
    public Transform pinky;
    public Transform ring;
    public Transform middle;
    public Transform index;
    public Transform thumb;
    private List<Transform> fingers = new List<Transform>();
    private List<GameObject> objectFaces;
    private int layerMask = 1 << 8;
    private string objectHitName;
    private IDictionary<string, int> hitCountFaces;
    private float correctionX = -0.7f;
    private float correctionY = -1.8f;
    private Quaternion previosRotation;
    public Quaternion rotationFace;
    private int nrOfHit;
    public List<GameObject> sideHit;
    // Start is called before the first frame update

    void Start()
    {
        controller = new Controller();
        hitCountFaces = new Dictionary<string, int>();
        objectFaces = new List<GameObject>();
        cubeState = FindObjectOfType<CubeState>();
        hitCountFaces.Add("Up", 0);
        hitCountFaces.Add("Down", 0);
        hitCountFaces.Add("Left", 0);
        hitCountFaces.Add("Right", 0);
        hitCountFaces.Add("Front", 0);
        hitCountFaces.Add("Back", 0);
        
        fingers.Add(thumb);
        fingers.Add(index);
        fingers.Add(middle);
        fingers.Add(ring);
        fingers.Add(pinky);

    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = controller.Frame();
        
        if (frame.Hands.Count > 1)
        {
            List<Hand> hands = frame.Hands;
            nrOfHit = 0;
            leapHand = hands[1];
            LeapTransform leapMatrix = transform.GetLeapMatrix();
            List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };
            if (leapHand.IsLeft)
            {                

                for (int f = 0; f < leapHand.Fingers.Count; f++)
                {
                    Finger leapFinger = leapHand.Fingers[f];
                    Vector transformedPosition = leapMatrix.TransformPoint(leapFinger.TipPosition);
                    Vector3 transformedDirection = leapMatrix.TransformDirection(leapFinger.Direction).ToVector3();
                    Vector3 fingerPos = transformedPosition.ToVector3();
                    fingerPos.x += correctionX;
                    fingerPos.y += correctionY;
                    fingers[f].position = fingerPos;

                    fingers[f].forward = transformedDirection;
                    RaycastHit hit;
                    if (Physics.Raycast(fingerPos, fingers[f].forward, out hit, 1, layerMask))
                    {
                        Debug.DrawRay(fingerPos, fingers[f].forward * hit.distance, Color.yellow);
                        //print(hit.collider.gameObject);
                        foreach (List<GameObject> cubeSide in cubeSides)
                        {
                            print(cubeSide);
                            if (cubeSide.Contains(hit.collider.gameObject))
                            {

                                //make it childrens to the side
                                //cubeState.PickUp(cubeSide);
                                //start the side rotation logic
                                //cubeSide[4].transform.parent.GetComponent<PivitRotation>().Rotate(cubeSide);
                                //hitCountFaces[hit.collider.gameObject.name]++;
                                hitCountFaces[hit.collider.gameObject.name]++;
                                string colFace = hit.collider.gameObject.name;
                                print(colFace);
                            }
                        }
                        //objectFaces.Add(hit.collider.gameObject);
                        
                    }
                    else
                    {
                        Debug.DrawRay(fingerPos, fingers[f].forward *1, Color.green);
                        nrOfHit++;
                    }
                }

                string faceHitDeb = getHitFace();
                print(faceHitDeb);
                Quaternion handRotation = leapMatrix.TransformQuaternion(leapHand.Rotation).ToQuaternion();
                rotationFace = Quaternion.Inverse((previosRotation * Quaternion.Inverse(handRotation)));
                
            }

            previosRotation = leapMatrix.TransformQuaternion(leapHand.Rotation).ToQuaternion();
        }

    }

    public string getHitFace()
    {
        string objHitname;
        if (nrOfHit > 2)
        {
            int maxFaceHit = hitCountFaces.ElementAt(0).Value;
            objHitname = hitCountFaces.ElementAt(0).Key;
            for (int index = 1; index < hitCountFaces.Count; index++)
            {
                if (maxFaceHit < hitCountFaces.ElementAt(index).Value)
                {
                    objHitname = hitCountFaces.ElementAt(index).Key;
                    maxFaceHit = hitCountFaces.ElementAt(index).Value;
                }
            }
            return objHitname;
        }
        else
        {
            return null;
        }
    }

}
