using UnityEngine;
using UnityEngine.Events;

public class JugadorVR : MonoBehaviour
{
    public int vidasMaximas = 3;
    private int vidasActuales;

    public UnityEvent alRecibirDano;
    public UnityEvent alMorir;

    private void Start()
    {
        vidasActuales = vidasMaximas;
    }

    public void RecibirDano(int cantidad)
    {
        vidasActuales -= cantidad;
        Debug.Log("Vida restante del jugador: " + vidasActuales);
        alRecibirDano?.Invoke();
        if (vidasActuales <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    { Debug.Log("¡El jugador ha muerto!");
        alMorir?.Invoke();
    }
}