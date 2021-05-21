﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;



    // Use this for initialization
    void Start()
    {
        offset= new Vector3(0, 5, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if(yaw != yaw + Input.GetAxis("Mouse X")) {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            offset.x = -10*Mathf.Sin(yaw*Mathf.PI/180);
            offset.z = -10*Mathf.Cos(yaw*Mathf.PI/180);
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        if(player!=null) {
            player.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
            transform.position = player.position + offset;
        }
    }
}