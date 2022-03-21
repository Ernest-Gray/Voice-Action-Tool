using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice Action Tool/New Voice", fileName = "New Voice")]
[System.Serializable]
public class RandomVoice : ScriptableObject
{
    [SerializeField]
    public List<VoiceAction> m_voiceActionList;

    [SerializeField]
    public string randomVoiceSoName = "n/a";
    public RandomVoice()
    {
        //Debug.Log("Created RandomVoice SO");
        this.m_voiceActionList = new List<VoiceAction>();
        this.m_voiceActionList.Add(new VoiceAction("Action1"));
    }

    public void PlayIdle(string actionName, AudioSource source)
    {
        VoiceAction action = this.m_voiceActionList.Find(item => item.action_name == actionName);
        if (action == null)
        {
            Debug.LogWarning("Action of: " + actionName + " not found in Voice of: " + this.name);
        }
        else
        {
            AudioClip clip = action.m_actionLines[Random.Range(0, action.m_actionLines.Count)];

            source.clip = clip;
            source.Play();
        }
    }

    public static void PlayIdle(RandomVoice voice, string actionName, AudioSource source)
    {
        if (voice == null)
        {
            Debug.LogWarning("RandomVoiceSO Not Found");
            return;
        }
        if (source == null)
        {
            Debug.LogWarning("AudioSource not Found");
            return;
        }

        VoiceAction action = voice.m_voiceActionList.Find(item => item.action_name == actionName);
        if (action == null)
        {
            Debug.LogWarning("Action of: " + actionName + " not found in Voice of: " + voice.name);
        }
        else
        {
            AudioClip clip = action.m_actionLines[Random.Range(0, action.m_actionLines.Count)];

            source.clip = clip;
            source.Play();
        }
    }
}

[System.Serializable]
public class VoiceAction
{
    [SerializeField]
    public string action_name = "Hello World";

    [SerializeField]
    public List<AudioClip> m_actionLines;

    public VoiceAction(string action)
    {
        //Debug.Log("Created VoiceAction SO");
        this.action_name = action;
    }
}
