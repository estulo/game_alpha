using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePeriodicEnemy : MonoBehaviour
{
    public Rigidbody bullet;
    float rotation = 0.0f;
    float rotationSpeed = 2.0f;
    float timer = 0.0f;
    float shotFrequency = 0.1f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>shotFrequency) {
            timer -= shotFrequency;
            Rigidbody bulletClone;
            bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            bulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
        rotation = (rotation + rotationSpeed)%360;
        this.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward);
    }
}
