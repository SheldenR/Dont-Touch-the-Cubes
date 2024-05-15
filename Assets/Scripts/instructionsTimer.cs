using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructionsTimer : MonoBehaviour
{
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = (13 - (int) Time.timeSinceLevelLoad).ToString("0");

        if (Time.timeSinceLevelLoad > 8f){
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
