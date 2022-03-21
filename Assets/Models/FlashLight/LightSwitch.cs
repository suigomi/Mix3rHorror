using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable _interactable;
 
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();

        bool isActive = !Light.activeSelf;
        Light.SetActive(isActive);
        audioSource.PlayOneShot(audioClips[Light.activeSelf ? 0 : 1]);
    }
}