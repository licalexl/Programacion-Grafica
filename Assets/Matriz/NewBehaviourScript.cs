using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   Matriz matriz = new Matriz();

    public GameObject cube;


    private void FixedUpdate()
    {
        //cube.transform.position = matriz.Traslation(cube.transform.position, new Vector3(0.08f, 0, 0.02f));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.position = matriz.RotZ(this.transform.position, 30);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.position = matriz.RotX(this.transform.position, 30);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.transform.position = matriz.RotY(this.transform.position, 30);
        }
    }
}
