using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectsOfType<BackButtonController>().Length <= 1)
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Main") {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}
