using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable _interactable;

    private void OnEnable()
    {
        _interactable.activated.AddListener(switchOnOff);
    }

    private void OnDisable()
    {
        _interactable.activated.RemoveListener(switchOnOff);
    }

    private void switchOnOff(BaseInteractionEventArgs args){
        GameObject Light = transform.GetChild(0).gameObject;

        bool isActive = !Light.activeSelf;
        Light.SetActive(isActive);
    }
}