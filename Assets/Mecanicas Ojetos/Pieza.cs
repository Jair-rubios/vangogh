using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Collider))]
public class Pieza : MonoBehaviour
{
    private AudioClip sonidoRecoger;
    private float velocidadGiro = 60f;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, velocidadGiro * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<XROrigin>() == null) return;

        if (sonidoRecoger != null)
            AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position);

        NivelManager.Instancia.RecogerPieza();
        Destroy(gameObject);
    }
}