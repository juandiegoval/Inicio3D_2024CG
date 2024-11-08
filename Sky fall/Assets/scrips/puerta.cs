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
        // Guardar la rotación inicial de la puerta
        rotacionInicial = transform.rotation;

        // Calcular la rotación cuando la puerta esté abierta
        rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);
    }

    void Update()
    {
        if (estaAbierta)
        {
            // Si la puerta está abierta, girarla suavemente
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                rotacionFinal,
                velocidadApertura * Time.deltaTime
            );
        }
    }

    // Método público para abrir la puerta
    public void AbrirPuerta()
    {
        estaAbierta = true;
    }

    // Método público para cerrar la puerta
    public void CerrarPuerta()
    {
        estaAbierta = false;
        transform.rotation = rotacionInicial;
    }
}
