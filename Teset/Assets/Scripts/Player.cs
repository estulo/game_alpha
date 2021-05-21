using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public GameObject player;
    public CameraScript camera;
    public Material playerMaterial;
    //private CharacterController controller;
    Vector3 playerVelocity;
    bool groundedPlayer;
    float playerSpeed;
    float jumpForce;
    Vector3 jumpVector;
    bool isGrounded;
    Rigidbody rb;
    int health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSpeed = 5.0f;
        jumpForce = 2.0f;
        jumpVector = new Vector3(0.0f, 1.0f, 0.0f);
        isGrounded = true;
        health = 50;
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
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.Contains("Bullet")){
            Destroy(collision.gameObject);
            health -= 5;
            print("health: " + health);
            float newColorValue = playerMaterial.color.b+0.2f*(5.0f/50.0f);
            playerMaterial.color = new Color(newColorValue,0.3f,newColorValue);
            if(health <= 0) {
                Destroy(player);
                player = null;
                camera.player = null;
            }
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
}