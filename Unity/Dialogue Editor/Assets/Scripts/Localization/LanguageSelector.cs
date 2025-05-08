using TMPro;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    void Update()
    {
        text.text = Global.localizationSystem.Get("SelectLanguage");
    }

    private void ChangeLanguage(int value)
    {
        switch (value)
        {
            case 0:
                Global.localizationSystem.language = "en";
                break;
            case 1:
                Global.localizationSystem.language = "fr";
                break;
            case 2:
                Global.localizationSystem.language = "sp";
                break;
        }
    }
}
