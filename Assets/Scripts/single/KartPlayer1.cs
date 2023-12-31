using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utility;
using ArduinoBluetoothAPI;
using System;
using UnityEngine.UI;

public class KartPlayer1 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameManager1 manager;
    public Rigidbody sphere;

    public Transform kartModel;
    public Transform kartNormal;
    float speed, currentSpeed;
    float rotate, currentRotate;

    public float acceleration = 30f;
    public float steering = 80f;
    public float gravity = 10f;
    public float accelspeed = 50;
    public LayerMask layerMask;

    public float x;
    public string accel;
    public string accel2;
    private float on = 0;

    public float movespeed = 8.0f;
    public float turnspeed = 0.0f;
    public float jumppower = 5.0f;

    private float turnspeedvalue = 200.0f;

    BluetoothHelper bluetoothHelper;
    string deviceName;
    string received_message;
    public Text text;
    RaycastHit hit;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager1>();
    }
    void OnConnected(BluetoothHelper helper)
    {

        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }
    void OnConnectionFailed(BluetoothHelper helper)
    {

    }
    void OnMessageReceived(BluetoothHelper helper)
    {
        //StartCoroutine(blinkSphere());
        received_message = helper.Read();

        x = float.Parse(received_message.Substring(0, 5)) - 507;
        //text.text = received_message;
        accel2 = received_message.Substring(6, 1);
        accel = received_message.Substring(7);
        //Debug.Log(x);

    }
    void OnDestroy()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }

    void Start()
    {
        deviceName = "HC-06"; //bluetooth should be turned ON;
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
            //bluetoothHelper.OnDataReceived = OnMessageReceived(bluetoothHelper); //read the data
            bluetoothHelper.setFixedLengthBasedStream(8);
            bluetoothHelper.setLengthBasedStream();
            LinkedList<BluetoothDevice> ds = bluetoothHelper.getPairedDevicesList();
            
            foreach (BluetoothDevice d in ds)
            {
                Debug.Log($"{d.DeviceName} {d.DeviceAddress}");
            }
            //Debug.Log("연결이 되냐 안되냐");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            text.text = ex.Message;
            //BlueToothNotEnabledException == bluetooth Not turned ON
            //BlueToothNotSupportedException == device doesn't support bluetooth
            //BlueToothNotReadyException == the device name you chose is not paired with your android or you are not connected to the bluetooth device;
            //                        bluetoothHelper.Connect () returned false;
        }
        turnspeed = 0.0f;
        turnspeed = turnspeedvalue;
    }

    public void ApplyAccelerations(float input)
    {

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
        speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f);
        rotate = 0f;
    }
    public void Steer(float steeringSignal)
    {
        int steerDirection2 = steeringSignal > 0 ? 1 : -1;
        rotate = (steering * steerDirection2) * x /10000;
    }

    private void FixedUpdate()
    {
        if (bluetoothHelper != null)
            if (bluetoothHelper.Available)
            {
                OnMessageReceived(bluetoothHelper);
                received_message = bluetoothHelper.Read();
                on = 1;
            }
        if (x > 0 && on == 1)
        {
            ApplyAccelerations(1);
            Steer(1);

        }
        else if (x < 0 && on == 1)
        {
            ApplyAccelerations(0);
        }
        Steer(0);
        float text2;
        if (GameManager1.Inst.state1 == State1.RacingStart)
        {
            sphere.AddForce(sphere.transform.forward * currentSpeed, ForceMode.Acceleration);
            if (accel2 == "3")
            {
                sphere.AddForce(sphere.transform.forward * accelspeed, ForceMode.Acceleration);
            }
            sphere.transform.Rotate(Vector3.up * Time.smoothDeltaTime * turnspeed * rotate);
           // if (sphere.velocity.z > 0)
            //{
                text2 = sphere.velocity.magnitude * 10;
                manager.cur_speed.text = text2.ToString().Substring(0, 5);
           // }        
            //text2 = (((transform.position - sphere.position).magnitude) / Time.deltaTime);
            
        }
    }
    void OnGUI()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.DrawGUI();
        else
            return;

        if (!bluetoothHelper.isConnected())
            //if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Connect"))
          //  {
                if (bluetoothHelper.isDevicePaired())
                    bluetoothHelper.Connect(); // tries to connect
                else
                    sphere.GetComponent<Renderer>().material.color = Color.magenta;
           // }

    }

    public void playerstart()
    {
        gameObject.GetComponent<KartPlayer1>().enabled = true;
    }
}
