using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCubeHandler : MonoBehaviour
{
    public GameObject MysteryCube;
    private float cubeSpawnRate = 5f; // Default 20f
    private float nextCubeSpawn = 0.0f;
    private bool cubesSpawnable;

    // Start is called before the first frame update
    void Start()
    {
        cubesSpawnable = true;
        nextCubeSpawn = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > 3f) {
            if (cubesSpawnable && Time.timeSinceLevelLoad > nextCubeSpawn)
            {
                nextCubeSpawn = Time.timeSinceLevelLoad + cubeSpawnRate;
                Instantiate(MysteryCube, new Vector3(Random.Range(-6.7f, 6.7f), transform.position.y - 100f, Random.Range(-6.7f, 6.7f)), Quaternion.identity);
            }
        }
    }
}
