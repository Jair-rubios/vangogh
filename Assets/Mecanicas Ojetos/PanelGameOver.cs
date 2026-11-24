using UnityEngine;

public class PanelGameOver : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform camara;
    [SerializeField] private float distancia = 1.5f;
    [SerializeField] private GameObject locomotion;   

    void Start()
    {
        if (camara == null) camara = Camera.main.transform;
        panel.SetActive(false);
    }

    void OnEnable()  => NivelManager.PerdioJuego += Mostrar;
    void OnDisable() => NivelManager.PerdioJuego -= Mostrar;

    private void Mostrar()
    {
        if (locomotion != null) locomotion.SetActive(false);

        Vector3 adelante = camara.forward;
        adelante.y = 0f;
        adelante.Normalize();

        panel.transform.position = camara.position + adelante * distancia;
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - camara.position);
        panel.SetActive(true);
    }

    public void Reiniciar() => NivelManager.Instancia.ReiniciarNivel();
    public void IrAlMenu()  => NivelManager.Instancia.IrAlMenu();
}