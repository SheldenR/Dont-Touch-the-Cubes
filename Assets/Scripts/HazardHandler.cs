using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardHandler : MonoBehaviour
{
    public GameObject CubeHazard;
    public GameObject OddHazard;
    public GameObject LaserHazard;
    public GameObject ScoreCube;
    public GameObject DistanceMarker;
    private GameObject player;
    private float blockSpawnRate = 8f; // Default 8f
    private float markerSpawnRate = -250f; // Default 250f
    private float playerStartPos;
    private int objectToSpawn;
    public bool tripwireMatrix;
    public bool spawning;
    public float hazardSpawnDelay = 3f;

    private float zAxis = 0f;

    // Start is called before the first frame update
    void Start(){   
        tripwireMatrix = false;
        spawning = true;
        player = GameObject.Find("Player");
        playerStartPos = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1,3) == 2){
            zAxis = 90f;
        } else { zAxis = 0f; }

        if (Time.timeSinceLevelLoad > hazardSpawnDelay && spawning) {
            if (player.transform.position.y < (playerStartPos - blockSpawnRate))
            {
                Quaternion rot = Quaternion.Euler(90, 0, zAxis);
                playerStartPos = player.transform.position.y;
                objectToSpawn = Random.Range(1, 17); // 1 to 16
                if (objectToSpawn == 1){ // Laser Spawn
                    if (!tripwireMatrix){
                        Instantiate(LaserHazard, new Vector3(Random.Range(-6.7f, 6.7f), transform.position.y - 100f, Random.Range(-6.7f, 6.7f)), rot);
                    }
                } else if (objectToSpawn == 2) { // Score Cube
                    Instantiate(ScoreCube, new Vector3(Random.Range(-6.7f, 6.7f), transform.position.y - 100f, Random.Range(-6.7f, 6.7f)), Quaternion.identity);
                } else if (objectToSpawn >= 3) { // Cube Spawn 
                    if (tripwireMatrix){ // Spawns lasers instead of cubes during tripwire matrix
                        Instantiate(LaserHazard, new Vector3(Random.Range(-6.7f, 6.7f), transform.position.y - 100f, Random.Range(-6.7f, 6.7f)), rot);
                    } else {
                        if (objectToSpawn >= 16 && Time.timeSinceLevelLoad > 40f){
                            Instantiate(OddHazard, new Vector3(Random.Range(-5.3f, 5.3f), transform.position.y - 100f, Random.Range(-5.3f, 5.3f)), Quaternion.identity);
                        }
                        else {
                            Instantiate(CubeHazard, new Vector3(Random.Range(-6.7f, 6.7f), transform.position.y - 100f, Random.Range(-6.7f, 6.7f)), Quaternion.identity);
                        }
                    }
                }
            }
        }

        if (player.transform.position.y < markerSpawnRate) { // Distance Marker
            markerSpawnRate-=250f;
            Instantiate(DistanceMarker, new Vector3(-20f, markerSpawnRate+7.5f, -3.75f), Quaternion.identity);
        }
    }
}
