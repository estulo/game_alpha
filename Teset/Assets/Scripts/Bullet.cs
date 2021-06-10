// IMPORTS
using UnityEngine;

// CLASSES
// Bullet script with damage getter, setter, and destroy function.
public class Bullet : MonoBehaviour, IProjectile
{
    // METHOD
    // Method for setting and getting hidden damage attribute.
    public float damage {get; set;}
    
    // Start function. Sets the bullet duration to two seconds.
    void Start()
    {
        // Destroys the game object 2 seconds after loading the object
        Destroy(gameObject, 2);
    }
    // OnCollisionEnter method. Destroys the game object as soon as it collides with any other collider.
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag != "Bullet") {
            Destroy(gameObject);
        }
    }

}
