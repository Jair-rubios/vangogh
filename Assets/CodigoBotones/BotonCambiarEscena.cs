using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class BotonCambiarEscena : MonoBehaviour
{
    [SerializeField] private string nombreEscena = "Menu";

    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    void OnEnable()  => interactable.selectEntered.AddListener(OnSeleccionado);
    void OnDisable() => interactable.selectEntered.RemoveListener(OnSeleccionado);

    private void OnSeleccionado(SelectEnterEventArgs args)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}