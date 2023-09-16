using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LanguageButtonController : MonoBehaviour
{
    public int button_type = Helper.LANG_EN;
    // Start is called before the first frame update

    public Sprite offSprite;
    public Sprite onSprite;

    void Start()
    {
        if (button_type == Helper.LANG_EN)
        {
            if (Helper.getPreferedLanguage() == 0)
                gameObject.GetComponent<Image>().sprite = onSprite;
            else
                gameObject.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            if (Helper.getPreferedLanguage() == 1)
                gameObject.GetComponent<Image>().sprite = onSprite;
            else
                gameObject.GetComponent<Image>().sprite = offSprite;
        }
    }

    private void Update()
    {

        if (button_type == Helper.LANG_EN)
        {
            if (Helper.getPreferedLanguage() == 0)
                gameObject.GetComponent<Image>().sprite = onSprite;
            else
                gameObject.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            if (Helper.getPreferedLanguage() == 1)
                gameObject.GetComponent<Image>().sprite = onSprite;
            else
                gameObject.GetComponent<Image>().sprite = offSprite;
        }
    }
}



