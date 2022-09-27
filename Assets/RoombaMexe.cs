using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMexe : MonoBehaviour
{
    [SerializeField]
    private float dist = 2;
    [SerializeField]
    private float distLateral = 2;
    [SerializeField]
    private float velocidade = 2;
    [SerializeField]
    private float velRot = 2;

    private float acelera;

    private Vector3 REsq;
    private Vector3 RDir;

    void Update()
    {
        REsq = transform.position + (transform.forward + transform.right * -1);
        RDir = transform.position + (transform.forward + transform.right);

        //DEBUG
        Debug.DrawLine(transform.position, transform.position + transform.forward * dist, Color.blue);
        Debug.DrawLine(transform.position, REsq, Color.red);
        Debug.DrawLine(transform.position, RDir, Color.red);
        //

        if (Physics.Raycast(transform.position, transform.forward, dist * 0.8f))
        {
            acelera = Mathf.Lerp(acelera, 0, Time.deltaTime);
        }
        if (Physics.Raycast(transform.position, transform.forward, dist)
        ||  Physics.Raycast(transform.position, REsq, distLateral)
        ||  Physics.Raycast(transform.position, RDir, distLateral))
        {
            print(Physics.Raycast(transform.position, REsq, distLateral));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0), velRot * Time.deltaTime);
        } else
            acelera = Mathf.Lerp(acelera, velocidade, Time.deltaTime);

        transform.Translate(Vector3.forward * acelera * Time.deltaTime);

    }
}
