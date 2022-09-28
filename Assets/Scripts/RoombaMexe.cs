using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMexe : MonoBehaviour
{
    [SerializeField]
    private float distFrontal = 2;
    [SerializeField]
    private float distLateral = 2;
    [SerializeField]
    private float velocidade = 2;
    [SerializeField]
    private float velRot = 2;
    [SerializeField]
    private float velDesac = 5;
    [SerializeField]
    public bool DEBUG = false;

    private float acelera;
    private float novoAng = 120;
    private Quaternion novoQuat = Quaternion.identity;
    private int esqDir = 1;
    private bool freando = false;

    private Vector3 REsq;
    private Vector3 RDir;

    void Update()
    {
        if (DEBUG)
        {
            REsq = transform.position + (transform.forward + transform.right * distLateral * -1);
            RDir = transform.position + (transform.forward + transform.right * distLateral);
            Debug.DrawLine(transform.position, transform.position + transform.forward * distFrontal, Color.blue);
            Debug.DrawLine(transform.position, REsq, Color.red);
            Debug.DrawLine(transform.position, RDir, Color.red);
        }

        if (freando) 
        {
            acelera = Mathf.Lerp(acelera, 0, Time.deltaTime * velDesac);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, novoQuat, velRot * Time.deltaTime);
            if (Mathf.DeltaAngle(transform.rotation.eulerAngles.y , novoQuat.eulerAngles.y) < 5) { freando = false; }
        } else
            acelera = Mathf.Lerp(acelera, velocidade, Time.deltaTime);

        if (
          (Physics.Raycast(transform.position, transform.forward, distFrontal)
        || Physics.Raycast(transform.position, transform.forward + transform.right, distLateral)
        || Physics.Raycast(transform.position, transform.forward + transform.right * -1, distLateral) )
        && !freando)
        {
            if (Physics.Raycast(transform.position, transform.forward + transform.right, distLateral))
                esqDir = 1;
            else if (Physics.Raycast(transform.position, transform.forward + transform.right * -1, distLateral))
                esqDir = 1;
            novoAng = transform.rotation.eulerAngles.y + Random.Range(60, 110) * esqDir;
            novoQuat = Quaternion.Euler(0, novoAng, 0);
            freando = true;
        }

        transform.Translate(Vector3.forward * acelera * Time.deltaTime);

    }
}
