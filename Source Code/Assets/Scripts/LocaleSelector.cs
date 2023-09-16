using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System;

public class LocaleSelector : MonoBehaviour
{

    // project specific
    public Button btnEnglish;
    public Button btnRuss;
    private static int LANGUAGE_EN = 0;
    private static int LANGUAGE_RU = 1;

    public Sprite eng_enabled;
    public Sprite eng_disabled;
    public Sprite ru_enabled;
    public Sprite ru_disabled;

    private bool active = false;

    private void Start()
    {
        int local_id = PlayerPrefs.GetInt(Helper.LANGUAGE, 0);
        ChangeLocale(local_id);
    }

    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }


    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        if(btnEnglish != null && btnRuss!= null)
            changeButtonSprites(_localeID);

        PlayerPrefs.SetInt(Helper.LANGUAGE, _localeID);
    
        active = false;
    }

    private void changeButtonSprites(int localeID)
    {
        if(localeID == LANGUAGE_EN)
        {
            btnEnglish.GetComponent<Image>().sprite = eng_enabled;
            btnRuss.GetComponent<Image>().sprite = ru_disabled;
        }
        else if(localeID == LANGUAGE_RU)
        {
            btnEnglish.GetComponent<Image>().sprite = eng_disabled;
            btnRuss.GetComponent<Image>().sprite = ru_enabled;
        }


    }


}
