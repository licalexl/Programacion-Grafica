using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Piramide1 : MonoBehaviour
{
    public Material material;
    Matriz matriz; // Referencia al script Matriz

    Vector3[] vertices = {
        new Vector3(0, Mathf.Sqrt(6) / 3, Mathf.Sqrt(3) / 3),  // 0
        new Vector3(0, 0, 0),                                   // 1a
        new Vector3(0.5f, 0, Mathf.Sqrt(3) / 2),                // 2b
        new Vector3(0, Mathf.Sqrt(6) / 3, Mathf.Sqrt(3) / 3),  // 3
        new Vector3(-0.5f, 0, Mathf.Sqrt(3) / 2),               // 4c
        new Vector3(0, Mathf.Sqrt(6) / 3, Mathf.Sqrt(3) / 3)   // 5
    };

    int[] triangles = {
        4, 2, 5,  // Cara dentro
        1, 4, 3,
        1, 0, 2,
        1, 2, 4
    };

    Vector2[] uvs = {
        new Vector2(0, 0),
        new Vector2(0.25f, 0.5f),
        new Vector2(0.5f, 0),
        new Vector2(0.5f, 1),
        new Vector2(0.75f, 0.5f),
        new Vector2(1, 0)
    };

    Vector3 pivot; // Pivote en el centro de la figura

    public float translationDistance; // Distancia de traslación
    public float rotationXAngle; // Ángulo de rotación en el eje X
    public float rotationYAngle; // Ángulo de rotación en el eje Y
    public float rotationZAngle; // Ángulo de rotación en el eje Z

    void Start()
    {
        Prisma(vertices);

        // Calcular el pivote en el centro de la figura
        CalculatePivot();

        // Obtener referencia al script Matriz
        matriz = GetComponent<Matriz>();

        // Iniciar la corrutina de animación
        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        // Traslación
        yield return Move(translationDistance, 0.05f);
        yield return new WaitForSeconds(2f);

        // Rotación en X
        yield return RotateX(rotationXAngle, 0.05f);
        yield return new WaitForSeconds(2f);

        // Rotación en Y
        yield return RotateY(rotationYAngle, 0.05f);
        yield return new WaitForSeconds(2f);

        // Rotación en Z
        yield return RotateZ(rotationZAngle, 0.05f);
    }

    void CalculatePivot()
    {
        pivot = Vector3.zero;
        foreach (Vector3 vertex in vertices)
        {
            pivot += vertex;
        }
        pivot /= vertices.Length;
    }

    IEnumerator Move(float distance, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.Traslation(vertices[i], new Vector4(distance * t, 0, 0, 0));
            }
            UpdateMesh();
            yield return null;
        }
    }

    IEnumerator RotateX(float angle, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.RotX(vertices[i], angle * t);
            }
            UpdateMesh();
            yield return null;
        }
    }

    IEnumerator RotateY(float angle, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.RotY(vertices[i], angle * t);
            }
            UpdateMesh();
            yield return null;
        }
    }

    IEnumerator RotateZ(float angle, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = matriz.RotZ(vertices[i], angle * t);
            }
            UpdateMesh();
            yield return null;
        }
    }

    // Método para crear la malla
    void Prisma(Vector3[] vertices)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    // Método para actualizar la malla con los nuevos vértices
    void UpdateMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        Prisma(vertices);
    }
}
