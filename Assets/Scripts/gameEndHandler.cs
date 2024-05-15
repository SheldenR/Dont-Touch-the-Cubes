using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameEndHandler : MonoBehaviour
{
    private float endScreenStartTime;
    private bool newStartTime = true;
    private int distance;
    public Transform player;
    public AudioSource deathSound;
    public Text distanceHS;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= Screen.currentResolution.height/2)
        {
            if (newStartTime){
                deathSound.Play();
                endScreenStartTime = Time.time;
                distance = Mathf.RoundToInt(Mathf.Abs(player.position.y / 2.5f));
                if (distance > PlayerPrefs.GetInt("DistanceHS")){
                    PlayerPrefs.SetInt("DistanceHS", distance);
                }
                distanceHS.text = PlayerPrefs.GetInt("DistanceHS").ToString() + "m Fallen";
                newStartTime = false;
            }

            if ((endScreenStartTime + 2f) < Time.time && Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Escape)){
                    Application.Quit();
                    Debug.Log("Application Has Quit");
                }
                else{
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
