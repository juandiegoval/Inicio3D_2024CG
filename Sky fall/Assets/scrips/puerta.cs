using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour
{
    public float velocidadApertura = 2f;
    public float anguloApertura = 90f;
    private bool estaAbierta = false;
    private Quaternion rotacionInicial;
    private Quaternion rotacionFinal;

    void Start()
    {
        // Guardar la rotaci�n inicial de la puerta
        rotacionInicial = transform.rotation;

        // Calcular la rotaci�n cuando la puerta est� abierta
        rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);
    }

    void Update()
    {
        if (estaAbierta)
        {
            // Si la puerta est� abierta, girarla suavemente
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                rotacionFinal,
                velocidadApertura * Time.deltaTime
            );
        }
    }

    // M�todo p�blico para abrir la puerta
    public void AbrirPuerta()
    {
        estaAbierta = true;
    }

    // M�todo p�blico para cerrar la puerta
    public void CerrarPuerta()
    {
        estaAbierta = false;
        transform.rotation = rotacionInicial;
    }
}
