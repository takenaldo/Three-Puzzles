using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragBeep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("drag_beep").Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
