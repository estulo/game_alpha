using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglePeriodicEnemy : MonoBehaviour
{
    public Rigidbody bullet;
    float rotation = 0.0f;
    float rotationSpeed = 2.0f;
    float shotAngle = 0.0f;
    float shotFrequency = 8.0f;

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
}
