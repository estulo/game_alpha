// IMPORTS
using UnityEngine;

// CLASSES
// Requirement of rigidbody for the creation of the instance.
[RequireComponent(typeof(Rigidbody))]
// Player controller script in which the game object responds to the player's input.
public class PlayerController : MonoBehaviour
{
    // ATTRIBUTES
    // Reference for game object to act upon.
    public GameObject player;
    // Reference for object material to modify.
    public Material playerMaterial;
    // Reference for projectile to shoot.
    public Rigidbody bullet;
    // Reference for the player's speed
    float playerSpeed;
    // Reference for the player's jump distance.
    float jumpForce;
    // Reference for the player's jump vector.
    Vector3 jumpVector;
    // Reference to determine whether the player is standing on th ground or not.
    bool isGrounded;
    // Reference to game object's rigidbody component.
    Rigidbody playerRigidBody;
    // Reference for enemy's current health.
    float currentHealth;
    // Reference for enemy's rotation angle.
    public float maxHealth;
    // Reference for the amount of time the player has held shoot button down.
    float shotTime;
    // Reference for the player's shot damage multiplier.
    int damageMultiplier;
    // Reference for the minimum distance required to shoot a projectile correctly.
    float minDistance;

// METHODS
    /**
    * Start method.
    * Sets current health to the max health and sets material color to its defauld value.
    * Finds rigidbody component assosiated with game object.
    * Sets player speed and jump force.
    * Sets jump vector, grounded, shot time and damage multiplier.
    */
    void Start()
    {
        currentHealth = maxHealth;
        playerMaterial.color = new Color(0.1f,0.3f,0.1f);
        playerRigidBody = GetComponent<Rigidbody>();
        playerSpeed = 5.0f;
        jumpForce = 3.0f;
        jumpVector = new Vector3(0.0f, 1.0f, 0.0f);
        isGrounded = true;
        shotTime = 0.0f;
        damageMultiplier = 20;
    }
    /**
    * Update method. Position is updated according to the player's input.
    * Bullet is shot depending on the player's input.
    */
    void Update()
    {
        if(player!= null) {
            // Action performed when W key is pressed.
            if(Input.GetKey(KeyCode.W)) {
                player.transform.position += player.transform.forward * Time.deltaTime * playerSpeed;
            }
            // Action performed when S key is pressed.
            if(Input.GetKey(KeyCode.S)) {
                player.transform.position -= player.transform.forward * Time.deltaTime * playerSpeed;
            }
            // Action performed when A key is pressed.
            if(Input.GetKey(KeyCode.A)) {
                player.transform.position -= player.transform.right * Time.deltaTime * playerSpeed;
            }
            // Action performed when D key is pressed.
            if(Input.GetKey(KeyCode.D)) {
                player.transform.position += player.transform.right * Time.deltaTime * playerSpeed;
            }
            // Action performed when space key is pressed and pleyer is grounded.
            if(Input.GetKey(KeyCode.Space) && isGrounded) {
                // Force added to rigidbody to perform jump
                isGrounded = false;
                playerRigidBody.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
            }
            // Action performed when primary mouse button is pressed.
            if(Input.GetMouseButton(0)) {
                shotTime += Time.deltaTime;
            }
            // Action performed when primary mouse button is released.
            if(Input.GetMouseButtonUp(0)) {
                shoot();
                shotTime = 0.0f;
            }
        }
    }

    // OnCollisionEnter method. Detects new collisions and generates a response in the game object.
    void OnCollisionEnter(Collision collision) {
        // If the collision is a bullet, the game object receives damage.
        if(collision.gameObject.name.Contains("Bullet")){
            receiveDamage(collision.gameObject.GetComponent<Bullet>().damage);
        }
    }
    // OnCollisionStay method. Detects persistent collisions and generates a response in the game object.
    void OnCollisionStay(Collision collision) {
        // If the collision is terrain, the game object is set as grounded.
        if(collision.gameObject.tag == "Terrain") {
            isGrounded = true;
        }
    }
    // OnCollisionExit method. Detects ending collisions and generates a response in the game object.
    void OnCollisionExit(Collision collision) {
        // If the collision was terrain, the game object is set as not grounded.
        if(collision.gameObject.tag == "Terrain") {
            isGrounded = false;
        }
    }

    // Creates a new bullet and shoots straight ahead.
    void shoot() {
        // Determines the required distance between the game object and the point where the bullet will be created.
        minDistance = shotTime>1? shotTime:1;
        Rigidbody bulletClone;
        bulletClone = Instantiate(bullet, player.transform.position + minDistance*player.transform.forward, player.transform.rotation);
        // Size of the bullet is changed depending on shotTime.
        bulletClone.GetComponent<Transform>().localScale = shotTime*new Vector3(1,1,1);
        // Damage is set for the bullet component asociated to the rigidbody depending on shotTime.
        bulletClone.GetComponent<Bullet>().damage = damageMultiplier*shotTime;
        // Direction is given to the bullet's rigidbody.
        bulletClone.velocity = player.transform.TransformDirection(Vector3.forward * 10);
    }
    // Changes game object's health attributes according to damageReceived.
    void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        // New color value is determined depending on the current color and the damage received.
        float newColorValue = playerMaterial.color.b+0.2f*(damageReceived/maxHealth);
        playerMaterial.color = new Color(newColorValue,0.3f,newColorValue);
        // Checks if current health has reached 0 and destroys the game object.
        if(currentHealth <= 0) {
            Destroy(player);
            // Sets reference to player instance to null.
            player = null;
        }
    }
}