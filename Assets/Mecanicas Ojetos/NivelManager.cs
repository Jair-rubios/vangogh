using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelManager : MonoBehaviour
{
    public static NivelManager Instancia { get; private set; }

    [SerializeField] private int vidasMaximas = 3;
    [SerializeField] private string escenaMenu = "Menu";

    public int Vidas { get; private set; }
    public int PiezasRecogidas { get; private set; }
    public int PiezasTotales { get; private set; }
    public bool JuegoTerminado { get; private set; }

    public static event Action<int> VidasCambiaron;
    public static event Action<int, int> PiezasCambiaron;
    public static event Action PerdioJuego;
    public static event Action TodasLasPiezas;

    void Awake()
    {
        Instancia = this;
        Vidas = vidasMaximas;
        
        PiezasTotales = FindObjectsByType<Pieza>(FindObjectsSortMode.None).Length;
    }

    void Start()
    {
        VidasCambiaron?.Invoke(Vidas);
        PiezasCambiaron?.Invoke(PiezasRecogidas, PiezasTotales);
    }

    public void PerderVida()
    {
        if (JuegoTerminado) return;

        Vidas--;
        VidasCambiaron?.Invoke(Vidas);

        if (Vidas <= 0)
        {
            JuegoTerminado = true;
            PerdioJuego?.Invoke();
        }
    }

    public void RecogerPieza()
    {
        if (JuegoTerminado) return;

        PiezasRecogidas++;
        PiezasCambiaron?.Invoke(PiezasRecogidas, PiezasTotales);

        if (PiezasRecogidas >= PiezasTotales)
            TodasLasPiezas?.Invoke();
    }

    public void ReiniciarNivel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void IrAlMenu() => SceneManager.LoadScene(escenaMenu);
}