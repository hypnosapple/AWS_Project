using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        public PlayerController player;

        public override void Execute()
        {
            if (player.audioSource && player.deadAudio)
                player.audioSource.PlayOneShot(player.deadAudio);
            
            // disable enput
            player.controlEnabled = false;
            
            // TODO: Animation and other special effects ...
            
            Simulation.Schedule<PlayerRespawn>(2).player = player;
        }
    }
}