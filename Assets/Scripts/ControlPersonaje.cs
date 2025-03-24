using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlPersonaje : MonoBehaviour
{
    [Header("CAMINAR")]
    [SerializeField] private Transform comienzoRayo;
    private bool _caminarDerecha = true;

    [Header("CRISTAL")]
    [SerializeField] private GameObject destelloCristal;

    [Header("REFERENCIAS")]
    private Rigidbody _rb;
    private Animator _animator;
    private GameManager _gameManager;
    private AudioSource _sonidoCristal;




    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _sonidoCristal = GetComponent<AudioSource>();
    }



    private void Update()
    {
        //--> Cambiar de direccion al personaje al presionar Space
        if (Input.GetKeyDown(KeyCode.Space) && _gameManager.juegoIniciado)
        {
            CambiarDireccion();
        }



        //--> Validar si hay algo debajo del personaje y si no hacer la animación de caer
        RaycastHit contacto;

        if (!Physics.Raycast(comienzoRayo.position, -transform.up, out contacto, Mathf.Infinity) && transform.position.y < 0.48)
        {
            _animator.SetTrigger("Caer");
        }



        //--> Reiniciar el juego al estar en una altura < 2
        if (transform.position.y < -2)
        {
            _gameManager.FinalizarJuego();
        }
    }



    private void FixedUpdate()
    {
        //--> Iniciar el juego
        if (!_gameManager.juegoIniciado)
            return;
        else
            _animator.SetTrigger("InicioElJuego");


        //--> Desplazar el personaje
        _rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }



    //--> Metodo para cambiar de dirección al personaje
    private void CambiarDireccion()
    {
        _caminarDerecha =! _caminarDerecha;

        if (_caminarDerecha)
            transform.rotation = Quaternion.Euler(0, 45, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }



    //--> Desaparecer un cristal cuando el personaje lo toca
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cristal"))
        {
            _gameManager.AumentarPuntaje();

            //--> Sonido del cristal
            _sonidoCristal.Play();

            //--> Activar particulas del cristal en la posicion del personaje
            GameObject gameObjectCristal = Instantiate(destelloCristal, comienzoRayo.position, Quaternion.identity);

            //--> Destruir la particulas a los 2 segundos
            Destroy(gameObjectCristal, 2f);

            //--> Destruir el cristal
            Destroy(other.gameObject);
        }
    }
}
