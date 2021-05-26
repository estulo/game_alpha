// IMPORTS.
using UnityEngine;

// CLASSES
// Enemy script in which the game object rotates at a consant speed and shoots a projectile periodicaly depending on the object's angle.
public class AnglePeriodicEnemy : MonoBehaviour, IEnemy
{
    // ATTRIBUTES
    // Reference for game object to act upon.
    public GameObject enemy;
    // Reference for object material to modify.
    public Material enemyMaterial;
    // Reference for projectile to shoot.
    public Rigidbody bullet;
    // Reference for enemy's current health.
    float currentHealth;
    //  Reference for enemy's max health for editing in scene.
    public float maxHealth;
    // Reference for enemy's rotation angle.
    float rotation = 0.0f;
    // Reference for enemy's rotation speed(angles per frame).
    float rotationSpeed = 2.0f;
    // Reference for enemy's angle since last shot.
    float shotAngle = 0.0f;
    // Reference for enemy's shot frequency(shots per rotation). 
    float shotFrequency = 8.0f;
    // Reference for color value to change.
    float colorValue;

    // METHODS
    // Start method. Sets current health to the max health and sets material color to its defauld value.
    void Start() {
        currentHealth = maxHealth;
        enemyMaterial.color = new Color(0.1f,0.1f,0.5f);
    }
    /**
    * Update method. Rotation and shotAngle are updated according to the rotation speed.
    Game object's rotation is modified. Bullet is shot if criteria is met.
    */
    void Update()
    {
        // Check if bullet needs to be shot depending on shotAngle and shotFrequency.
        if(shotAngle>360/shotFrequency) {
            shoot();
            // Shot angle is kept under the angle determined by shotFrequency.
            shotAngle = shotAngle%(360/shotFrequency);
        }
        // Rotation is kept under 360 degrees.
        rotation = (rotation + rotationSpeed)%360;
        shotAngle += rotationSpeed;
        // Enemy game object's rotation is changed.
        enemy.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
        // Line drawn to show enemy direction in game.
        Debug.DrawLine(enemy.transform.position, enemy.transform.position + enemy.transform.forward);
    }
    // OnCollisionEnter method. Detects new collisions and generates a response in the game object.
    void OnCollisionEnter(Collision collision) {
        // If the collision is a bullet, the game object receives damage.
        if(collision.gameObject.name.Contains("Bullet")){
            receiveDamage(collision.gameObject.GetComponent<IProjectile>().damage);
        }
    }
    // Changes game object's health attributes according to damageReceived.
    public void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        // New color value is determined depending on the current color and the damage received.
        colorValue = enemyMaterial.color.r+0.4f*(damageReceived/maxHealth);
        enemyMaterial.color = new Color(colorValue, colorValue, 0.5f);
        // Checks if current health has reached 0 and destroys the game object.
        if(currentHealth <= 0) {
            Destroy(enemy);
            // Sets reference to enemy instance to null.
            enemy = null;
        }
    }
    // Creates a new bullet and shoots straight ahead.
    void shoot() {
        Rigidbody bulletClone;
        bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        // Damage is set for the bullet component asociated to the rigidbody.
        bulletClone.GetComponent<Bullet>().damage = 10;
        // Direction is given to the bullet's rigidbody.
        bulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
    }
}
