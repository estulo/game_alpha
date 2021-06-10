﻿// IMPORTS
using UnityEngine;

// CLASSES
// Camera script in which the camera follows the player from an overview perspective and reacts to the mouse movements.
public class CameraScript : MonoBehaviour
{
    // ATTRIBUTES
    // Reference to the player associated to the camera.
    public Transform playerController;
    // Reference to the camera offset from the player's position.
    Vector3 offset;
    // Reference to the horizontal speed of the camera movement.
    float speedH = 2.0f;
    // Reference to the vertical speed of the camera movement.
    float speedV = 2.0f;
    // Reference to the horizontal degree of rotation of the camera.
    float horizontalDegree = 0.0f;
    // Reference to the vertical degree of rotation of the camera.
    float verticalDegree = 0.0f;

    // METHODS
    // Start method. Sets the default offset vector.
    void Start() {
        offset= new Vector3(0, 4, -10);
    }
    // Update method. Takes the player's mouse input and updates the player's rotation and the camera's position and rotation accordingly.
    void Update() {
        // Get mouse input axes.
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        // Check if mouse has moved.
        if(inputX != 0 || inputY != 0) {
            // Update references of degrees.
            horizontalDegree += speedH * inputX;
            verticalDegree -= speedV * inputY;
            // Check if vertical degree is above threshold.
            if(Mathf.Abs(verticalDegree) > 30.0f) {
                // Keep vertical degree under 30 degrees.
                verticalDegree = 30 * verticalDegree/Mathf.Abs(verticalDegree);
            }
            // Update camera offset.
            offset.x = -10*Mathf.Sin(horizontalDegree*Mathf.PI/180)*Mathf.Cos(verticalDegree*Mathf.PI/180);
            offset.y = 4+10*Mathf.Sin(verticalDegree*Mathf.PI/180);
            offset.z = -10*Mathf.Cos(horizontalDegree*Mathf.PI/180)*Mathf.Cos(verticalDegree*Mathf.PI/180);
        }
        // Update camera rotation.
        transform.eulerAngles = new Vector3(verticalDegree, transform.eulerAngles.y, 0.0f);
        // Check if player exists.
        if(playerController!=null) {
            // Update player rotation and camera position.
            playerController.eulerAngles = new Vector3(0.0f, horizontalDegree, 0.0f);
            transform.position = playerController.position + offset;
        }        
    }
}