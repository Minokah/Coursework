using System;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public string language = "en";
    public Language english;
    public Language french;
    public Language spanish;

    // Tick hotReload ON to continuously refresh resources
    private bool hotReload = true;

    private bool loaded = false;

    // Get a string for the selected language
    public string Get(string key)
    {
        string str = null;

        try
        {
            if (hotReload || !loaded) Load();

            switch (language)
            {
                case "en":
                    str = english.Get(key);
                    break;
                case "fr":
                    str = french.Get(key);
                    break;
                case "sp":
                    str = spanish.Get(key);
                    break;
            }
        }
        // Catch whatever exception.. whether it be a language pack missing or invalid key
        catch (Exception) {}

        if (str == null) return key;
        return str;
    }

    // Force load the language assets
    public void Load()
    {
        if (english == null) {
            Debug.Log("Localization: Loading English");
            english = Resources.Load<Language>("Localization/en");
        }
        if (french == null)
        {
            Debug.Log("Localization: Loading French");
            french = Resources.Load<Language>("Localization/fr");
        }
        if (spanish == null)
        {
            Debug.Log("Localization: Loading Spanish");
            spanish = Resources.Load<Language>("Localization/sp");
        }

        loaded = true;
    }
}
