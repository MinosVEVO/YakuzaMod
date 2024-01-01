using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YakuzaMod.MonoBehaviours
{
    public class PlushScrap : GrabbableObject
    {
        public AudioSource noiseAudio;
        public AudioSource noiseAudioFar;

        [Space(3f)]
        public float noiseRange;

        private bool musicPlaying = false;
        private RoundManager roundManager;
        public float noiseInterval = 1f;
        public int timesPlayedWithoutTurningOff = 0;
        public override void Start()
        {
            base.Start();
            roundManager = FindObjectOfType<RoundManager>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                musicPlaying = true;
            }
        }

        public override void ItemActivate(bool used, bool buttonDown = true)
        {
            base.ItemActivate(used, buttonDown);
            if(!(GameNetworkManager.Instance.localPlayerController == null))
            {
                if (IsOwner)
                {
                    musicPlaying = !musicPlaying;
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (musicPlaying)
            {
                if (!noiseAudio.isPlaying)
                {
                    noiseAudio.Play();
                    noiseAudioFar.Play();
                }

                if (noiseInterval <= 0f)
                {
                    noiseInterval = 1f;
                    timesPlayedWithoutTurningOff++;
                    roundManager.PlayAudibleNoise(transform.position, 16f, 0.9f, timesPlayedWithoutTurningOff, noiseIsInsideClosedShip: false, 5);
                }
                else
                {
                    noiseInterval -= Time.deltaTime;
                }
            }
            else
            {
                timesPlayedWithoutTurningOff = 0;
                if(noiseAudio.isPlaying)
                {
                    noiseAudio.Pause();
                    noiseAudioFar.Pause();
                }
            }
        }
    }
}