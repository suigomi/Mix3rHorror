using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class AxleInfo
{
    public WheelCollider Wheel;
    public bool motor; //駆動輪か?
    public bool steering; //ハンドル操作をしたときに角度が変わるか？
}
public class MotorController : MonoBehaviour
{
    public GameObject xrOrigin;

    public GameObject cameraPos;
    public GameObject getOffPos;
    public GameObject[] hands;
    public GameObject[] visualHands;
    public bool riding = false; //乗っているか

    public Transform handle;
    public List<AxleInfo> axleInfos; // 個々の車軸の情報
    public float maxMotorTorque; //ホイールに適用可能な最大トルク
    public float maxSteeringAngle; // 適用可能な最大ハンドル角度

    public float brakePower;

    private Rigidbody motorRb;

    public float limitSpeed;

    TestInputActions testInputActions;

    private AudioSource engineSound;

    void Start()
    {
        engineSound = GetComponent<AudioSource>();

        motorRb = GetComponent<Rigidbody>();
        motorRb.centerOfMass = new Vector3(0, -0.5f, 0.3f);
        foreach (GameObject hand in visualHands)
        {
            hand.SetActive(false);
        }
    }

    void Awake()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();
    }

    public void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SwitchRiding();
        }

        if (testInputActions.Player.GetOff.ReadValue<float>() > 0.5 && riding)
        {
            SwitchRiding();
        }
        if (riding)
        {
            xrOrigin.transform.position = cameraPos.transform.position;
            xrOrigin.transform.parent = cameraPos.transform;
        }
    }


    public void FixedUpdate()
    {
        if (riding)
        {
            float motor = maxMotorTorque * testInputActions.Player.Accelerate.ReadValue<float>();
            //float steering = maxSteeringAngle * testInputActions.Player.Rotate.ReadValue<float>();
            float steering = testInputActions.Player.Look.ReadValue<Quaternion>().eulerAngles.y;
            if (steering > 180f)
            {
                steering -= 360f;
            }
            steering = Mathf.Clamp(steering, -maxSteeringAngle, maxSteeringAngle);
            float brake = brakePower * testInputActions.Player.Brake.ReadValue<float>();

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.Wheel.steerAngle = steering;
                    handle.localEulerAngles = new Vector3(0, steering, 0);
                }
                if (axleInfo.motor)
                {
                    axleInfo.Wheel.motorTorque = motor;
                }
                axleInfo.Wheel.brakeTorque = brake;
            }

            if (motorRb.velocity.magnitude > limitSpeed)
            {
                motorRb.velocity = motorRb.velocity.normalized * limitSpeed;
            }
        }
        else
        {
            motorRb.velocity = Vector3.zero;
        }
    }

    public void SwitchRiding()
    {
        riding = !riding;
        if (riding) // 乗るときの処理
        {
            xrOrigin.GetComponent<CharacterController>().enabled = false;
            xrOrigin.transform.position = cameraPos.transform.position;
            xrOrigin.transform.rotation = cameraPos.transform.rotation;
            xrOrigin.transform.parent = cameraPos.transform;
            engineSound.Play();
            foreach (GameObject hand in hands)
            {
                hand.SetActive(false);
            }
            foreach (GameObject hand in visualHands)
            {
                hand.SetActive(true);
            }
        }
        if (!riding) // 降りるときの処理
        {
            xrOrigin.transform.parent = null;
            xrOrigin.GetComponent<CharacterController>().height = 1.1176f;
            xrOrigin.transform.position = getOffPos.transform.position;
            xrOrigin.transform.rotation = getOffPos.transform.rotation;
            xrOrigin.GetComponent<CharacterController>().enabled = true;
            engineSound.Stop();
            foreach (GameObject hand in hands)
            {
                hand.SetActive(true);
            }
            foreach (GameObject hand in visualHands)
            {
                hand.SetActive(false);
            }
        }
    }
}

