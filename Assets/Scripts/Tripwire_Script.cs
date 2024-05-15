using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire_Script : MonoBehaviour
{
    public Vector2 dir = Vector2.left;
    private float startX = 0;
    private float laserspeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(dir * laserspeed * Time.deltaTime);

        if (transform.position.x <= startX - 5)
        {
            dir = Vector2.right;
        }
        else if (transform.position.x >= startX + 5)
        {
            dir = Vector2.left;
        }
    }
}
