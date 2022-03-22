using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleVoiceActionPlayer : MonoBehaviour
{
    public VoiceActionSet voiceActionSet;
    public AudioSource audioSource;

    [Min(0f)]
    public float minDelay = 10;
    public float maxDelay = 20;

    [Tooltip("This must be the same as the voice action name you want to play")]
    public string idleActionString = "idle";

    private float TimeTilNextSound;

    [Tooltip("Prevents numerous IdleVoiceActionPlayer's from playing their audio at the same time")]
    public bool preventOtherIdleVoiceOverlap = true;

    private static float globalDelayTime;
    private static IdleVoiceActionPlayer[] idlePlayers;
    private static List<IdleVoiceActionPlayer> voiceQ;

    public bool playOnlyIfNearPlayer = true;

    private void OnValidate()
    {
        if (minDelay > maxDelay)
        {
            minDelay = maxDelay;
        }
        if (maxDelay < minDelay)
        {
            maxDelay = minDelay;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (idlePlayers == null)
        {
            idlePlayers = FindObjectsOfType<IdleVoiceActionPlayer>();
            voiceQ = new List<IdleVoiceActionPlayer>(idlePlayers);
        }
        

        if (this.voiceActionSet == null)
        {
            this.enabled = false;
            return;
        }
        TimeTilNextSound = Time.time + Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > TimeTilNextSound)
        {
            if (preventOtherIdleVoiceOverlap)
            {
                IdleVoiceActionPlayer va;
                do
                {
                    int number = Random.Range(0, voiceQ.Count);
                    va = voiceQ[number];
                    voiceQ.RemoveAt(number);

                }
                while (va.voiceActionSet.PlayAction(idleActionString, va.audioSource, va.playOnlyIfNearPlayer) == false);
                
            }
            else
            {
                voiceActionSet.PlayAction(idleActionString, audioSource, playOnlyIfNearPlayer);
                TimeTilNextSound = Time.time + Random.Range(minDelay, maxDelay) + audioSource.clip.length;
            }
        }
    }
}
