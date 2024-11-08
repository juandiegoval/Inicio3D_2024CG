using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteam : MonoBehaviour
{
    public DoorController puertaAsociada; // Referencia a la puerta que abrir�
    public float velocidadRotacion = 30f; // Velocidad de rotaci�n del item

    void Update()
    {
        // Hace que el �tem rote para hacerlo m�s visible
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Abre la puerta
            if (puertaAsociada != null)
            {
                puertaAsociada.AbrirPuerta();
            }

            // Reproduce un sonido de recolecci�n si lo tiene
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            // Desactiva el renderer del �tem
            GetComponent<Renderer>().enabled = false;

            // Destruye el �tem despu�s de un peque�o delay (por si hay sonido)
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
        // Guarda la rotaci�n inicial de la puerta
        rotacionInicial = transform.rotation;
        // Calcula la rotaci�n objetivo
        rotacionObjetivo = rotacionInicial * Quaternion.Euler(0, abrirHaciaAfuera ? anguloApertura : -anguloApertura, 0);
    }

    void Update()
    {
        if (estaAbierta)
        {
            // Interpola suavemente hacia la rotaci�n objetivo
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