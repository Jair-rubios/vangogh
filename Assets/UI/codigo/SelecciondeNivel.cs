using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecciondeNivel : MonoBehaviour
{
    [Header("Configuracion del Nivel")]
    [Tooltip("indice de la escena en Build Settings que corresponde a esta pintura")]
    public int indiceDeNivel = 1;

    [Header("UI Compartida")]
    [Tooltip("Arrastra aqui el GameObject CanvasConfirmacion de la escena")]
    public GameObject canvasConfirmacion;

    [Tooltip("Punto transform opcional para posicionar el Canvas (deja vacio para usar la posicion frente a la pintura)")]
    public Transform posicionObjetivoCanvas;

    private bool estaDesbloqueado = false;

    // Guarda globalmente cual nivel debe cargar el boton "Empezar Nivel"
    private static int nivelSeleccionadoParaCargar = 1;

    void Start()
    {
        // El nivel 1 siempre inicia desbloqueado por defecto
        int nivelMaximoDesbloqueado = PlayerPrefs.GetInt("UnlockedLevel", 1);
        estaDesbloqueado = (indiceDeNivel <= nivelMaximoDesbloqueado);

        // Asegura que el Canvas empiece oculto al iniciar la escena
        if (canvasConfirmacion != null && canvasConfirmacion.activeSelf)
        {
            canvasConfirmacion.SetActive(false);
        }
    }

    // Este metodo se ejecuta al interactuar con el XR Simple Interactable de la pintura
    public void AlInteractuarConPintura()
    {
        if (estaDesbloqueado)
        {
            // Guarda el qndice exacto de la pintura que fue tocada
            nivelSeleccionadoParaCargar = indiceDeNivel;

            if (canvasConfirmacion != null)
            {
                // Posiciona el Canvas frente a la pintura interactuada
                Vector3 posicionObjetivo = (posicionObjetivoCanvas != null) 
                    ? posicionObjetivoCanvas.position 
                    : transform.position + transform.forward * 0.5f;

                Quaternion rotacionObjetivo = (posicionObjetivoCanvas != null) 
                    ? posicionObjetivoCanvas.rotation 
                    : transform.rotation;

                canvasConfirmacion.transform.position = posicionObjetivo;
                canvasConfirmacion.transform.rotation = rotacionObjetivo;

                canvasConfirmacion.SetActive(true);
                Debug.Log($"[VR Seleccion de Nivel] Canvas abierto para cargar el Nivel/Escena indice: {nivelSeleccionadoParaCargar}");
            }
            else
            {
                Debug.LogError("[VR Seleccion de Nivel] Falta asignar el GameObject canvasConfirmacion en el Inspector!");
            }
        }
        else
        {
            Debug.LogWarning($"[VR Seleccion de Nivel] El Nivel {indiceDeNivel} aun esta bloqueado.");
        }
    }

    // Evento para el boton "Empezar Nivel" del Canvas
    public void EmpezarNivel()
    {
        Debug.Log($"[VR Seleccion de Nivel] Cargando la escena con indice: {nivelSeleccionadoParaCargar}...");
        SceneManager.LoadScene(nivelSeleccionadoParaCargar);
    }

    // Evento para el boton "No estoy listo" del Canvas
    public void CancelarSeleccion()
    {
        if (canvasConfirmacion != null)
        {
            canvasConfirmacion.SetActive(false);
            Debug.Log("[VR Seleccion de Nivel] Seleccion cancelada, Canvas ocultado.");
        }
    }
}