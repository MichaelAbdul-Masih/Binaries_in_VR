using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using CenterSpace.NMath.Analysis;
using CenterSpace.NMath.Core;

public class calcR : MonoBehaviour
{

    public Text radius;
    public GameObject Drop_Zone;
    public Text v_crit;

    private XRBaseInteractable target;
    private Vector3 vert;
    private double rad;
    private double vc;
    private double theta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private double MyFunction(double r)
    {
        //Debug.Log(theta);
        double pot = 1.0 / 3.0 * (3 * vc - vc * vc * vc) / (3 * rad) * (3 * vc - vc * vc * vc) / (3 * rad) * r * r * r * Mathf.Sin((float)theta) * Mathf.Sin((float)theta) - r + rad;
        return pot * pot;
    }


    // Update is called once per frame
    public void Update_vcrit_old()
    {
        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        rad = 0.25;
        vc = double.Parse(v_crit.text);
        Mesh mesh = target.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        var f = new OneVariableFunction(new Func<double, double>(MyFunction));
        var minimizer = new GoldenMinimizer();

        for (var i = 0; i < vertices.Length; i++)
        {
            vert = vertices[i];
            double current_rad = Math.Sqrt((vert[0] * vert[0]) + (vert[1] * vert[1]) + (vert[2] * vert[2]));
            theta = Mathf.Acos((float)(vert[1] / current_rad));
            double scale = minimizer.Minimize(f, rad * 0.9, rad * 1.5);
            vertices[i] = vertices[i] * ((float)scale / (float)current_rad);
        }

        mesh.vertices = vertices;
    }

    // Update is called once per frame
    public void Update_vcrit()
    {
        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        rad = 0.25;
        vc = double.Parse(v_crit.text);
        Mesh mesh = target.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        double[] x = new double[] { rad };
        double[] bndl = new double[] { rad * 0.9};
        double[] bndu = new double[] { rad * 1.5};
        double epsg = 0.0001;
        double epsf = 0;
        double epsx = 0;
        int maxits = 0;
        alglib.minlmstate state;
        alglib.minlmreport rep;

        alglib.minlmcreatev(2, x, 0.0001, out state);
        alglib.minlmsetbc(state, bndl, bndu);
        alglib.minlmsetcond(state, epsx, maxits);
        alglib.minlmoptimize(state, MyFunction, null, null);
        alglib.minlmresults(state, out x, out rep);

        var f = new OneVariableFunction(new Func<double, double>(MyFunction));
        var minimizer = new GoldenMinimizer();

        for (var i = 0; i < vertices.Length; i++)
        {
            vert = vertices[i];
            double current_rad = Math.Sqrt((vert[0] * vert[0]) + (vert[1] * vert[1]) + (vert[2] * vert[2]));
            theta = Mathf.Acos((float)(vert[1] / current_rad));
            double scale = minimizer.Minimize(f, rad * 0.9, rad * 1.5);
            vertices[i] = vertices[i] * ((float)scale / (float)current_rad);
        }

        mesh.vertices = vertices;
    }

    public void Update_r()
    {
        double r1 = double.Parse(radius.text);

        target = Drop_Zone.GetComponent<XRSocketInteractor>().selectTarget;
        target.transform.localScale = new Vector3((float)r1, (float)r1, (float)r1);
    }
}
