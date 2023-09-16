using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float timeout_seconds = 60;
    private float start_time;
    public TextMeshProUGUI txtTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.timeUp && !GameManager.instance.gameFinished)
            handleTiming();   
    }


    private void handleTiming()
    {
        float now = Time.time;

        if (now - start_time > timeout_seconds)
        {
            GameManager.instance.timeUp = true;
            return;
        }

        int min = (((int)(now - start_time)) / 60);
        int sec = (((int)(now - start_time)) % 60);
        string timeRemaining = ((int)(timeout_seconds / 60) - min - 1) + ":" + ((int)(59 - sec)).ToString().PadLeft(2, '0');
        txtTimeRemaining.text = timeRemaining;


    }
}
