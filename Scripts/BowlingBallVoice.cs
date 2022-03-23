using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BowlingBallVoice : MonoBehaviour, HMD_Interactable
{
    public VoiceActionSet voice;
    private AudioSource source;


    public enum BallState
    {
        [EnumMember(Value = "Idle")]
        Idle,
        [EnumMember(Value = "Rolling")]
        Rolling,
        [EnumMember(Value = "InHand")]
        InHand,
        [EnumMember(Value = "OnViewEnter")]
        OnViewEnter,
        [EnumMember(Value = "OnViewExit")]
        OnViewExit,
        [EnumMember(Value = "OnGrab")]
        OnGrab,
        [EnumMember(Value = "OnThrow")]
        OnThrow

    }

    public BallState defaultState;
    public float voiceDelayMin = 7;
    public float voiceDelayMax = 15;

    [SerializeField]
    private float nextVoiceTime;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        SetNextVoiceTime();


        VR_Grabbable g;
        if (g = this.GetComponent<VR_Grabbable>())
        {
            g.pickupFunctions += PlayGrabNoise;
            g.throwFunctions += PlayThrowNoise;
            g.inHandFunctions += PlayInHandNoise;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Playing Idles
        if (Time.time > nextVoiceTime)
        {
            voice.PlayAction(defaultState.ToString(), source);
            SetNextVoiceTime();
        }
    }

    //Functions
    
    private void SetNextVoiceTime()
    {
        nextVoiceTime = Time.time + Random.Range(voiceDelayMin, voiceDelayMax);
        if (source!= null && source.clip != null)
        {
            nextVoiceTime += source.clip.length;
        }
    }

    public void PlayInHandNoise()
    {
        if (Time.time > nextVoiceTime)
        {
            voice.PlayAction(BallState.InHand.ToString(), source);
            SetNextVoiceTime();
        }
        
    }

    public void PlayGrabNoise()
    {
        voice.PlayAction(BallState.OnGrab.ToString(), source);
        SetNextVoiceTime();
    }

    public void PlayThrowNoise()
    {
        voice.PlayAction(BallState.OnThrow.ToString(), source);
        SetNextVoiceTime();
    }

    public void OnHMDViewEnter()
    {
        if (Time.time > nextVoiceTime)
        {
            VR_Grabbable g;
            if (g = GetComponent<VR_Grabbable>())
            {
                if (g.isPickedUp == false)
                {
                    voice.PlayAction(BallState.OnViewEnter.ToString(), source);
                    SetNextVoiceTime();
                }
            }
            else
            {
                voice.PlayAction(BallState.OnViewEnter.ToString(), source);
                SetNextVoiceTime();
            }
            
        }
    }

    public void OnHMDViewStay()
    {
        if (Time.time > nextVoiceTime)
        {
            VR_Grabbable g;
            if (g = GetComponent<VR_Grabbable>())
            {
                if (g.isPickedUp == false)
                {
                    voice.PlayAction(BallState.OnViewEnter.ToString(), source);
                    SetNextVoiceTime();
                }
            }
            else
            {
                voice.PlayAction(BallState.OnViewEnter.ToString(), source);
                SetNextVoiceTime();
            }
            
        }
    }

    public void OnHMDViewExit()
    {

        if (Time.time > nextVoiceTime)
        {
            VR_Grabbable g;
            if (g = GetComponent<VR_Grabbable>())
            {
                if (g.isPickedUp == false)
                {
                    voice.PlayAction(BallState.OnViewExit.ToString(), source);
                    SetNextVoiceTime();
                }
            }
            else
            {
                voice.PlayAction(BallState.OnViewExit.ToString(), source);
                SetNextVoiceTime();
            }

        }

    }
}
