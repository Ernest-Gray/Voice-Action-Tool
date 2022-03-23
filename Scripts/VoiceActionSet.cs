using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice Action Tool/New Voice", fileName = "New Voice")]
[System.Serializable]
public class VoiceActionSet : ScriptableObject
{
    [SerializeField]
    public List<VoiceAction> m_voiceActionList;

    [SerializeField]
    public string randomVoiceSoName = "n/a";
    public VoiceActionSet()
    {
        //Debug.Log("Created RandomVoice SO");
        this.m_voiceActionList = new List<VoiceAction>();
        this.m_voiceActionList.Add(new VoiceAction("Action1"));
    }

    public bool PlayAction(string actionName, AudioSource source, bool onlyIfNearPlayer = false)
    {
        if (onlyIfNearPlayer == false || (onlyIfNearPlayer == true && Vector3.Distance(FindObjectOfType<AudioListener>().transform.position, source.transform.position) < source.maxDistance))
        {
            VoiceAction action = this.m_voiceActionList.Find(item => item.action_name.ToLower() == actionName.ToLower());
            if (action == null)
            {
                Debug.LogWarning("Action of: " + actionName + " not found in Voice of: " + this.name);
                return false;
            }
            else
            {
                if (action.m_actionLines != null && action.m_actionLines.Count > 0)
                {
                    AudioClip clip = action.m_actionLines[Random.Range(0, action.m_actionLines.Count)];

                    source.clip = clip;
                    source.Play();
                    return true;
                }
                
            }
        }
        return false;
        
    }

    public static bool PlayAction(VoiceActionSet voice, string actionName, AudioSource source, bool onlyIfNearPlayer = false)
    {
        if (onlyIfNearPlayer == false || (onlyIfNearPlayer == true && Vector3.Distance(FindObjectOfType<AudioListener>().transform.position, source.transform.position) < source.maxDistance))
        {
            if (voice == null)
            {
                Debug.LogWarning("RandomVoiceSO Not Found");
                return false;
            }
            if (source == null)
            {
                Debug.LogWarning("AudioSource not Found");
                return false;
            }

            VoiceAction action = voice.m_voiceActionList.Find(item => item.action_name == actionName);
            if (action == null)
            {
                Debug.LogWarning("Action of: " + actionName + " not found in Voice of: " + voice.name);
                return false;
            }
            else
            {
                if (action.m_actionLines != null && action.m_actionLines.Count > 0)
                {
                    AudioClip clip = action.m_actionLines[Random.Range(0, action.m_actionLines.Count)];

                    source.clip = clip;
                    source.Play();
                    return true;
                }
            }
        }
        return false;

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
