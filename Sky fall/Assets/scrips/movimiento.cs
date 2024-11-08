using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    public float velocidad = 3f;

    private bool moviendoHaciaFinal = true;

    void Start()
    {
        // Guardar la posición inicial del objeto
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Decidir hacia dónde nos movemos
        Vector3 destino = moviendoHaciaFinal ? posicionFinal : posicionInicial;

        // Mover el objeto
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // Si llegamos al destino, cambiar dirección
        if (transform.position == destino)
        {
            moviendoHaciaFinal = !moviendoHaciaFinal;
        }
    }
}