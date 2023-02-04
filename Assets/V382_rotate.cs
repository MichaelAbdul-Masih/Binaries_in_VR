using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V382_rotate : MonoBehaviour
{
    public float play = 0.0f;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 120f, 0f) * play * Time.deltaTime / 1.885545f);
    }
    public void UpdatePlay()
    {
        if (play == 1.0){
            play = 0.0f;
        }
        else{
            play = 1.0f;
        }
    }
}
