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
    public bool riding = false;

    public Transform handle;
    public List<AxleInfo> axleInfos; // 個々の車軸の情報
    public float maxMotorTorque; //ホイールに適用可能な最大トルク
    public float maxSteeringAngle; // 適用可能な最大ハンドル角度

    public float brakePower;

    private Rigidbody motorRb;

    public float limitSpeed;

    TestInputActions testInputActions;
    void Start()
    {
        motorRb = GetComponent<Rigidbody>();
        motorRb.centerOfMass = new Vector3(0, -0.5f, 0.3f);
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
            Vector2 steering_vec = testInputActions.Player.Look.ReadValue<Vector2>();
            float steering = steering_vec.x * maxSteeringAngle;
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
                ApplyLocalPositionToVisuals(axleInfo.Wheel);
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

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }
        Transform visualWheel = collider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void SwitchRiding()
    {
        riding = !riding;
        if (riding)
        {
            xrOrigin.GetComponent<CharacterController>().enabled = false;
            xrOrigin.transform.position = cameraPos.transform.position;
            xrOrigin.transform.rotation = cameraPos.transform.rotation;
            xrOrigin.transform.parent = cameraPos.transform;
            foreach (GameObject hand in hands)
            {
                hand.SetActive(false);
            }
        }
        if (!riding)
        {
            xrOrigin.transform.parent = null;
            xrOrigin.GetComponent<CharacterController>().height = 1.1176f;
            xrOrigin.transform.position = getOffPos.transform.position;
            xrOrigin.transform.rotation = getOffPos.transform.rotation;
            xrOrigin.GetComponent<CharacterController>().enabled = true;
            foreach (GameObject hand in hands)
            {
                hand.SetActive(true);
            }
        }
    }
}

