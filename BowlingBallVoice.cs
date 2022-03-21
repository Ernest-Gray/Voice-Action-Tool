using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BowlingBallVoice : MonoBehaviour
{
    public RandomVoice voice;
    private AudioSource source;


    public enum BallState
    {
        [EnumMember(Value = "Idle")]
        Idle,
        [EnumMember(Value = "Rolling")]
        Rolling,
        [EnumMember(Value = "InHand")]
        InHand

    }

    public BallState state;
    public float voiceDelayMin = 7;
    public float voiceDelayMax = 15;

    [SerializeField]
    private float nextVoice;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        SetNextVoiceTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextVoice)
        {
            voice.PlayIdle(state.ToString(), source);
            SetNextVoiceTime();
        }
    }

    //Functions
    private void SetNextVoiceTime()
    {
        nextVoice = Time.time + Random.Range(voiceDelayMin, voiceDelayMax);
        if (source!= null && source.clip != null)
        {
            nextVoice += source.clip.length;
        }
    }
}
