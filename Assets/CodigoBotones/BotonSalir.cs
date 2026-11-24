using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class BotonSalir : MonoBehaviour
{
    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    void OnEnable()  => interactable.selectEntered.AddListener(OnSeleccionado);
    void OnDisable() => interactable.selectEntered.RemoveListener(OnSeleccionado);

    private void OnSeleccionado(SelectEnterEventArgs args)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}