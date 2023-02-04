using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GetObjectInfo : MonoBehaviour
{
    public GameObject Drop_Zone;
    public Canvas Info_Canvas;
    //public Slider changeSlider;

    private XRBaseInteractable target;
    // Start is called before the first frame update
    void Start()
    {
        Canvas binaryCanvas = Info_Canvas.transform.Find("BinaryCanvas").GetComponent<Canvas>();
        binaryCanvas.gameObject.SetActive(false);
        Canvas rocheCanvas = Info_Canvas.transform.Find("RocheCanvas").GetComponent<Canvas>();
        rocheCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo()
    {
        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        Drop_Zone.GetComponent<Binary_rotate>().t0 = target.GetComponent<ObjectInfo>().t0;
        Drop_Zone.GetComponent<Binary_rotate>().period = target.GetComponent<ObjectInfo>().period;

        Slider changeSlider = Info_Canvas.transform.Find("Slider").GetComponent<Slider>();
        changeSlider.value = (float)target.GetComponent<ObjectInfo>().inclination;

        Text nameText = Info_Canvas.transform.Find("Name_text").GetComponent<Text>();
        nameText.text = target.GetComponent<ObjectInfo>().objectName;

        if (target.GetComponent<ObjectInfo>().objectType == "binary")
        {
            Canvas binaryCanvas = Info_Canvas.transform.Find("BinaryCanvas").GetComponent<Canvas>();
            binaryCanvas.gameObject.SetActive(true);
            Canvas rocheCanvas = Info_Canvas.transform.Find("RocheCanvas").GetComponent<Canvas>();
            rocheCanvas.gameObject.SetActive(false);
        }
        else if (target.GetComponent<ObjectInfo>().objectType == "roche")
        {
            Canvas binaryCanvas = Info_Canvas.transform.Find("BinaryCanvas").GetComponent<Canvas>();
            binaryCanvas.gameObject.SetActive(false);
            Canvas rocheCanvas = Info_Canvas.transform.Find("RocheCanvas").GetComponent<Canvas>();
            rocheCanvas.gameObject.SetActive(true);
        }
        else
        {
            Canvas binaryCanvas = Info_Canvas.transform.Find("BinaryCanvas").GetComponent<Canvas>();
            binaryCanvas.gameObject.SetActive(false);
            Canvas rocheCanvas = Info_Canvas.transform.Find("RocheCanvas").GetComponent<Canvas>();
            rocheCanvas.gameObject.SetActive(false);
        }
    }
}
