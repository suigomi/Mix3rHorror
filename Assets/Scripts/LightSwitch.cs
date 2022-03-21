using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public InputActionReference toggleReference = null;

    private void Awake(){
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context) {
        GameObject Light = transform.GetChild(0).gameObject;
        bool isActive = !Light.activeSelf;
        Light.SetActive(isActive);
    }
}