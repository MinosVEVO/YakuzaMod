using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YakuzaMod.MonoBehaviours
{
    public class StaminanRoyale : GrabbableObject
    {
        public AudioClip use;
        public AudioSource audio;
        public int uses = 0;

        public override void Start()
        {
            audio = GetComponent<AudioSource>();
            base.Start();
        }
        public override void DiscardItem()
        {
            playerHeldBy.activatingItem = false;
            base.DiscardItem();
        }

        public override void ItemActivate(bool used, bool buttonDown = true)
        {
            base.ItemActivate(used, buttonDown);
            
            if(Mouse.current.leftButton.isPressed)
            {
                int health = playerHeldBy.health;
                if (playerHeldBy.health >= health)
                {
                    Debug.Log("Cant use staminan");
                    return;
                }
                audio.PlayOneShot(use);
                int healValue = 100;
                int potentialHealth = playerHeldBy.health + 100;
                if(potentialHealth > playerHeldBy.health)
                {
                    healValue -= potentialHealth - health;
                }
                playerHeldBy.DamagePlayer(-healValue, false, true, CauseOfDeath.Unknown, 0, false, Vector3.zero);
                if(playerHeldBy.health >= 20)
                {
                    playerHeldBy.MakeCriticallyInjured(false);
                }
            }
        }
    }
}
