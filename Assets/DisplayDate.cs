using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDate : MonoBehaviour
{
    
    public Text changingText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static double calcJulianDate(System.DateTime date) {
        return date.ToOADate() + 2415018.5;
    }
    
    public void UpdateDate()
    {
        //changingText.text = System.DateTime.Now.ToString();
        changingText.text = calcJulianDate(System.DateTime.Now).ToString();
    }
}
