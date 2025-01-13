using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using UnityEngine.UI;
using TMPro;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        // public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        // internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        //NEW: coin,heart,NPC
        public TMP_Text coinText;         
        public TMP_Text heartText;       
        public GameObject dialogueBox; 
        public Text dialogueText;

        //variables
        private int coins = 0;        
        private int hearts = 3;       
        private bool nearNPC = false; 
        private string npcDialogue = "";
        private Rigidbody2D rb;


        void Awake()
        {
            // health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            // animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }

            // NEW: NPC Interaction
            if (nearNPC && Input.GetKeyDown(KeyCode.F))
            {
                dialogueBox.SetActive(!dialogueBox.activeSelf);
                dialogueText.text = npcDialogue;
            }

            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // animator.SetBool("grounded", IsGrounded);
            // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        //NEW:Coins and obstacles
        // For triggers like coins and NPCs
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Collect Coins
            if (collision.gameObject.CompareTag("Coin"))
            {
                coins++;
                UpdateUI();
                // Instead of destroying an object, it is deactive, so that it can be reactivated when the character regenerates
                collision.gameObject.SetActive(false);
            }

            // Detect NPC
            if (collision.gameObject.CompareTag("NPC"))
            {
                nearNPC = true;

                //show npc dialogue
                NPC npc = collision.gameObject.GetComponent<NPC>();
                npc.ShowDialogue();
            }

            // DeadZone
            if (collision.gameObject.CompareTag("DeadZone"))
            {
                Debug.Log("Player entered the DeadZone!");
                hearts = 0;
                Respawn(true); 
                hearts = 3; 
                UpdateUI(); 
            }

            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Player hit an obstacle!");
                hearts--;
                UpdateUI();

                if (hearts > 0)
                {
                    Respawn(false); // Respawn at the most recent checkpoint
                }
                else
                {
                    Debug.Log("Player ran out of hearts!");
                    Respawn(true); // Respawn at the first checkpoint

                    // Refill hearts to the initial value (e.g., 3)
                    hearts = 3;
                    UpdateUI(); // Update the UI to reflect the refilled hearts
                }
            }

        }

        //RESPAWN
        private void Respawn(bool isHeartZero)
        {
            Simulation.Schedule<PlayerRespawn>(0.1f);
            Vector2 respawnPoint = CheckpointManager.Instance.GetRespawnPoint(isHeartZero);

            // Add an offset to the Y-axis to position the player above the checkpoint
            float yOffset = 2.0f; // Adjust this value based on your game's scale
            Vector2 adjustedRespawnPoint = new Vector2(respawnPoint.x, respawnPoint.y + yOffset);

            transform.position = adjustedRespawnPoint; // Move player to the adjusted respawn point
            Debug.Log("Player respawned at: " + adjustedRespawnPoint);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                nearNPC = false;

                // Hide NPC dialogue
                NPC npc = collision.gameObject.GetComponent<NPC>();
                npc.HideDialogue();
            }
        }

        void UpdateUI()
        {
            coinText.text = "Coins: " + coins;
            heartText.text = "Hearts: " + hearts;
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}