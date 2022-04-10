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
    public GameObject mainCamera;
    
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
        if (Keyboard.current.rKey.wasPressedThisFrame && motorRb.velocity.magnitude < 2f)
        {
            SwitchRideing();
        }

        if (testInputActions.Player.GetOff.ReadValue<bool>() && riding && motorRb.velocity.magnitude < 2f)
        {
            SwitchRideing();
        }
        if (riding)
        {
            mainCamera.transform.position = cameraPos.transform.position;
            mainCamera.transform.parent = cameraPos.transform;
        }
    }


    public void FixedUpdate()
    {
        if (riding)
        {
            float motor = maxMotorTorque * testInputActions.Player.Accelerate.ReadValue<float>();
            //float steering = maxSteeringAngle * testInputActions.Player.Rotate.ReadValue<float>();
            Quaternion steeringQua = testInputActions.Player.Look.ReadValue<Quaternion>();
            float steering = Mathf.Clamp(steeringQua.eulerAngles.z, -maxSteeringAngle, maxSteeringAngle);
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

    public void SwitchRideing()
    {
        riding = !riding;
        if (riding)
        {
            mainCamera.transform.position = cameraPos.transform.position;
            mainCamera.transform.rotation = cameraPos.transform.rotation;
            mainCamera.transform.parent = cameraPos.transform;
            foreach(GameObject hand in hands)
            {
                hand.SetActive(false);
            }
        }
        if (!riding)
        {
            xrOrigin.transform.position = getOffPos.transform.position;
            xrOrigin.transform.rotation = getOffPos.transform.rotation;
            mainCamera.transform.parent = xrOrigin.transform;
            foreach (GameObject hand in hands)
            {
                hand.SetActive(false);
            }
        }
    }
}


