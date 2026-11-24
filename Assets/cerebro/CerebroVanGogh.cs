using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CerebroVanGogh : MonoBehaviour
{
    [Header("Patrulla")]
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private float velocidadCaminar = 1.5f;

    [Header("Persecución")]
    [SerializeField] private float velocidadCorrer = 3.5f;
    [SerializeField] private float rangoVision = 8f;
    [SerializeField, Range(10, 180)] private float anguloVision = 110f;
    [SerializeField] private float segundosParaPerderte = 4f;
    [SerializeField] private LayerMask capasParedes;

    [Header("Ataque")]
    [SerializeField] private float rangoAtaque = 1.2f;
    [SerializeField] private float pausaTrasGolpe = 2f;

    [Header("Animación")]
    [SerializeField] private Animator animator;   

    private NavMeshAgent agente;
    private Transform jugador;
    private bool persiguiendo;
    private float tiempoSinVerte;
    private float pausaHasta;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        jugador = Camera.main.transform;
        IrAPuntoAleatorio();
    }

    void Update()
    {
        if (NivelManager.Instancia != null && NivelManager.Instancia.JuegoTerminado)
        {
            agente.isStopped = true;
            animator.SetBool("Corriendo", false);
            return;
        }

        
        if (Time.time < pausaHasta)
        {
            agente.isStopped = true;
            animator.SetBool("Corriendo", false);
            return;
        }
        agente.isStopped = false;

        if (PuedeVerAlJugador())
        {
            persiguiendo = true;
            tiempoSinVerte = 0f;
        }
        else if (persiguiendo)
        {
            tiempoSinVerte += Time.deltaTime;
            if (tiempoSinVerte >= segundosParaPerderte)
            {
                persiguiendo = false;
                IrAPuntoAleatorio();
            }
        }

        if (persiguiendo) Perseguir();
        else Patrullar();
    }

    private void Patrullar()
    {
        agente.speed = velocidadCaminar;
        animator.SetBool("Corriendo", false);

        if (!agente.pathPending && agente.remainingDistance <= 0.5f)
            IrAPuntoAleatorio();
    }

    private void Perseguir()
    {
        agente.speed = velocidadCorrer;
        animator.SetBool("Corriendo", true);
        agente.SetDestination(jugador.position);

        if (DistanciaPlana(jugador.position) <= rangoAtaque)
            Atrapar();
    }

    private void Atrapar()
    {
        NivelManager.Instancia.PerderVida();
        pausaHasta = Time.time + pausaTrasGolpe;
        persiguiendo = false;
        IrAPuntoAleatorio();
    }

    private bool PuedeVerAlJugador()
    {
        Vector3 ojos = transform.position + Vector3.up * 1.6f;
        Vector3 haciaJugador = jugador.position - ojos;

        if (DistanciaPlana(jugador.position) > rangoVision) return false;

        float angulo = Vector3.Angle(transform.forward, new Vector3(haciaJugador.x, 0, haciaJugador.z));
        if (angulo > anguloVision * 0.5f && !persiguiendo) return false;

        
        return !Physics.Linecast(ojos, jugador.position, capasParedes);
    }

    private float DistanciaPlana(Vector3 destino)
    {
        Vector3 a = transform.position; a.y = 0;
        Vector3 b = destino; b.y = 0;
        return Vector3.Distance(a, b);
    }

    private void IrAPuntoAleatorio()
    {
        if (puntosPatrulla == null || puntosPatrulla.Length == 0) return;
        agente.SetDestination(puntosPatrulla[Random.Range(0, puntosPatrulla.Length)].position);
    }
}
