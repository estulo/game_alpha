using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
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
    float shotTime;
    int damageMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSpeed = 5.0f;
        jumpForce = 3.0f;
        jumpVector = new Vector3(0.0f, 1.0f, 0.0f);
        isGrounded = true;
        maxHealth = 100.0f;
        currentHealth = 100.0f;
        playerMaterial.color = new Color(0.1f,0.3f,0.1f);
        shotTime = 0.0f;
        damageMultiplier = 20;
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
            if(Input.GetMouseButton(0)) {
                shotTime += Time.deltaTime;
            }
            if(Input.GetMouseButtonUp(0)) {
                shoot();
                shotTime = 0.0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.Contains("Bullet")){
            Destroy(collision.gameObject);
            receiveDamage(collision.gameObject.GetComponent<Bullet>().damage);
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
        float distance = shotTime>1? shotTime:1;
        Vector3 bulletSize = new Vector3(1,1,1);
        Rigidbody bulletClone;
        bulletClone = Instantiate(bullet, player.transform.position + distance*player.transform.forward, player.transform.rotation);
        bulletClone.GetComponent<Transform>().localScale = shotTime*bulletSize;
        bulletClone.GetComponent<Bullet>().damage = damageMultiplier*shotTime;
        bulletClone.velocity = player.transform.TransformDirection(Vector3.forward * 10);
    }

    void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        float newColorValue = playerMaterial.color.b+0.2f*(damageReceived/maxHealth);
        playerMaterial.color = new Color(newColorValue,0.3f,newColorValue);
        if(currentHealth <= 0) {
            Destroy(player);
            player = null;
        }
    }
}