using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Villano : MonoBehaviour
{
    public enum EstadoVillano { Caminando, Aturdido, Corriendo }
    public EstadoVillano estadoActual = EstadoVillano.Caminando;

    [Header("Referencias")]
    public Transform camaraJugadorVR; 
    public Animator animador;
    public NavMeshAgent agenteNavegacion;
    public Transform puntoLanzamiento; 

    [Header("Deteccion")]
    public float rangoVision = 12f;
    public float anguloVision = 60f;
    public LayerMask capasObstaculos; 

    [Header("Ataque (Proyectiles)")]
    public GameObject[] prefabsProyectiles; 
    public float tiempoEntreAtaques = 3f;
    public float fuerzaLanzamiento = 10f;
    private float temporizadorAtaque;

    [Header("Patrullaje")]
    public Transform[] puntosPatrulla;
    private int indicePatrulla = 0;

    private void Start()
    {if (agenteNavegacion == null) agenteNavegacion = GetComponent<NavMeshAgent>();
        if (animador == null) animador = GetComponent<Animator>();
      IrAlSiguientePuntoPatrulla();
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadoVillano.Caminando:
                ComportamientoCaminar();
                if (PuedeVerAlJugador())
                {
                    StartCoroutine(TransicionAAturdido());
                }
                break;

            case EstadoVillano.Aturdido:
                
                break;

            case EstadoVillano.Corriendo:
                ComportamientoCorrer();
                if (!PuedeVerAlJugador())
                {
                    VolverACaminar();
                }
                break;
        }
    }

   
    private bool PuedeVerAlJugador()
    {
        if (camaraJugadorVR == null) return false;

        Vector3 direccionHaciaJugador = (camaraJugadorVR.position - transform.position).normalized;
        float distancia = Vector3.Distance(transform.position, camaraJugadorVR.position);

        if (distancia <= rangoVision)
        {
            float angulo = Vector3.Angle(transform.forward, direccionHaciaJugador);
            if (angulo <= anguloVision / 2f)
            {
            if (!Physics.Linecast(transform.position + Vector3.up * 1.5f, camaraJugadorVR.position, capasObstaculos))
            {
             return true;
            }
            }
        }
        return false;
    }

  
    private void ComportamientoCaminar()
    { agenteNavegacion.speed = 2.0f;
        animador.SetBool("caminando", true);
        animador.SetBool("corriendo", false);

        if (!agenteNavegacion.pathPending && agenteNavegacion.remainingDistance < 0.5f)
        {
            IrAlSiguientePuntoPatrulla();
        }
    }

    private IEnumerator TransicionAAturdido()
    {estadoActual = EstadoVillano.Aturdido;
        agenteNavegacion.isStopped = true;
        animador.SetBool("caminando", false);
        animador.SetBool("corriendo", false);
        animador.SetTrigger("aturdido");
        yield return new WaitForSeconds(2.0f);
       agenteNavegacion.isStopped = false;
        estadoActual = EstadoVillano.Corriendo;
    }

    private void ComportamientoCorrer()
    {
        agenteNavegacion.speed = 4.5f;
        animador.SetBool("caminando", false);
        animador.SetBool("corriendo", true);

        agenteNavegacion.SetDestination(camaraJugadorVR.position);

        
        temporizadorAtaque += Time.deltaTime;
        if (temporizadorAtaque >= tiempoEntreAtaques)
        {
        LanzarObjeto();
        temporizadorAtaque = 0f;
        }
    }

    private void VolverACaminar()
    { estadoActual = EstadoVillano.Caminando;
     animador.SetBool("corriendo", false);
     IrAlSiguientePuntoPatrulla();
    }

    private void IrAlSiguientePuntoPatrulla()
    {
        if (puntosPatrulla.Length == 0) return;
        agenteNavegacion.SetDestination(puntosPatrulla[indicePatrulla].position);
        indicePatrulla = (indicePatrulla + 1) % puntosPatrulla.Length;
    }

    
    private void LanzarObjeto()
    {
        if (prefabsProyectiles.Length == 0 || puntoLanzamiento == null) return;

        animador.SetTrigger("lanzar");

        
        int indiceAleatorio = Random.Range(0, prefabsProyectiles.Length);
        GameObject proyectilInstanciado = Instantiate(prefabsProyectiles[indiceAleatorio], puntoLanzamiento.position, puntoLanzamiento.rotation);

        Rigidbody rb = proyectilInstanciado.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direccion = (camaraJugadorVR.position - puntoLanzamiento.position).normalized;
            rb.AddForce(direccion * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
}