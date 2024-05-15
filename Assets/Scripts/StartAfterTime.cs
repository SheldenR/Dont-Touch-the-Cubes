using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAfterTime : MonoBehaviour
{
    public float timeToShow;

    // Update is called once per frame

    void Start()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0f,0f,0f);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > timeToShow){
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(5.5f,5.5f,5.5f);
        }
    }
}
