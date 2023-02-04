using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFTS_352_rotate : MonoBehaviour
{
    public Text jd;
    public double t0 = 2455261.119d;
    public double period = 1.1241452f;
    public float play = 0.0f;
    public double inclination = 55.6f;
    
    void Start()
    {
        double phase = (double.Parse(jd.text) - t0) % period;
        double angle = phase *360f - 90f;
        //transform.Rotate(new Vector3(0f, angle, 0f));
        transform.Rotate(new Vector3(0f, (float)angle, 0f));
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 120f, 0f) * play * Time.deltaTime / (float)period);
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
    public void SnapToCurrentJD()
    {
        double phase = (double.Parse(jd.text) - t0) % period;
        double angle = phase *360f - 90f;
        transform.localRotation = Quaternion.Euler((float)inclination - 90f,0f,0f);
        transform.Rotate(new Vector3(0f, (float)angle, 0f));
    }
}
