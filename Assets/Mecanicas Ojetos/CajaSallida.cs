using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Collider))]
public class CajaSalida : MonoBehaviour
{
    void Awake()
    {
        Activar(false);
    }

    void OnEnable()  => NivelManager.TodasLasPiezas += MostrarCaja;
    void OnDisable() => NivelManager.TodasLasPiezas -= MostrarCaja;

    private void MostrarCaja() => Activar(true);

    private void Activar(bool activo)
    {
        foreach (var r in GetComponentsInChildren<Renderer>(true)) r.enabled = activo;
        GetComponent<Collider>().enabled = activo;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<XROrigin>() == null) return;
        NivelManager.Instancia.IrAlMenu();
    }
}