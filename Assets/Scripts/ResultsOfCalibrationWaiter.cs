﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.IO;

public class ResultsOfCalibrationWaiter : MonoBehaviour {

    public GameObject PythonRecieve;
    public GameObject GameController;
    public GameObject CalibrationPlusCSV;
    public GameObject CanvasController;
    public GameObject WriteReadCoefsController;

    Process runPython = new Process();


    void OnEnable ()
    {
        if(CalibrationPlusCSV.activeSelf) CalibrationPlusCSV.SetActive(false);
        UnityEngine.Debug.Log("runPython");
        runPython = new Process();
        runPython.StartInfo.FileName = "C:\\Users\\Sergey\\Documents\\MuseCSV\\log_regression.py";
        runPython.StartInfo.Arguments = "py -3";
        runPython.Start();

        
    }


    void Update () {
        if (PythonRecieve.GetComponent<PythonRecieve>().accuracy_ > 0 && PythonRecieve.GetComponent<PythonRecieve>().accuracy_ != GameController.GetComponent<MindVisualisationGameController>().GetAccuracy()) 
        {
            UnityEngine.Debug.Log("Acc: " + Convert.ToString(PythonRecieve.GetComponent<PythonRecieve>().accuracy_));
            string local_accuracy = "Acc: " + PythonRecieve.GetComponent<PythonRecieve>().accuracy_.ToString();
            
            UnityEngine.Debug.Log("Run WriteReadCoefs");

            WriteReadCoefsController.GetComponent<WriteReadCoefs>().WriteCoefs(PythonRecieve.GetComponent<PythonRecieve>().coefs_, PythonRecieve.GetComponent<PythonRecieve>().accuracy_, PythonRecieve.GetComponent<PythonRecieve>().intercept_);  //Запись коэффициентов в файл.RecordState определяет скрипт WriteReadCoefs

            PythonRecieve.GetComponent<PythonRecieve>().accuracy_ = 0.0f;
            runPython.Kill();
            CanvasController.GetComponent<CanvasController>().StartGame(local_accuracy);   // Начинает игру (конец работы питона)
        }
	}
}
