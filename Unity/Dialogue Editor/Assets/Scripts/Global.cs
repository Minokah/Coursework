// Global variables and modules for use anywhere in the game
using UnityEngine;

public static class Global
{
    public static LocalizationSystem localizationSystem = GameObject.FindWithTag("LocalizationSystem").GetComponent<LocalizationSystem>();
    public static DialogueHandler dialogueHandler = GameObject.FindWithTag("DialogueHandler").GetComponent<DialogueHandler>();
}
