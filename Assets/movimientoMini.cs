using UnityEngine;

public class movimientoMini : MonoBehaviour
{
    public float amplitud = 0.1f;
    public float velocidadMovimiento = 2f;

    private float posicionInicialY;
    private float temporizador;

    void Start()
    {
        posicionInicialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float nuevaY = posicionInicialY + Mathf.Sin(Time.time * velocidadMovimiento) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

        // --- Temporizador para iniciar giro ---
        
    
}
}
