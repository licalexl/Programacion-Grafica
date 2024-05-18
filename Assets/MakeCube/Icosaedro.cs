using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Icosaedro : MonoBehaviour
{
    

    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0, 45, 0); // Velocidad de rotación en grados por segundo

    [SerializeField]
    private Vector3 rotationSpeed1 = new Vector3(0, 45, 0); // Velocidad de rotación en grados por segundo

    [SerializeField]
    private Vector3 rotationSpeed2 = new Vector3(0, 45, 0); // Velocidad de rotación en grados por segundo

    public float speedx;
    public float speedy;
    public float speedz;





    public float a = 0.364f;
    public float b = 0.315f;

    public Material material;

    Vector3[] vertices = {
                           new Vector3(0f, 1f, 0f),
                           new Vector3(-0.7236f, 0.44721f, -0.52572f),
                           new Vector3(0.27639f, 0.44721f, -0.85064f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(0.89442f, 0.44721f, 0f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(0.27639f, 0.44721f, 0.85064f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(-0.7236f, 0.44721f, 0.52572f),
                           new Vector3(0f, 1f, 0f),
                           new Vector3(-0.7236f, 0.44721f, -0.52572f),
                           new Vector3(-0.27639f, -0.44721f, -0.85064f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(-0.89442f, -0.44721f, 0f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(-0.27639f, -0.44721f, 0.85064f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(0.7236f, -0.44721f, 0.52572f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(0.7236f, -0.44721f, -0.52572f),
                           new Vector3(0f, -1f, 0f),
                           new Vector3(-0.27639f, -0.44721f, -0.85064f)
                         };
    int[] triangles = {
                        //Icosaedro

                        //Arriba
                        0, 2, 1,//si 0
                        2, 3, 4,//no 1
                        5, 6, 4,//no 2
                        7, 8, 6,//si 3 
                        9,10, 8,//si 4

                        ////Medio
                        10, 11, 13,//5
                        10,13, 8,//6
                        8, 13, 15,//7
                        8, 15, 6,//8
                        6, 15, 17,//9
                        6, 17, 4,//10 no
                        19, 4, 17,//11 no
                        2, 4, 19,//12 no
                        2, 19, 21,//13
                        2, 21, 1,//14

                        ////Abajo
                        11, 12, 13,//15
                        13, 14, 15,//16
                        15, 16, 17,//17
                        17, 18, 19,//18
                        19, 20, 21//19
                      };
    Vector2[] uvs; // Declarar el arreglo de coordenadas UV sin inicializarlo

    void Start()
    {
        // Inicializa las coordenadas UV
        InitializeUVs();

        // Llama al método que inicializa y actualiza la malla del icosaedro
        Icosaedro_(vertices);

        // Comienza la corutina para actualizar la malla periódicamente
        StartCoroutine(Actualizarcubo());

        rotate();

    }
    

    IEnumerator Actualizarcubo()
    {
        while (true) // Bucle infinito
        {
            
            InitializeUVs();
            Icosaedro_(vertices);
            UpdateMesh();
            rotate();
            yield return null;
        }
    }

    void Icosaedro_(Vector3[] vertices)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs; // Asigna las coordenadas UV al mesh
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    void UpdateMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        Icosaedro_(vertices);

    }

    // Método para inicializar las coordenadas UV
    void InitializeUVs()
    {
        uvs = new Vector2[]
        {
            new Vector2(0.091f, 0.472f),
            new Vector2(0f, 0.315f),
            new Vector2(0.182f, 0.315f),
            new Vector2(0.273f, 0.472f),
            new Vector2(a, b), // Aquí reemplaza a y b por los valores deseados
            new Vector2(0.455f, 0.472f),
            new Vector2(0.545f, 0.315f),
            new Vector2(0.636f, 0.472f),
            new Vector2(0.727f, 0.315f),
            new Vector2(0.818f, 0.472f),
            new Vector2(0.909f, 0.315f),
            new Vector2(1f, 0.157f),
            new Vector2(0.909f, 0f),
            new Vector2(0.818f, 0.157f),
            new Vector2(0.727f, 0f),
            new Vector2(0.636f, 0.157f),
            new Vector2(0.545f, 0f),
            new Vector2(0.455f, 0.157f),
            new Vector2(0.364f, 0f),
            new Vector2(0.273f, 0.157f),
            new Vector2(0.182f, 0f),
            new Vector2(0.091f, 0.157f),
        };
    }

    public void rotate()
    {

        transform.Rotate(rotationSpeed * speedx);
        transform.Rotate(rotationSpeed1 * speedy);
        transform.Rotate(rotationSpeed2 * speedz);

    }

}


