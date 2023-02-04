using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayJD : MonoBehaviour
{

    public Text changingText;
    public Text buttonText;
    
    public float play = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changingText.text = (double.Parse(changingText.text) + play * Time.deltaTime / 3.0f).ToString();
    }
    
    public void UpdateJD()
    {
        if (play == 1.0){
            play = 0.0f;
        }
        else{
            play = 1.0f;
        }
    }
    public void UpdateButtonText()
    {
        if (buttonText.text == "Play"){
            buttonText.text = "Pause";
        }
        else{
            buttonText.text = "Play";
        }
    }
}
