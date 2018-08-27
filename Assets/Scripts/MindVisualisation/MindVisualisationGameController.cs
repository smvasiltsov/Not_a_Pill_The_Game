﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MindVisualisationGameController : MonoBehaviour {

    public GameObject PythonRecieve;
    public GameObject MuseRecieve;
    public GameObject ResultsOfCalibrationWaiter;

    public GameObject ConcentrationCube;

    public float strenth;

    float accuracy;
    float[] coefs;
    double concentration;
    double concentration_centred;
    float intercept;

    //Rigidbody Player_rb;

    // Use this for initialization
    void Start () {
        ResultsOfCalibrationWaiter.SetActive(false);

        accuracy = PythonRecieve.GetComponent<PythonRecieve>().accuracy_;
        coefs = PythonRecieve.GetComponent<PythonRecieve>().coefs_;
        intercept = PythonRecieve.GetComponent<PythonRecieve>().intercept_;
        //Player_rb = Player.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        float[] a_r = MuseRecieve.GetComponent<MuseRecieve>().alpha_relative;
        float[] b_r = MuseRecieve.GetComponent<MuseRecieve>().beta_relative;
        float[] g_r = MuseRecieve.GetComponent<MuseRecieve>().gamma_relative;
        concentration = intercept+coefs[0]*a_r[0]+coefs[1]*a_r[1]+coefs[2]*a_r[2]+coefs[3]*a_r[3]
            +coefs[4]*b_r[0]+coefs[5]*b_r[1]+coefs[6]*b_r[2]+coefs[7]*b_r[3]
            +coefs[8]*g_r[0]+coefs[9]*g_r[1]+coefs[10]*g_r[2]+coefs[11]*g_r[3];
        concentration = 1.0 / (1.0 + Math.Exp(-concentration));

        Debug.Log(concentration);
        concentration_centred = concentration - 0.5;
        ConcentrationCube.transform.localScale = new Vector3(ConcentrationCube.transform.localScale.x, (float)concentration_centred, ConcentrationCube.transform.localScale.z);
        //Player_rb.AddForce(transform.up * Convert.ToSingle(concentration_centred));

    }
}
