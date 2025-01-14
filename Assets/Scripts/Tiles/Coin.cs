using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Tiles
{
    public class Coin : MonoBehaviour
    {
        public AudioClip tokenCollectAudio;
        internal bool collected = false;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }
        
        void OnPlayerEnter(PlayerController player)
        {
            if (collected) return;
            
            // Instead of destroying an object, it is inactive, so that it can be reactivated when the character regenerates
            gameObject.SetActive(false);
            
            //send an event into the gameplay system to perform some behaviour.
            var ev = Schedule<PlayerCoinCollision>();
            ev.coin = this;
            ev.player = player;
        }
    }
}