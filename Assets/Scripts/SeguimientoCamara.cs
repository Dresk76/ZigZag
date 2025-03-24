using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("CAMARA")]
    [SerializeField] private Transform objetivo;
    private Vector3 _diferencia;



    private void Awake()
    {
        _diferencia = transform.position - objetivo.position;
    }



    private void LateUpdate()
    {
        transform.position = objetivo.position + _diferencia;
    }
}
