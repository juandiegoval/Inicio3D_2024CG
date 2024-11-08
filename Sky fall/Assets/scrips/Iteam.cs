using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteam : MonoBehaviour
{
    public DoorController puertaAsociada; // Referencia a la puerta que abrirá
    public float velocidadRotacion = 30f; // Velocidad de rotación del item

    void Update()
    {
        // Hace que el ítem rote para hacerlo más visible
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Abre la puerta
            if (puertaAsociada != null)
            {
                puertaAsociada.AbrirPuerta();
            }

            // Reproduce un sonido de recolección si lo tiene
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            // Desactiva el renderer del ítem
            GetComponent<Renderer>().enabled = false;

            // Destruye el ítem después de un pequeño delay (por si hay sonido)
            Destroy(gameObject, 0.5f);
        }
    }
}

// DoorController.c

public class DoorController : MonoBehaviour
{
    public float velocidadApertura = 2f;
    public float anguloApertura = 90f;
    public bool abrirHaciaAfuera = true;

    private bool estaAbierta = false;
    private Quaternion rotacionInicial;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        // Guarda la rotación inicial de la puerta
        rotacionInicial = transform.rotation;
        // Calcula la rotación objetivo
        rotacionObjetivo = rotacionInicial * Quaternion.Euler(0, abrirHaciaAfuera ? anguloApertura : -anguloApertura, 0);
    }

    void Update()
    {
        if (estaAbierta)
        {
            // Interpola suavemente hacia la rotación objetivo
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                rotacionObjetivo,
                velocidadApertura * Time.deltaTime
            );
        }
    }

    public void AbrirPuerta()
    {
        estaAbierta = true;
    }
}