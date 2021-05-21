using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglePeriodicEnemy : MonoBehaviour, IEnemy
{
    public GameObject enemy;
    public Material enemyMaterial;
    public Rigidbody bullet;
    public float currentHealth {get; set;}
    public float maxHealth {get;} = 100.0f;
    float rotation = 0.0f;
    float rotationSpeed = 2.0f;
    float shotAngle = 0.0f;
    float shotFrequency = 8.0f;

    void Start() {
        currentHealth = 100.0f;
        enemyMaterial.color = new Color(0.1f,0.1f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        rotation = (rotation + rotationSpeed)%360;
        shotAngle += rotationSpeed;
        this.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward);
        if(shotAngle>360/shotFrequency) {
            shotAngle = shotAngle%(360/shotFrequency);
            shoot();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.Contains("Bullet")){
            Destroy(collision.gameObject);
            receiveDamage(collision.gameObject.GetComponent<Bullet>().damage);
        }
    }

    public void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        float newColorValue = enemyMaterial.color.r+0.4f*(damageReceived/maxHealth);
        enemyMaterial.color = new Color(newColorValue, newColorValue, 0.5f);
        if(currentHealth <= 0) {
            Destroy(enemy);
            enemy = null;
        }
    }

    void shoot() {
        Rigidbody bulletClone;
        bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
        bulletClone.GetComponent<Bullet>().damage = 10;
        bulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
    }
}
