using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    public float damage {get; set;}
    
    // Start is called before the first frame update

    void Start()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
