using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class SonidoHover : MonoBehaviour
{
    private AudioClip sonidoHover;
    [SerializeField, Range(0f, 1f)] private float volumen = 0.7f;

    private XRBaseInteractable interactable;
    private AudioSource audioSource;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; 
    }

    void OnEnable()  => interactable.hoverEntered.AddListener(OnHover);
    void OnDisable() => interactable.hoverEntered.RemoveListener(OnHover);

    private void OnHover(HoverEnterEventArgs args)
    {
        if (sonidoHover != null)
            audioSource.PlayOneShot(sonidoHover, volumen);
    }
}
