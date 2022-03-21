# Voice-Action-Tool
Unity tool to easily create a list of sounds and associate them with an action

# How to Get Started
## Creating The Voice Objects
### Create a Voice Scriptable Object First by right clicking in your project pane and locating Create/Voice Action Tool/New Voice.
### From here you can add new Actions and Voices to those actions.  
## Using the Voice Objects
### on your monobehavior scripts add a public variable of type VoiceActionSet.  Now on objects that contain your monobehavior script you will be able to select a voice action set.
### To play a random sound from the action you can use VoiceActionSet.PlayAction(VoiceActionSet, actionName, audioSource) and it will randomly play a audioclip from you voice action set with the action name given.
