using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YakuzaMod.MonoBehaviours
{
    public class Kiryu : GrabbableObject
    {
        public override void Start()
        {
            base.Start();
            noisemakerRandom = new System.Random(StartOfRound.Instance.randomMapSeed + 85);
        }

        public override void ItemActivate(bool used, bool buttonDown = true)
        {
            base.ItemActivate(used, buttonDown);
            if (!(GameNetworkManager.Instance.localPlayerController == null))
            {
                if(!musicPlaying)
                {
                    musicPlaying = true;
                    noiseAudio.PlayOneShot(noiseSFX[0]);
                    if (noiseAudioFar != null)
                    {
                        noiseAudioFar.PlayOneShot(noiseSFXFar[0]);
                        WalkieTalkie.TransmitOneShotAudio(noiseAudio, noiseSFX[0]);
                        RoundManager.Instance.PlayAudibleNoise(base.transform.position, noiseRange, 100, 0, isInElevator && StartOfRound.Instance.hangarDoorsClosed);
                    }
                }
                else if(musicPlaying)
                {
                    musicPlaying = false;
                    noiseAudio.Pause();
                    if(noiseAudioFar != null)
                    {
                        noiseAudio.Pause();
                    }
                }
            }

        }

        public AudioSource noiseAudio;

        public AudioSource noiseAudioFar;

        [Space(3f)]
        public AudioClip[] noiseSFX;

        public AudioClip[] noiseSFXFar;

        [Space(3f)]
        public float noiseRange;
        private bool musicPlaying = false;

        private System.Random noisemakerRandom;

    }
}