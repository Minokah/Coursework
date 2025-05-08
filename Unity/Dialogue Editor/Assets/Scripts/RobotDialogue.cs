using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDialogue : MonoBehaviour
{
    // Mr. Robot's dialogue
    DialogueGraph graph;

    // Start is called before the first frame update
    void Start()
    {
        
        Global.dialogueHandler.BeginDialogue("RobotStart");
    }
}
