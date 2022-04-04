using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable _interactable;
 
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    private bool flagIsShoot = true;

    private void OnEnable()
    {
        _interactable.activated.AddListener(Shoot);
    }

    private void OnDisable()
    {
        _interactable.activated.RemoveListener(Shoot);
    }

    private void Shoot(BaseInteractionEventArgs args){
        if(flagIsShoot){
            ParticleSystem Flare = transform.Find("Flare").GetComponent<ParticleSystem>();
            audioSource = GetComponent<AudioSource>();

            Flare.Play();
            audioSource.PlayOneShot(audioClips[0]);
            flagIsShoot = false;
        }
    }
}
