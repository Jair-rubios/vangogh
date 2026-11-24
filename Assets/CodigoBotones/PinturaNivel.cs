using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class PinturaNivel : MonoBehaviour
{
    [SerializeField] private string nombreEscena = "Nivel1";
    [SerializeField] private string nombreVisible = "Nivel 1";
    [SerializeField] private PanelConfirmacion panelConfirmacion;

    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    void OnEnable()  => interactable.selectEntered.AddListener(OnSeleccionado);
    void OnDisable() => interactable.selectEntered.RemoveListener(OnSeleccionado);

    private void OnSeleccionado(SelectEnterEventArgs args)
    {
        panelConfirmacion.Mostrar(nombreEscena, nombreVisible);
    }
}