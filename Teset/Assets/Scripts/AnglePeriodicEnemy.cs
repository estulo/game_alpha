using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglePeriodicEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Material enemyMaterial;
    public Rigidbody bullet;
    float rotation = 0.0f;
    float rotationSpeed = 2.0f;
    float shotAngle = 0.0f;
    float shotFrequency = 8.0f;
    float currentHealth;
    float maxHealth;

    void Start() {
        maxHealth = 100.0f;
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
            Rigidbody bulletClone;
            bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            bulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.Contains("Bullet")){
            Destroy(collision.gameObject);
            receiveDamage(collision.gameObject.GetComponent<Bullet>().dealDamage());
        }
    }

    void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        print("currentHealth: " + currentHealth);
        float newColorValue = enemyMaterial.color.r+0.4f*(damageReceived/maxHealth);
        enemyMaterial.color = new Color(newColorValue, newColorValue, 0.5f);
        if(currentHealth <= 0) {
            Destroy(enemy);
            enemy = null;
        }
    }
}
