using System;
using System.Collections;
using System.Collections.Generic; // use array lists
using UnityEngine;
using UnityEngine.UI; // interact with ui elements
using System.IO.Ports; // port access

public class SerialMonitor : MonoBehaviour {
    SerialPort data_stream = new SerialPort ("/dev/cu.usbserial-0001", 115200); // arduino is connected to port "/dev/cu.usbserial-11210" at 9600 baudrate
    public string receivedstring;
    public GameObject test_data;
    public Rigidbody rb;
    public float sensitivity = 0.01f;

    public string[] datas;


    private Animator m_Animator;
    // public int tempValue;

    void Start() {
        data_stream.Open();
        m_Animator = GetComponentInChildren<Animator>(); // attach animator to gameobject we want to animate
    }

    void Update() {
        if (m_Animator != null) {
            receivedstring = data_stream.ReadLine();
            Debug.Log(receivedstring);
            string[] datas = receivedstring.Split(','); // split the data between the comma
            Int32.TryParse(datas[0], out int tempValue);
            // Debug.Log(tempValue);
            if(tempValue == 0) {
                m_Animator.SetTrigger("Open");
            }
        }
    }
}
