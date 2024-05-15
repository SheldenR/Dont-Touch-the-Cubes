using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VerticalMovement : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject player;
    public GameObject specialAvailable;
    public GameObject specialVignette;
    private Renderer mainRenderer;
    public AudioSource specialActivateSound;
    public float speed;
    public Text speedText;
    private bool specialKey;
    private int remainingPowerupUses;
    private float specialCooldown;

    void Start()
    {
        speed = 30f; // Starting Speed of 30f
        remainingPowerupUses = 2;
        specialCooldown = 10f;
        speedText.enabled = false;
        mainRenderer = player.GetComponent<Renderer>();
        specialVignette.SetActive(false);
    }

    IEnumerator specialMoveInit(){
        playerMovement.specialActive = true;
        playerMovement.willPhase = true;
        remainingPowerupUses--;
        specialCooldown = 15f;
        mainRenderer.material.color = new Color(0.3f,1f,0.9f,0.5f);
        specialVignette.SetActive(true);
        speed+=5;
        yield return new WaitForSeconds(5);
        playerMovement.specialActive = false;
        playerMovement.willPhase = false;
        mainRenderer.material.color = new Color(1f,1f,1f,0.5f);
        specialVignette.SetActive(false);
        speed-=5;
    }

    void Update()
    {        
        // Movement of the player 
        transform.position += new Vector3(0,-speed*Time.deltaTime,0);

        // Key Mapping for game
        specialKey = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X);

        // Progressive Speed Increase
        if (Time.timeSinceLevelLoad > 40f){
            if (playerMovement.specialActive){
                speed = 60f; 
            } else { speed = 65f; }  
            if (Time.timeSinceLevelLoad < 42.5f){
                speedText.enabled = true;
            } else { speedText.enabled = false; }
        } else if (Time.timeSinceLevelLoad > 25f){
            if (playerMovement.specialActive){
                speed = 50f; 
            } else { speed = 55f; }  
            if (Time.timeSinceLevelLoad < 27.5f){
                speedText.enabled = true;
            } else { speedText.enabled = false; }
        } else if ( Time.timeSinceLevelLoad > 15f){
            if (playerMovement.specialActive){
                speed = 40f; 
            } else { speed = 45f; }  
            if (Time.timeSinceLevelLoad < 17.5f){
                speedText.enabled = true;
            } else { speedText.enabled = false; }
        }

        // Special Powerup Mechanics
        if (specialCooldown > 0f)
        {
            specialCooldown -= Time.deltaTime;
            specialAvailable.SetActive(false);
        }
        else if (specialCooldown <= 0f && remainingPowerupUses >= 1)
        {
            specialAvailable.SetActive(true);
        }

        if (specialKey && remainingPowerupUses >= 1 && specialCooldown <= 0.05f){ //&& specialCooldown <= 0f
            specialActivateSound.Play();
            StartCoroutine(specialMoveInit());
        } 
    }
}