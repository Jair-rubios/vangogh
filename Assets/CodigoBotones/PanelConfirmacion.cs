using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PanelConfirmacion : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titulo;           // el texto del título
    [SerializeField] private Transform camara;
    [SerializeField] private float distancia = 1.5f;

    private string escenaPendiente;

    void Start()
    {
        if (camara == null) camara = Camera.main.transform;
        panel.SetActive(false);
    }

    public void Mostrar(string nombreEscena, string nombreVisible)
    {
        escenaPendiente = nombreEscena;

        if (titulo != null) titulo.text = nombreVisible;

        Vector3 adelante = camara.forward;
        adelante.y = 0f;
        adelante.Normalize();

        panel.transform.position = camara.position + adelante * distancia;
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - camara.position);
        panel.SetActive(true);
    }

    public void IniciarNivel()
    {
        SceneManager.LoadScene(escenaPendiente);
    }

    public void Cerrar()
    {
        panel.SetActive(false);
    }
}