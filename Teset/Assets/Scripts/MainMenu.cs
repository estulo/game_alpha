// IMPORTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// CLASSES
// Main menu script for button behavior.
public class MainMenu : MonoBehaviour
{
    // METHODS
    // Play button behavior.
    public void PlayGame() {
        // Loads next scene in queue defined in the build.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Loads scene defined with the name provided.
        //SceneManager.LoadScene("SampleScene");
    }
    // Quit button behavior.
    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();

    }
}
