# Dialogue System and Editor (using XNode)

This tool allows a user to easily create dialogue nodes for use in a conversation.

## Usage
To start, go to Assets > Resources > Dialogue > Right Click > Create > Dialogue > New Dialogue Graph.
An example for MrRobot is already there. Double click the file to open the graph editor.

To make a node, right click the space and click Dialogue. Please assign it an ID.
For the rest of the fields, they will be run through the localization system. Please enter your statements
and selections as IDs that are were made using the localization tool.

To pull up the localization tool, it is under Top Bar > Tools > Localization Editor.

Out IDs will correspond to the connected Dialogue nodes you have made and their IDs.
Please ensure the number of OutIDs match the number of nodes you have hooked up to the current node.

The selections will also correspond with your OutIDs, so if there are 2 OutIDs there should be 2 selections.

To begin dialogue, simply just call Global.dialogueHandler.BeginDialogue("My Starting ID");

You can add event listeners to the dialogue selections you make, for example:
Global.dialogueHandler.GetButton(0).onClick.AddListener(SetMall);
Global.dialogueHandler.GetButton(0).onClick.RemoveListener(SetMall);

### Demo
A scene has been created to demo these features, with some simple dialogue setup.
Click Play to run it. To progress the robot's text, just Left Click within the dialogue box.
To change the current language in the game, in the top left click the dropdown and change the language.
You can talk to the robot OR change the music.