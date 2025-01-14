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
        public PlayerController player;

        public override void Execute()
        {
            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);
            
            // reset player position
            Vector2 respawnPoint = CheckpointManager.Instance.GetRespawnPoint(player.GetHearts() <= 0);

            // Add an offset to the Y-axis to position the player above the checkpoint
            float yOffset = 2.0f; // Adjust this value based on your game's scale
            Vector2 adjustedRespawnPoint = new Vector2(respawnPoint.x, respawnPoint.y + yOffset);
            player.transform.position = adjustedRespawnPoint; // Move player to the adjusted respawn point
            
            // reset status
            if (player.GetHearts() <= 0)
            {
                player.SetHearts(3); 
            }
            player.UpdateUI();
            
            // enable enput
            player.controlEnabled = true;
            player.IsDead = false;
            
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