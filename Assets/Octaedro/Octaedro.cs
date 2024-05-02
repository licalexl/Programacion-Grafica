using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Octaedro : MonoBehaviour
{
    Matriz modelMatriz;

    // Ángulo de rotación en grados
    float angle = 30.0f;

    // Ángulo de rotación en radianes
    float rad;

    public Material material;
    Vector3[] vertices = {
                           //Base Octaedro
                           new Vector3(1, 0, 0),//0
                           new Vector3(0.5f, 0, 0.87f),//1
                           new Vector3(0.5f, 0.82f, 0.29f),//2                          
                           new Vector3(1, 0.82f, 1.15f),//3                          
                           new Vector3(1.5f, 0.82f, 0.29f),//4
                           new Vector3(1.5f, 0, 0.87f)//5
                                                            };

    //Cube
    //new Vector3(0, 0, 0), //0
    //new Vector3(0, 1, 0), //1
    //new Vector3(1, 1, 0), //2
    //new Vector3(1, 0, 0), //3
    //new Vector3(0, 1, 1), //4
    //new Vector3(0, 0, 1), //5
    //new Vector3(1, 1, 1), //6
    //new Vector3(1, 0, 1)  //7
    //};

    int[] triangles = { 
                        //Octaedro
                        0,2,4,
                        0,1,2,
                        1,3,2,
                        1,5,3,
                        3,5,4,
                        0,5,1,
                        0,4,5,
                        2,3,4 };
    //Cube
    //0, 1, 2,
    //0, 2, 3,
    //1, 0, 5,
    //1, 5, 4,
    //2, 6, 3,
    //6, 7, 3,
    //1, 4, 2,
    //4, 6, 2,
    //5, 0, 3,
    //5, 3, 7,
    //4, 5, 7,
    //4, 7, 6
    //};

    Vector3 initialPosition;

    void Prisma(Vector3[] vertices)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    void Start()
    {
        // Convertir el ángulo de grados a radianes
        rad = angle * Mathf.Deg2Rad;

        // Guardar la posición inicial del objeto
        initialPosition = transform.position;

        // Centrar la pirámide en su centro de masa
        Vector3 center = Vector3.zero;
        foreach (Vector3 vertex in vertices)
        {
            center += vertex;
        }
        center /= vertices.Length;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
        }

        Prisma(vertices); // Llamar a Prisma con los vértices centrados al inicio

        // Iniciar la secuencia de transformaciones como corrutina después de un retraso de 6 segundos
        StartCoroutine(TransformSequence(3.0f));
    }

    // Corrutina para la secuencia de transformaciones
    IEnumerator TransformSequence(float delay)
    {
        yield return new WaitForSeconds(delay);

        yield return StartCoroutine(TranslateZX(0, 3.0f));
        yield return StartCoroutine(Rotate360());

    }

    // Método para trasladar la pirámide en los ejes Z y X
    IEnumerator TranslateZX(float amount, float duration)
    {
        Vector3 targetPosition = initialPosition + new Vector3(amount, 0, amount);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la posición final sea exactamente igual a la deseada
        transform.position = targetPosition;
    }

    // Corrutina para rotar la pirámide 360 grados en los ejes X e Y
    IEnumerator Rotate360()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(180, 180, 0);
        float duration = 5.0f; // Duración total de la rotación en segundos
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la rotación final sea exactamente igual a la deseada
        transform.rotation = endRotation;
    }


    // Corrutina para rotar la pirámide 360 grados en los ejes X e Y

}
