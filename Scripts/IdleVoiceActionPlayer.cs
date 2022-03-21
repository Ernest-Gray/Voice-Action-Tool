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
        TimeTilNextSound = Time.time + Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > TimeTilNextSound)
        {
            voiceActionSet.PlayAction(idleActionString, audioSource);
            TimeTilNextSound = Time.time + Random.Range(minDelay, maxDelay) + audioSource.clip.length;
        }
    }
}
