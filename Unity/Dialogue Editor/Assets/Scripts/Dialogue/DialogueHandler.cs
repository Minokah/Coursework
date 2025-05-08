using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // The graph
    DialogueGraph dialogueGraph;

    // UI panel
    public GameObject dialoguePanel;
    public GameObject selectionPanel;
    RectTransform selectionArea;

    // Dialogue box
    public TMP_Text speaker;
    public TMP_Text dialogue;

    // Selection box
    public GameObject selectionBox1;
    Button selectionButton1;
    public TMP_Text selectionText1;

    public GameObject selectionBox2;
    Button selectionButton2;
    public TMP_Text selectionText2;

    public GameObject selectionBox3;
    Button selectionButton3;
    public TMP_Text selectionText3;

    public GameObject selectionBox4;
    Button selectionButton4;
    public TMP_Text selectionText4;

    DialogueNode currentNode;

    // Variables for dialogue box
    string statement;
    int current = 0;
    float time = 0;
    int characterCount = 0;
    bool canProgress = false;
    public bool conversing = false;
    public string initiate;
    public List<string> selections = new List<string>();

    void Awake()
    {
        selectionArea = selectionPanel.GetComponent<RectTransform>();
        selectionButton1 = selectionBox1.GetComponent<Button>();
        selectionButton1.onClick.AddListener(OnSelction1Click);
        selectionButton2 = selectionBox2.GetComponent<Button>();
        selectionButton2.onClick.AddListener(OnSelction2Click);
        selectionButton3 = selectionBox3.GetComponent<Button>();
        selectionButton3.onClick.AddListener(OnSelction3Click);
        selectionButton4 = selectionBox4.GetComponent<Button>();
        selectionButton4.onClick.AddListener(OnSelction4Click);
    }

    // Allow other scripts to hook onto these buttons
    public Button GetButton(int i)
    {
        switch (i)
        {
            case 0:
                return selectionButton1;
            case 1:
                return selectionButton2;
            case 2:
                return selectionButton3;
            case 3:
                return selectionButton4;
        }

        return null;
    }

    public string GetCurrentID()
    {
        if (currentNode != null) return currentNode.id;
        return null;
    }

    public void SetDialogueGraph(DialogueGraph graph)
    {
        dialogueGraph = graph;
    }

    // Force override the node if one chooses
    public void BeginDialogue(string id) {
        // No graph? Don't do anything
        if (dialogueGraph == null) return;

        currentNode = dialogueGraph.GetNode(id);
        if (currentNode == null)
        {
            StopDialogue();
            return;
        }

        conversing = true;
        initiate = id;
        UpdateDialogueBox(currentNode);
    }

    // Immediately stop dialogue
    public void StopDialogue()
    {
        conversing = false;
        currentNode = null;
        characterCount = 0;
        current = 0;
        time = 0;
        dialoguePanel.SetActive(false);
        selectionPanel.SetActive(false);
    }

    // Update the dialogue box with node information
    private void UpdateDialogueBox(DialogueNode node)
    {
        // Invalid id? Don't do anything
        if (node == null) StopDialogue();

        // Don't even bother if there are no statements
        if (node.statements.Count == 0) StopDialogue();

        UpdateSelections();
        characterCount = 0;
        current = 0;
        time = 0;
        selectionPanel.SetActive(false);
    }

    void Update()
    {
        // No node? Don't show the panels
        if (currentNode == null || currentNode.statements.Count == 0)
        {
            StopDialogue();
            return;
        }

        UpdateTranslations();

        dialoguePanel.SetActive(true);

        if (Input.GetMouseButtonDown(0) && canProgress)
        {
            // Get the next dialogue
            if (current + 1 < currentNode.statements.Count)
            {
                time = 0;
                characterCount = 0;
                current++;
            }
            // In the case someone is mashing the mouse, immediately skip the end of the statement
            // Show options if any, goto the next node, or exit conversation
            else
            {
                dialogue.text = statement;
                characterCount = statement.Length;

                if (currentNode.outIds.Count > 0) selectionPanel.SetActive(true);
                else if (currentNode.GetNodes().Count == 1)
                {
                    current = 0;
                    currentNode = currentNode.GetNextNode();
                    UpdateDialogueBox(currentNode);
                }
                else StopDialogue();
            }
        }

        if (current != currentNode.statements.Count)
        {
            // Make the text "roll" out instead of displaying at once
            if (characterCount <= statement.Length)
            {
                dialogue.text = statement.Substring(0, characterCount);

                time += Time.deltaTime;

                if (time > 0.02)
                {
                    time = 0;
                    characterCount++;
                }
            }
            // End of rolling out and there's options, show it
            else if (current + 1 == currentNode.statements.Count)
            {
                dialogue.text = statement;
                if (currentNode.GetNodes().Count > 0) selectionPanel.SetActive(true);
            }
        }
    }

    void UpdateTranslations()
    {
        // Update selections and setup dialogue box
        // Only update speaker if not blank (otherwise keep what was there)
        string sp = Global.localizationSystem.Get(currentNode.speaker);
        if (!sp.Equals("")) speaker.text = sp;

        statement = Global.localizationSystem.Get(currentNode.statements[current]);

        if (characterCount >= statement.Length) dialogue.text = statement;

        // Set the text of the boxes
        for (int i = 0; i != currentNode.selections.Count; i++)
        {
            switch (i)
            {
                case 0:
                    selectionText1.text = Global.localizationSystem.Get(currentNode.selections[i]);
                    break;
                case 1:
                    selectionText2.text = Global.localizationSystem.Get(currentNode.selections[i]);
                    break;
                case 2:
                    selectionText3.text = Global.localizationSystem.Get(currentNode.selections[i]);
                    break;
                case 3:
                    selectionText4.text = Global.localizationSystem.Get(currentNode.selections[i]);
                    break;
            }
        }
    }

    void UpdateSelections() {
        // Reset colours
        selectionBox1.SetActive(false);
        selectionBox2.SetActive(false);
        selectionBox3.SetActive(false);
        selectionBox4.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

        selectionArea.sizeDelta = new Vector2(329, currentNode.selections.Count * 50);

        for (int i = 0; i != currentNode.selections.Count; i++) {
            switch (i) {
                case 0:
                    selectionBox1.SetActive(true);
                    break;
                case 1:
                    selectionBox2.SetActive(true);
                    break;
                case 2:
                    selectionBox3.SetActive(true);
                    break;
                case 3:
                    selectionBox4.SetActive(true);
                    break;
            }
        }
    }

    void OnSelction1Click() { HandleSelection(0); }

    void OnSelction2Click() { HandleSelection(1); }

    void OnSelction3Click() { HandleSelection(2); }

    void OnSelction4Click() { HandleSelection(3); }

    private void HandleSelection(int i)
    {
        // If the selection is marked as "!StopDialogue" immediately stop
        if (currentNode.outIds[i].Equals("!StopDialogue"))
        {
            StopDialogue();
            return;
        }

        // Set node if matching
        foreach (DialogueNode node in currentNode.GetNodes())
        {
            if (node.id.Equals(currentNode.outIds[i]))
            {
                if (node.id.Equals("!StopDialogue"))
                {
                    StopDialogue();
                    return;
                }

                // Add to selection cache for current dialogue tree
                selections.Add(currentNode.outIds[i]);

                currentNode = node;
                UpdateDialogueBox(node);
                return;
            }
        }

        // No match? Try to the next possible available node
        DialogueNode next = currentNode.GetNextNode();
        if (next != null)
        {
            // Avoid dupes
            string toAdd = currentNode.outIds[i];
            if (!selections.Contains(toAdd)) selections.Add(toAdd);

            currentNode = next;
            UpdateDialogueBox(next);
            return;
        }

        // Otherwise, end conversation
        StopDialogue();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canProgress = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canProgress = false;
    }
}
