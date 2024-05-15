using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool arcadeButtons; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        arcadeButtons = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X);

        if (Time.timeSinceLevelLoad > 1f && arcadeButtons){
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
            Debug.Log("Application Has Quit");
        }
    }
}
