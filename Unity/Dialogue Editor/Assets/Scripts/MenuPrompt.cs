using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrompt : MonoBehaviour
{
    DialogueGraph graph;
    public AudioSource mallMusic;
    public AudioSource cityMusic;

    public GameObject buttonArea;
    public TMP_Text text;
    public Button talk;
    public TMP_Text talkText;
    public Button music;
    public TMP_Text musicText;

    public GameObject mallBackground;
    public GameObject cityBackground;

    // Start is called before the first frame update
    void Start()
    {
        graph = Resources.Load<DialogueGraph>("Dialogue/MrRobot");
        Global.dialogueHandler.SetDialogueGraph(graph);
        talk.onClick.AddListener(TalkRobot);
        music.onClick.AddListener(ChangeMusic);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Global.localizationSystem.Get("WhatToAsk");
        talkText.text = Global.localizationSystem.Get("TalkToRobot");
        musicText.text = Global.localizationSystem.Get("ChangeMusic");

        // Not talking? Show the menu
        if (!Global.dialogueHandler.conversing) buttonArea.SetActive(true);
    }

    void TalkRobot()
    {
        buttonArea.SetActive(false);
        Global.dialogueHandler.BeginDialogue("RobotStart");
        
        // Remove music change listeners
        Global.dialogueHandler.GetButton(0).onClick.RemoveListener(SetMall);
        Global.dialogueHandler.GetButton(1).onClick.RemoveListener(SetCity);
    }

    void ChangeMusic()
    {
        buttonArea.SetActive(false);
        Global.dialogueHandler.BeginDialogue("RobotMusic");

        // Listen to change music
        Global.dialogueHandler.GetButton(0).onClick.AddListener(SetMall);
        Global.dialogueHandler.GetButton(1).onClick.AddListener(SetCity);
    }

    void SetMall()
    {
        string current = Global.dialogueHandler.GetCurrentID();
        if (current != null && current.Equals("MusicChanged"))
        {
            if (cityMusic.isPlaying) cityMusic.Stop();
            if (!mallMusic.isPlaying) mallMusic.Play();
            mallBackground.SetActive(true);
            cityBackground.SetActive(false);
        }    
    }

    void SetCity()
    {
        // Only run if current ID is the music change one
        string current = Global.dialogueHandler.GetCurrentID();
        if (current != null && current.Equals("MusicChanged"))
        {
            if (mallMusic.isPlaying) mallMusic.Stop();
            if (!cityMusic.isPlaying) cityMusic.Play();
            cityBackground.SetActive(true);
            mallBackground.SetActive(false);
        }
    }
}
