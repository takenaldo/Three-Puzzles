using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;


public class Helper : MonoBehaviour
{
    public static string SETTING_VIBRATION = "setting_vibration";
    public static string SETTING_MUSIC     = "setting_music";
    public static string BG_MUSIC = "BG_MUSIC";
    public static string LANGUAGE = "user_language";


    public static string STATUS_ON = "on";
    public static string STATUS_OFF = "off";

    public static string USER_LEVEL = "user_level";

    public static int LANG_EN = 0;
    public static int LANG_RU = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadSceneSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadSceneLevels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadSceneInformation()
    {
        SceneManager.LoadScene("Information");
    }

    public void pause()
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        Time.timeScale = 1;
    }



    public void triggMusicSetting()
    {
        AudioSource[] allAudioSources = getAllAudioSources();
        if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_ON)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_OFF);
            foreach (AudioSource audioSource in allAudioSources)
                audioSource.Stop();
        }
        else if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_OFF)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_ON);

            AudioSource bgMusic = GameObject.FindGameObjectWithTag(BG_MUSIC).GetComponent<AudioSource>();
            bgMusic.Play();
        }
    }



    public void triggMusicSettingWithBG(GameObject audioManager)
    {
        AudioSource[] allAudioSources = getAllAudioSources();
        if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_ON)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_OFF);
            foreach (AudioSource audioSource in allAudioSources)
                audioSource.Stop();
        }
        else if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_OFF)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_ON);
            audioManager.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(audioManager);
        }
    }




    public AudioSource[] getAllAudioSources()
    {
      return  GameObject.FindObjectsOfType<AudioSource>();
    }


    public void changeVibrationSetting()
    {
        if(PlayerPrefs.GetString(SETTING_VIBRATION, STATUS_ON) == STATUS_ON)
            PlayerPrefs.SetString(SETTING_VIBRATION, STATUS_OFF);
        else
            PlayerPrefs.SetString(SETTING_VIBRATION, STATUS_ON);
    }


    public static void rotateAroundMyself(GameObject go)
    {
        go.transform.Rotate(new Vector3(0, 0, -1));
    }

    public void LoadLevel(int lvl)
    {
        if ((lvl) <= PlayerPrefs.GetInt(Helper.USER_LEVEL, 1))
            LoadScene("Level" + lvl);
    }

    public void PlayCurrentLevel()
    {
        Debug.Log("CURRENT LEVEL from setting  :" + (PlayerPrefs.GetInt(Helper.USER_LEVEL, 1)));

        LoadScene("Level" + (PlayerPrefs.GetInt(Helper.USER_LEVEL, 1)));
    }

    public static int getUserLevel()
    {
        return PlayerPrefs.GetInt(Helper.USER_LEVEL, 1);
    }

    // incrementes user's level after a level completion/win
    public static void updateLevel()
    {
        int new_high_lvl = Mathf.Min(PlayerPrefs.GetInt(Helper.USER_LEVEL, 1) + 1, GameManager.instance.levels);
        PlayerPrefs.SetInt(USER_LEVEL, new_high_lvl);
    }
    public static bool isMusicON()
    {
        return PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_ON;
    }

    public static bool isVibrationON()
    {
        return PlayerPrefs.GetString(SETTING_VIBRATION, STATUS_ON) == STATUS_ON;
    }


    public void loadNextLevel()
    {
        int next_lvl = GameManager.instance.current_level + 1;
        Debug.Log(">>>> "+next_lvl);
        LoadLevel(next_lvl);
    }
    public void reloadLevel()
    {
        LoadLevel(GameManager.instance.current_level);
    }

    // destroys an object , usefull to call from unity UI
    public void destroy(GameObject g)
    {
        Destroy(gameObject);
    }

    public void enable(GameObject g)
    {
        g.SetActive(true);
    }

    public void disable(GameObject g)
    {
        g.SetActive(false);
    }

    // retrieves the user's selected language
    public static int getPreferedLanguage()
    {
        int local_id = PlayerPrefs.GetInt(Helper.LANGUAGE, GameManager.LANGUAGE_EN);
        return local_id;
    }


    private bool lang_active = false;

    public void ChangeLocale()
    {
        int localeID = LANG_EN;
        if (LocalizationSettings.SelectedLocale.Identifier.Code == "en")
            localeID = LANG_RU;

        
        if (lang_active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }


    IEnumerator SetLocale(int _localeID)
    {
        lang_active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        PlayerPrefs.SetInt(Helper.LANGUAGE, _localeID);

        lang_active = false;
    }

    // returns all the childs in a given Game Object
    public static GameObject[] getChildsFor(GameObject gameObject)
    {
        GameObject[] childs = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < childs.Length; i++)
            childs[i] = gameObject.transform.GetChild(i).gameObject;
        return childs;
    }
}
