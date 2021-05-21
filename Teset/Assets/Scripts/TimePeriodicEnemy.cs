using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePeriodicEnemy : MonoBehaviour, IEnemy
{
    public GameObject enemy;
    public Material enemyMaterial;
    public Rigidbody bullet;
    public float currentHealth {get; set;}
    public float maxHealth {get;} = 100.0f;
    float rotation = 0.0f;
    float rotationSpeed = 2.0f;
    float timer = 0.0f;
    float shotFrequency = 0.1f;

    void Start() {
        currentHealth = 100.0f;
        enemyMaterial.color = new Color(0.5f,0.5f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>shotFrequency) {
            timer -= shotFrequency;
            shoot();
        }
        rotation = (rotation + rotationSpeed)%360;
        this.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name.Contains("Bullet")){
            Destroy(collision.gameObject);
            receiveDamage(collision.gameObject.GetComponent<Bullet>().damage);
        }
    }

    public void receiveDamage(float damageReceived) {
        currentHealth -= damageReceived;
        float newColorValue = enemyMaterial.color.b+0.4f*(damageReceived/maxHealth);
        enemyMaterial.color = new Color(0.5f, 0.5f, newColorValue);
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
