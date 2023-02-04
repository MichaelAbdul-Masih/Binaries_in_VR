using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changeincl : MonoBehaviour
{
    public Text changingText;
    public Slider sliderUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TextChange()
    {
        changingText.text = sliderUI.value.ToString();
    }
}
