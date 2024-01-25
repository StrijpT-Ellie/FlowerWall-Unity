using System;
using System.Collections;
using System.Collections.Generic; // use array lists
using UnityEngine;
using UnityEngine.UI; // interact with ui elements
using System.IO.Ports; // port access

public class SerialMonitor : MonoBehaviour {
    SerialPort data_stream = new SerialPort ("/dev/cu.usbserial-0001", 115200); // arduino is connected to port "/dev/cu.usbserial-11210" at 9600 baudrate
    public string receivedstring;
    public string[] datas;


    private Animator m_Animator;

    bool flowerOpen = false;

    void Start() {
        data_stream.Open();
        m_Animator = GetComponentInChildren<Animator>(); // attach animator to gameobject we want to animate
    }

    void Update() 
    {
        if (m_Animator != null) {
            receivedstring = data_stream.ReadLine();
            // Debug.Log(receivedstring); // data: CO2, TVOC, Temperature, Humidity, Sound
            string[] datas = receivedstring.Split(','); // split the data between the comma
            Int32.TryParse(datas[0], out int co2Value);
            Debug.Log(co2Value);

            if ((co2Value <= 419 && !flowerOpen) || (co2Value >= 420 && flowerOpen)) {
                m_Animator.SetTrigger(co2Value <= 419 ? "Open" : "Close");
                flowerOpen = !flowerOpen;
            }
        }
    }
}
