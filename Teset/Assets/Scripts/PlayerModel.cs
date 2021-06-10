// IMPORTS
using UnityEngine;

// CLASSES
// Requirement of rigidbody for the creation of the instance.
// Player game object script.
public class PlayerModel : MonoBehaviour
{
    // ATTRIBUTES
    // Reference to the animator component to trigger player animations.
    Animator animator;

    // METHODS
    /**
    * Start method.
    * Gets animator component.
    */
    void Start() {
        animator = GetComponent<Animator>();
    }
    // Update method.
    void Update() {
    }
    // Sets health for animation parameter.
    public void receiveDamage(float currentHealth) {
        animator.SetFloat("Health", currentHealth);
    }
    public void selfDestruct() {
        Destroy(gameObject);
    }
}