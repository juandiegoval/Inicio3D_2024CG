using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoCayendo : MonoBehaviour
{
    public GameObject[] objetosParaSpawnear;

    // Variables configurables desde el Inspector de Unity
    public float tiempoEntreSpawns = 2f;
    public float rangoSpawnX = 5f;
    public float alturaSpawn = 10f;

    // Variables para controlar los límites de spawn
    public float limiteIzquierdo = -5f;
    public float limiteDerecho = 5f;

    void Start()
    {
        // Iniciar la corrutina que genera objetos
        StartCoroutine(GenerarObjetos());
    }

    IEnumerator GenerarObjetos()
    {
        while (true)
        {
            // Esperar el tiempo especificado
            yield return new WaitForSeconds(tiempoEntreSpawns);

            // Generar una posición aleatoria en X
            float posicionRandomX = Random.Range(limiteIzquierdo, limiteDerecho);

            // Crear el vector de posición
            Vector3 posicionSpawn = new Vector3(posicionRandomX, alturaSpawn, 0f);

            // Seleccionar un objeto aleatorio del array
            int indiceObjeto = Random.Range(0, objetosParaSpawnear.Length);

            // Instanciar el objeto en la posición calculada
            GameObject objetoInstanciado = Instantiate(
                objetosParaSpawnear[indiceObjeto],
                posicionSpawn,
                Quaternion.identity
            );

            // Opcionalmente, destruir el objeto después de cierto tiempo para evitar sobrecarga
            Destroy(objetoInstanciado, 5f);
        }
    }
}