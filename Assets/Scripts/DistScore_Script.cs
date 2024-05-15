using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistScore_Script : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public Text scoreTextEnd;
    public Text scoreTextEnd2;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Mathf.Abs(player.position.y / 2.5f).ToString("0") + " m";
        scoreTextEnd2.text = Mathf.Abs(player.position.y / 2.5f).ToString("0") + "m Fallen";
        scoreTextEnd.text = "DISTANCE: " + Mathf.Abs(player.position.y / 2.5f).ToString("0") + "m";
    }
}
