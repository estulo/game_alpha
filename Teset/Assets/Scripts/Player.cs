using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public GameObject player;
    public Material playerMaterial;
    //private CharacterController controller;
    public Rigidbody bullet;

    float playerSpeed;
    float jumpForce;
    Vector3 jumpVector;
    bool isGrounded;
    Rigidbody rb;
    float currentHealth;
    float maxHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSpeed = 5.0f;
        jumpForce = 2.0f;
        jumpVector = new Vector3(0.0f, 1.0f, 0.0f);
        isGrounded = true;
        maxHealth = 100.0f;
        currentHealth = 100.0f;
        playerMaterial.color = new Color(0.1f,0.3f,0.1f);
    }

    void Update()
    {
        if(player!= null) {
            if(Input.GetKey(KeyCode.W)) {
                player.transform.position += player.transform.forward * Time.deltaTime * playerSpeed;
            }
            if(Input.GetKey(KeyCode.S)) {
                player.transform.position -= player.transform.forward * Time.deltaTime * playerSpeed;
            }
            if(Input.GetKey(KeyCode.A)) {
                player.transform.position -= player.transform.right * Time.deltaTime * playerSpeed;
            }
            if(Input.GetKey(KeyCode.D)) {
                player.transform.position += player.transform.right * Time.deltaTime * playerSpeed;
            }
            if(Input.GetKey(KeyCode.Space) && isGrounded) {
                rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
            if(Input.GetMouseButtonDown(0)) {
                shoot();
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.Contains("Bullet")){
            Destroy(collision.gameObject);
            receiveDamage(collision.gameObject.GetComponent<Bullet>().dealDamage());
        }
    }
    void OnCollisionStay(Collision collision) {
        if(collision.gameObject.tag == "Terrain") {
            isGrounded = true;
        }
    }
 
    void OnCollisionExit(Collision collision) {
        if(collision.gameObject.tag == "Terrain") {
            isGrounded = false;
        }
    }

    void shoot() {
        Rigidbody bulletClone;
        bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        bulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
    }

    void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        print("currentHealth: " + currentHealth);
        float newColorValue = playerMaterial.color.b+0.2f*(damageReceived/maxHealth);
        playerMaterial.color = new Color(newColorValue,0.3f,newColorValue);
        if(currentHealth <= 0) {
            Destroy(player);
            player = null;
        }
    }
}