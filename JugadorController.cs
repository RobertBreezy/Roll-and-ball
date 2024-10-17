using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JugadorController : MonoBehaviour
{
    public float velocidad;
    public Text textoContador, textoGanar, textoTiempo;
    public float tiempoLimite = 60.0f;

    private Rigidbody rb;
    private int contador;
    private bool juegoTerminado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        setTextoContador();
        textoContador.text = "";
        textoGanar.enabled = false; 
        StartCoroutine(IniciarTemporizador());
    }

    void FixedUpdate()
    {
        if (!juegoTerminado)
        {
            float movimientoH = Input.GetAxis("Horizontal");
            float movimientoV = Input.GetAxis("Vertical");

            Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

            rb.AddForce(movimiento * velocidad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!juegoTerminado && other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            contador = contador + 1;
            setTextoContador();
        }
    }

    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();
        if (contador >= 12)
        {
            GanarJuego();
        }
    }

    void GanarJuego()
    {
        juegoTerminado = true;
        textoGanar.text = "¡Ganaste!";
        ActivarTextoGanar();
    }

    void ActivarTextoGanar()
    {
        textoGanar.enabled = true;
    }

    IEnumerator IniciarTemporizador()
    {
        float tiempoRestante = tiempoLimite;

        while (tiempoRestante > 0 && !juegoTerminado)
        {
            yield return new WaitForSeconds(1.0f);
            tiempoRestante -= 1.0f;
            textoTiempo.text = "Tiempo: " + Mathf.RoundToInt(tiempoRestante).ToString() + ":00";

            if (tiempoRestante <= 0)
            {
                PerderJuego();
            }
        }
    }

    void PerderJuego()
    {
        juegoTerminado = true;
        textoGanar.text = "¡Perdiste! Tiempo agotado.";
        ActivarTextoGanar();
    }
}
