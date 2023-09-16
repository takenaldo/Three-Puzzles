using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class soundButtonController : MonoBehaviour
{
    public string button_type = Helper.STATUS_ON;
    // Start is called before the first frame update

    public Sprite offSprite;
    public Sprite onSprite;

    void Start()
    {
        if (button_type == Helper.STATUS_ON)
        {
            if (Helper.isMusicON())
            {
                // project specific
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = onSprite;
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                // project specific
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = offSprite;
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.gray;
            }

        }
    }

    private void Update()
    {
        if (button_type == Helper.STATUS_ON)
        {
            if (Helper.isMusicON())
            {
                // project specific
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = onSprite;
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                // project specific
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = offSprite;
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.gray;
            }

        }
        else
        {
            if (Helper.isMusicON())
            {
                // project specific
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = offSprite;
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.gray;
            }
            else
            {
                // project specific
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = onSprite;
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }
    }
}



