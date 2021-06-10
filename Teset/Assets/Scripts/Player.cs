// IMPORTS
using UnityEngine;

// CLASSES
// Requirement of rigidbody for the creation of the instance.
// Player game object script.
public class Player : MonoBehaviour
{
    // ATTRIBUTES
    // Reference for enemy's current health.
    PlayerController playerController;
    public float currentHealth {get; set;}
    // Reference for enemy's maximum health.
    public float maxHealth;
    // Reference for object material to modify.
    public Material playerMaterial;
    // Reference to determine whether the player is standing on th ground or not.
    public bool isGrounded {get; set;}
    // Reference to the animator component to trigger player animations.
    Animator animator;

    // METHODS
    /**
    * Start method.
    * Sets current health to the max health and sets player's material default color.
    * Gets animator component.
    */
    void Start() {
        currentHealth = maxHealth;
        playerMaterial.color = new Color(0.1f,0.3f,0.1f);
        isGrounded = true;
        animator = GetComponent<Animator>();
    }
    // Update method.
    void Update() {
    }
    // Changes player's current health and updates player material's color.
    public void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        // New color value is determined depending on the current color and the damage received.
        float newColorValue = playerMaterial.color.b+0.2f*(damageReceived/maxHealth);
        playerMaterial.color = new Color(newColorValue,0.3f,newColorValue);
        animator.SetFloat("Health", currentHealth);
    }
    public void selfDestruct() {
        Destroy(gameObject);
    }
}