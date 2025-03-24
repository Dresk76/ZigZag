using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("GAME MANAGER")]
    public bool juegoIniciado;
    public int puntajeActual;

    [Header("UI")]
    [SerializeField] private TMP_Text textoPuntaje;
    [SerializeField] private TMP_Text textoPuntajeMaximo;
    [SerializeField] private GameObject enter;
    [SerializeField] private GameObject space;

    [Header("REFERENCIAS")]
    private Ruta _ruta;




    private void Awake()
    {
        _ruta = FindObjectOfType<Ruta>();

        //--> Al iniciar mostrar el puntaje maximo que se ha guardado
        textoPuntajeMaximo.text = ObtenerPuntajeMaximo().ToString();
    }



    private void Update()
    {
        //--> Cambiar el estado de juegoIniciado a true al presionar Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IniciarJuego();
        }

        //--> Salir del juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }



    //--> Cambiar el estado de juegoIniciado a true e invoca el metodo de IniciarConstruccion() de Ruta()
    public void IniciarJuego()
    {
        juegoIniciado = true;
        _ruta.IniciarConstruccion();

        enter.SetActive(false);
        space.SetActive(true);
    }



    //--> Al perder Reinicia la escena
    public void FinalizarJuego()
    {
        SceneManager.LoadScene(0);
    }



    //--> Sumar el puntaje obtenido de los cristales en la UI
    public void AumentarPuntaje()
    {
        puntajeActual++;
        textoPuntaje.text = puntajeActual.ToString();

        /* 
            Si el puntaje actual obtenido es mayor al que esta guardado, guardar 
            con PlayerPrefs el puntaje maximo obtenido y mostrarlo en la UI
        */
        if (puntajeActual > ObtenerPuntajeMaximo())
        {
            PlayerPrefs.SetInt("PuntajeMaximo", puntajeActual);
            textoPuntajeMaximo.text = puntajeActual.ToString();
        }
    }



    //--> Mostrar con PlayerPrefs el puntaje maximo obtenido
    public int ObtenerPuntajeMaximo()
    {
        int i = PlayerPrefs.GetInt("PuntajeMaximo");
        return i;
    }
}
