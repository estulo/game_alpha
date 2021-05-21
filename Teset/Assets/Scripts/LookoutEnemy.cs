using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookoutEnemy : MonoBehaviour
{

    public float rotation = 0.0f;
    public float rotationSpeed = 2.0f;
    public Rigidbody bullet;
    int layerMask = 1 << 8;

    // Update is called once per frame
    void Update()
    {
        rotation = (rotation + rotationSpeed)%360;
        this.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward);
        if(Physics.Raycast(this.transform.position, this.transform.forward, Mathf.Infinity, layerMask)) {
            Rigidbody bulletClone;
            bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            bulletClone.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
        else {}
    }
}
