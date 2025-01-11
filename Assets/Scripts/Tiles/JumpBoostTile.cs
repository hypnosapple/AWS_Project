using Platformer.Mechanics;
using System.Collections;
using UnityEngine;

public class JumpBoostTile : MonoBehaviour
{
    public float jumpBoostMultiplier = 1.5f; 
    public float boostDuration = 0.5f; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
         //Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player landed on the jump boost square!");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            //if (player != null)
            //{
                //Debug.Log("PlayerController component found! Applying jump boost.");
                StartCoroutine(ApplyJumpBoost(player));
            //}
            
        }
    }

    private IEnumerator ApplyJumpBoost(PlayerController player)
    {
        // Temporarily increase the player's jumpTakeOffSpeed
        float originalJumpSpeed = player.jumpTakeOffSpeed;
        player.jumpTakeOffSpeed *= jumpBoostMultiplier;
        Debug.Log("Boost applied: New jumpTakeOffSpeed = " + player.jumpTakeOffSpeed);

        // Wait for the boost duration
        yield return new WaitForSeconds(boostDuration);

        // Reset to original jumpTakeOffSpeed
        player.jumpTakeOffSpeed = originalJumpSpeed;
        Debug.Log("Boost reset: jumpTakeOffSpeed = " + player.jumpTakeOffSpeed);
    }
}
