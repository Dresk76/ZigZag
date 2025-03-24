using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta : MonoBehaviour
{
    [Header("RUTA")]
    [SerializeField] private GameObject prefabRuta;
    [SerializeField] private Vector3 ultimaPosicion;
    [SerializeField] private float diferencia = 0.7071068f;
    private int _cuentaRuta = 0;





    //--> Cada 0.5 segundos invoca el metodo CrearNuevaParteRuta()
    public void IniciarConstruccion()
    {
        InvokeRepeating("CrearNuevaParteRuta", 1f, 0.5f);
    }



    public void CrearNuevaParteRuta()
    {
        Vector3 nuevaPosicion = Vector3.zero;
        float opcion = Random.Range(0, 100);

        if (opcion < 50)
        {
            nuevaPosicion = new Vector3(ultimaPosicion.x + diferencia, ultimaPosicion.y, ultimaPosicion.z + diferencia);
        }
        else
        {
            nuevaPosicion = new Vector3(ultimaPosicion.x - diferencia, ultimaPosicion.y, ultimaPosicion.z + diferencia);
        }

        GameObject gameObjectPrefabRuta = Instantiate(prefabRuta, nuevaPosicion, Quaternion.Euler(0, 45, 0));
        ultimaPosicion = gameObjectPrefabRuta.transform.position;

        _cuentaRuta++;

        //--> Activar cada 5 un cristal que esta ya como hijo en el prefabRuta
        if (_cuentaRuta % 5 == 0)
        {
            gameObjectPrefabRuta.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
