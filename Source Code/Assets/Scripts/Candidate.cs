using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candidate : MonoBehaviour
{
    // Start is called before the first frame update
    public bool matching;
    private GameObject[] childs;
    private float margin = 0.2f;
    private Vector3 initialPosition;


    void Start()
    {
        childs = Helper.getChildsFor(gameObject);
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onDrop()
    {
        GameManager.instance.movingCandidate = null;
        Debug.Log("^^^^^^^^^^^");
//        GameObject.FindGameObjectWithTag("drag_beep").GetComponent<AudioSource>().Play();
//      matching =  isMatching();
        matching = isMatchingWithNearest();

    }

    private bool isMatching()
    {
        bool match = true;
        List<GameObject[]> list = new List<GameObject[]>();
        Debug.Log("===> " +childs.Length);
        foreach (GameObject child in childs)
        {
            Debug.Log(";;");
            bool found_match = false;
            foreach (GameObject spot in GameManager.instance.spots)
            {
                float distance = Vector2.Distance(spot.transform.position, child.transform.position);
                if(isTrianglesMatching(spot, child))
                {
                    if (!isSpotHeldByOtherCandidate(spot, child))
                    {
                        GameObject[] arr = new GameObject[2];
                        arr[0] = child;
                        arr[1] = spot;
                        list.Add(arr);
                        found_match = true;
//                        Debug.Log(child.name + " Vs " + spot.name + " : " + distance);
                        Debug.Log("FOUND FOR"+ child.name);
                        break;
                    }
                }
                else
                    found_match = false;
            }

            if (!found_match)
                return false;
        }

        adjustTrianglePosition(list);
        return match;
    }


    private bool isMatchingWithNearest()
    {
        bool match = true;
        List<GameObject[]> list = new List<GameObject[]>();
        Debug.Log("===> " + childs.Length);
        foreach (GameObject child in childs)
        {
            Debug.Log(";;");
            bool found_match = false;
            GameObject nearest = null;
            float nearest_distance = 9999999f;
            foreach (GameObject spot in GameManager.instance.spots)
            {
                float distance = Vector2.Distance(spot.transform.position, child.transform.position);
                Debug.Log(distance);
                if (isTrianglesMatching(spot, child))
                {
                    if (!isSpotHeldByOtherCandidate(spot, child))
                    {

                        if(distance < nearest_distance)
                        {
                            nearest_distance = distance;
                            nearest = spot;
                            found_match = true;
                        }
                        //                        Debug.Log(child.name + " Vs " + spot.name + " : " + distance);
                        Debug.Log("FOUND FOR" + child.name);
                        break;
                    }
                }
                else
                    found_match = false;
            }

            if (found_match && nearest!=null)
            {
                GameObject[] arr = new GameObject[2];
                arr[0] = child;
                arr[1] = nearest;
                list.Add(arr);

            }

            if (!found_match)
                return false;
        }

        adjustTrianglePosition(list);
        return match;
    }



    // Checks if two Triangle Objects are near and their rotation are the same ,
    private bool isTrianglesMatching(GameObject spot, GameObject child)
    {
        float distance = Vector2.Distance(spot.transform.position, child.transform.position);
        bool similarRotationZ = (spot.transform.localRotation.z == child.transform.localRotation.z);
        return (distance < margin) && (similarRotationZ);
    }

    //Xhecks if other candidate has already occupied that spot
private bool isSpotHeldByOtherCandidate(GameObject spot, GameObject go)
    {
        foreach (Candidate item in GameManager.instance.candidates)
        {
            Debug.Log("candidate "+item.name);

            for (int i = 0; i < item.gameObject.transform.childCount; i++)
            {


                if (item.gameObject.transform.GetChild(i).gameObject.transform.parent != go.transform.parent)
                {
                    float distance = Vector2.Distance(spot.transform.position, item.gameObject.transform.GetChild(i).transform.position);
                    Debug.Log(item.gameObject.transform.GetChild(i).gameObject.name + " ----- " + go.name+ ">> "+distance);

                    if (distance < margin)
                    {
                        Debug.Log("HERE");
                        Debug.Log(spot.name + " <> " + item.gameObject.transform.GetChild(i).name);
                        bool similarRotationZ = (spot.transform.localRotation.z == item.gameObject.transform.GetChild(i).transform.localRotation.z);

                        if(similarRotationZ)
                            return true;
                    }
                }
            }
        }
        return false;
    }

    // puts the triangles in a matching spot on the board 
    private void adjustTrianglePosition(List<GameObject[]> list)
    {
        foreach (GameObject[] arr in list)
        {
            arr[0].transform.position = arr[1].transform.position;
        }
    }

}


