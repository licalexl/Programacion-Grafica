using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Piramide2 : MonoBehaviour
{
    Matriz modelMatriz;

    // Ángulo de rotación en grados
    float angle = 30.0f;

    // Ángulo de rotación en radianes
    float rad;

    public Material material;
    Vector3[] vertices = {
                           //piramide
                           new Vector3(0, 0, 0),//0 a
                           new Vector3(1, 0, 0),//1 b
                           new Vector3(0.5f, 0, 0.87f),//2 c
                           new Vector3(0.5f, 0.82f, 0.29f)//3 d
                                                                    };

    int[] triangles = {
                        //piramide
                        0, 1, 2,
                        0, 3, 1,
                        1, 3, 2,
                        2, 3, 0 };

    // Variable para almacenar la posición inicial del objeto
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
        modelMatriz = new Matriz();

        // Guardar la posición inicial del objeto
        initialPosition = transform.position;

        // Convertir el ángulo de grados a radianes
        rad = angle * Mathf.Deg2Rad;

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

        // Iniciar la secuencia de transformaciones como corrutina
        StartCoroutine(TransformSequence());
    }

    // Corrutina para la secuencia de transformaciones
    IEnumerator TransformSequence()
    {
        yield return StartCoroutine(TranslateZX(-0.5f, 3.0f));
        yield return StartCoroutine(RotateXY(180.0f, 3.0f));
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

    // Método para rotar la pirámide en los ejes X e Y
    IEnumerator RotateXY(float targetAngle, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetAngle, targetAngle, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la rotación final sea exactamente igual a la deseada
        transform.rotation = targetRotation;
    }
}
