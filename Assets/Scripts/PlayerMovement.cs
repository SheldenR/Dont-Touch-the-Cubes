using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject technicalMayhem;
    public GameObject endScreen;
    public GameObject forceField;
    public HazardHandler handler;
    public ParticleSystem explosionParticles;
    public GameObject gameMovement;
    public AudioSource scoreCollect;
    public Text cubeHS;
    private float speed = 11f;
    private string pural;
    private int score = 0;
    public Text scoreText;
    public Text scoreTextEnd;
    public Text powerupText;
    private int randomPowerup;
    public bool willPhase;
    public bool specialActive;
    public bool goThru;
    private bool gameEnded;
    
    void Start()
    {
        technicalMayhem.SetActive(false);
        endScreen.transform.position = new Vector3(Screen.currentResolution.width/2, Screen.currentResolution.height*2, 0); //Removes it from visibility [Default: 960, 540]
    }

    IEnumerator outputPowerup(string powerup, bool isBuff){
        if (isBuff){
            powerupText.color = new Color(0.65f,1f,0.58f,1f);
        } else { powerupText.color = new Color(1f,0.48f,0.45f,1f); }
        powerupText.text = "You got " + powerup;
        yield return new WaitForSeconds(2);
        powerupText.text = ("");
    }

    IEnumerator techMayhemInit(){
        technicalMayhem.SetActive(true);
        yield return new WaitForSeconds(3);
        technicalMayhem.SetActive(false);
    }

    IEnumerator tripMatrixInit(){
        handler.tripwireMatrix = true;
        yield return new WaitForSeconds(2);
        handler.tripwireMatrix = false;
    }

    IEnumerator phaseInit(){
        willPhase = true;
        goThru = true;
        forceField.SetActive(true);
        yield return new WaitForSeconds(5);
        willPhase = false;
        goThru = false;
        forceField.SetActive(false);
    }

    IEnumerator growingInit(){
        transform.localScale = Vector3.one * 1.2f;
        yield return new WaitForSeconds(3);
        transform.localScale = Vector3.one;
    }

    IEnumerator shrinkInit(){
        transform.localScale = Vector3.one / 1.4f;
        yield return new WaitForSeconds(5);
        transform.localScale = Vector3.one;
    }

    IEnumerator hazardBreakInit() {
        handler.spawning = false;
        yield return new WaitForSeconds(3);
        handler.spawning = true;

    }

    void Update()
    {
        // General movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement * speed * Time.deltaTime);

        // Prevent movement outside stage
        if (transform.position.z > 6.4f){
            transform.position = new Vector3(transform.position.x, transform.position.y, 6.4f);
        }
        if (transform.position.z < -6.4f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -6.4f);
        }
        if (transform.position.x > 6.4)
        {
            transform.position = new Vector3(6.4f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -6.4f)
        {
            transform.position = new Vector3(-6.4f, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Mystery Box(Clone)"){ // If player collects mystery box
            collision.gameObject.SetActive(false);
            scoreCollect.Play();
            randomPowerup = Random.Range(1,7); // [growing, shrinking, phasing, technical mayhem, tripwire matrix, hazard break]
            
            if (randomPowerup == 1){ // Growing
                StartCoroutine(growingInit());
                StartCoroutine(outputPowerup("Bigger", false));
            }

            if (randomPowerup == 2){ // Shrinking
                StartCoroutine(shrinkInit());
                StartCoroutine(outputPowerup("Smaller", true));
            }

            if (randomPowerup == 3){ // Phasing
                StartCoroutine(phaseInit());
                StartCoroutine(outputPowerup("Phasing", true));
            }

            if (randomPowerup == 4){ // Technical Mayhem
                StartCoroutine(techMayhemInit());
                StartCoroutine(outputPowerup("Technical Mayhem", false));
            }

            if (randomPowerup == 5){ // Tripwire Matrix
                StartCoroutine(tripMatrixInit());
                StartCoroutine(outputPowerup("Tripwire Matrix", false));
            }

            if (randomPowerup == 6){ // Hazard Break
                StartCoroutine(hazardBreakInit());
                StartCoroutine(outputPowerup("Hazard Break", true));
            }
        }

        if (collision.gameObject.name == "Score Cube(Clone)"){
            collision.gameObject.SetActive(false);
            scoreCollect.Play();
            score++;
            scoreText.text = "x" + score.ToString();
        }

        if (!willPhase && !goThru && collision.gameObject.name == "Cube Hazard(Clone)"){
            if (!specialActive){
                explosionParticles = ParticleSystem.Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
                explosionParticles.Play();
                
                Destroy(gameMovement.GetComponent("VerticalMovement"));
                gameObject.SetActive(false);
                gameEnded = true;
            }            
            specialActive = false;
        }

        if (!willPhase && !goThru && collision.gameObject.name == "Laser(Clone)"){
            if (!specialActive){
                explosionParticles = ParticleSystem.Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
                explosionParticles.Play();
                Destroy(gameMovement.GetComponent("VerticalMovement"));
                gameObject.SetActive(false);
                gameEnded = true;
            }
            specialActive = false;
        }

        if (gameEnded){
            endScreen.transform.position = new Vector3(Screen.currentResolution.width/2, Screen.currentResolution.height/2, 0);
            if (score == 1){
                pural = "";
            } else { pural = "s"; }
            scoreTextEnd.text = score.ToString() + " Score Cube" + pural;
            if (score > PlayerPrefs.GetInt("ScoreHS")){
                PlayerPrefs.SetInt("ScoreHS", score);
            }
            cubeHS.text = PlayerPrefs.GetInt("ScoreHS").ToString() + " Score Cubes";
            endScreen.SetActive(true);
            gameEnded = false;
        }
    }

}
