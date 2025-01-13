using System;
using CheckPoints;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerRespawn : Simulation.Event<PlayerRespawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            // Regenerate the scene objects
            var respawnManager = UnityEngine.Object.FindObjectOfType<RespawnManager>();
            if (respawnManager != null)
            {
                respawnManager.RespawnAll();
                Debug.Log("RespawnAll called successfully.");
            }
            else
            {
                Debug.LogWarning("RespawnManager not found in the scene.");
            }
        }
    }
}