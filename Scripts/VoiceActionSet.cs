using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Scriptable Object that holds voice lines and action names assoicated with those lines
/// </summary>
[CreateAssetMenu(menuName = "Voice Action Tool/New Voice", fileName = "New Voice")]
[System.Serializable]
public class VoiceActionSet : ScriptableObject
{
    [SerializeField]
    public List<VoiceAction> m_voiceActionList;//parameter list component of the insepctor

    [SerializeField]
    public string randomVoiceSoName = "n/a";//name of this SO
    
    //Default Constructor
    public VoiceActionSet()
    {
        this.m_voiceActionList = new List<VoiceAction>();
        this.m_voiceActionList.Add(new VoiceAction("Action1"));
    }

    /// <summary>
    /// Plays a random line from the action.  Audio is played at the AudioSource
    /// </summary>
    /// <param name="actionName">Name of the action</param>
    /// <param name="source">AudioSource you want to play from</param>
    /// <param name="onlyIfNearPlayer">Optional: default false: set to true if you want to play the sound at the audio source only if the source is near the player's AudioListener</param>
    /// <returns></returns>
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

    /// <summary>
    /// Static: Plays a random line from the action.  Audio is played at the AudioSource
    /// </summary>
    /// <param name="voice">The VoiceActionSet you want to sample from</param>
    /// <param name="actionName">Name of the action</param>
    /// <param name="source"AudioSource you want to play from></param>
    /// <param name="onlyIfNearPlayer">Optional: default false: set to true if you want to play the sound at the audio source only if the source is near the player's AudioListener</param>
    /// <returns></returns>
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

/// <summary>
/// Container for the action string and the list of audio clips
/// </summary>
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
