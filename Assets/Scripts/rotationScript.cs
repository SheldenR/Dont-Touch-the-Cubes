using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScript : MonoBehaviour
{
    // Axis To Rotate
    public float xAngle;
    public float yAngle;
    public float zAngle;
    public bool randomDirection;
    public bool startRandom;

    private void Start()
    {
        if (randomDirection){
            xAngle = Random.Range(-15, 15);
            yAngle = Random.Range(-15, 15);
            zAngle = Random.Range(-15, 15);
        }

        if (startRandom) {
            transform.eulerAngles = new Vector3(Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationToAdd = new Vector3(xAngle*Time.deltaTime, yAngle*Time.deltaTime, zAngle*Time.deltaTime);
        transform.Rotate(rotationToAdd);
    }
}
