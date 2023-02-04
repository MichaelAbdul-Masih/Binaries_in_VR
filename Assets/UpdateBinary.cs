using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using CenterSpace.NMath.Core;

public class UpdateBinary : MonoBehaviour
{
    public GameObject Drop_Zone;
    public Canvas binaryCanvas;

    private XRBaseInteractable target;
    private Vector3 vert;
    private double q;
    private double pot1;
    private double pot2;
    private double theta;
    private double phi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private double MyFunctionPrimary(double r)
    {
        //Debug.Log(theta);
        double x = r * Mathf.Sin((float)theta) * Mathf.Cos((float)phi);
        double y = r * Mathf.Sin((float)theta) * Mathf.Sin((float)phi);
        double z = r * Mathf.Cos((float)theta);
        double r2 = Math.Sqrt((x - 1) * (x - 1) + y * y + z * z);
        double pot = 2.0 / ((1.0 + q) * r) + 2.0 * q / ((1.0 + q) * r2) + (x - q / (1 + q)) * (x - q / (1 + q)) + y*y - pot1;
        return pot * pot;
    }

    private double MyFunctionSecondary(double r)
    {
        //Debug.Log(theta);
        double x = r * Mathf.Sin((float)theta) * Mathf.Cos((float)phi);
        double y = r * Mathf.Sin((float)theta) * Mathf.Sin((float)phi);
        double z = r * Mathf.Cos((float)theta);
        double r1 = Math.Sqrt(x * x + y * y + z * z);
        double pot = 2.0 / ((1.0 + q) * r1) + 2.0 * q / ((1.0 + q) * r) + (x - q / (1 + q)) * (x - q / (1 + q)) + y * y - pot2;
        return pot * pot;
    }

    public void Update_roche()
    {
        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        double m1 = double.Parse(binaryCanvas.transform.Find("M1_text").GetComponent<Text>().text);
        double m2 = double.Parse(binaryCanvas.transform.Find("M2_text").GetComponent<Text>().text);
        q = m2 / m1;
        double com = q / (1 + q);

        double r1 = double.Parse(binaryCanvas.transform.Find("R1_text").GetComponent<Text>().text);
        double r2 = double.Parse(binaryCanvas.transform.Find("R2_text").GetComponent<Text>().text);

        pot1 = 2.0 / ((1.0 + q) * r1) + 2.0 * q / ((1.0 + q) * (1 - r1)) + (r1 - q / (1 + q)) * (r1 - q / (1 + q));
        pot2 = 2.0 / ((1.0 + q) * (1 - r2)) + 2.0 * q / ((1.0 + q) * r2) + ((1 - r2) - q / (1 + q)) * ((1 - r2) - q / (1 + q));

        Mesh mesh_p = target.transform.Find("Primary").GetComponent<MeshFilter>().mesh;
        Vector3[] vertices_p = mesh_p.vertices;
        Vector3[] normals_p = mesh_p.normals;

        Mesh mesh_s = target.transform.Find("Secondary").GetComponent<MeshFilter>().mesh;
        Vector3[] vertices_s = mesh_s.vertices;
        Vector3[] normals_s = mesh_s.normals;

        var f1 = new OneVariableFunction(new Func<double, double>(MyFunctionPrimary));
        var f2 = new OneVariableFunction(new Func<double, double>(MyFunctionSecondary));
        var minimizer = new GoldenMinimizer();


        for (var i = 0; i < vertices_p.Length; i++)
        {
            vert = vertices_p[i];

            double current_rad = Math.Sqrt(vert[0] * vert[0] + vert[1] * vert[1] + vert[2] * vert[2]);
            theta = Mathf.Acos((float)(vert[1] / current_rad));
            phi = Mathf.Atan((float)(vert[2] / vert[0]));
            double scale = minimizer.Minimize(f1, 0, com);
            vertices_p[i] = vertices_p[i] * ((float)scale / (float)current_rad);
        }

        mesh_p.vertices = vertices_p;

        /*for (var i = 0; i < vertices_s.Length; i++)
        {
            vert = vertices_s[i];
            double current_rad = Math.Sqrt(vert[0] * vert[0] + vert[1] * vert[1] + vert[2] * vert[2]);
            theta = Mathf.Acos((float)(vert[1] / current_rad));
            phi = Mathf.Atan((float)(vert[2] / vert[0]));
            double scale = minimizer.Minimize(f2, 0, (1.0 - com));
            vertices_s[i] = vertices_s[i] * ((float)scale / (float)current_rad);
        }

        mesh_s.vertices = vertices_s;*/
    }

    public void Update_q()
    {
        double m1 = double.Parse(binaryCanvas.transform.Find("M1_text").GetComponent<Text>().text);
        double m2 = double.Parse(binaryCanvas.transform.Find("M2_text").GetComponent<Text>().text);
        q = m2 / m1;
        double x1 = 0.5 * q / (1 + q);
        double x2 = x1 - 0.5;
        Debug.Log(x1.ToString() + " " + x2.ToString());

        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        target.transform.Find("Primary").transform.localPosition = new Vector3((float)x1, 0, 0);
        target.transform.Find("Secondary").transform.localPosition = new Vector3((float)x2, 0f, 0f);
    }

    public void Update_r()
    {
        double r1 = double.Parse(binaryCanvas.transform.Find("R1_text").GetComponent<Text>().text);
        double r2 = double.Parse(binaryCanvas.transform.Find("R2_text").GetComponent<Text>().text);

        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        target.transform.Find("Primary").transform.localScale = new Vector3((float)r1, (float)r1, (float)r1);
        target.transform.Find("Secondary").transform.localScale = new Vector3((float)r2, (float)r2, (float)r2);
    }
}
