// IMPORTS
using UnityEngine;
using UnityEngine.SceneManagement;

// CLASSES
public class PlayerInteractionArea : MonoBehaviour
{
    // ATTRIBUTES
    bool interacting = false;
    string interactingObject = "";

    void Update() {
         if(Input.GetKey(KeyCode.F)) {
            if(interactingObject == "Door") {
                SceneManager.LoadScene("HouseScene");
            }
            if(interactingObject.Contains("Enemy")) {
                print("This is an enemy.");
            }
            
        }
    }
    
    // METHODS
    private void OnTriggerEnter(Collider collider) {
        if(!interacting && collider.gameObject.name.Contains("Enemy")) {
            interacting = true;
            interactingObject = collider.gameObject.name;
        }
        if(!interacting && collider.gameObject.name.Contains("Door")) {
            interacting = true;
            interactingObject = collider.gameObject.name;
        }
    }

    private void OnTriggerExit(Collider collider) {
            interacting = false;
            interactingObject = "";
    }
}
