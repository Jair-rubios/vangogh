using UnityEngine;

public class ProyectilDano : MonoBehaviour
{
    public int dano = 1;
    public float tiempoDeVida = 10f;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida); 
    }

    private void OnCollisionEnter(Collision colision)
    {
        // Revisa si colisiona con el jugador VR
        JugadorVR jugador = colision.gameObject.GetComponentInParent<JugadorVR>();
        
        if (jugador != null)
        {
            jugador.RecibirDano(dano);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 0.5f);
        }
    }
}