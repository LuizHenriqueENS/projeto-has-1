using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] Animator animatorInimigo;
    [SerializeField] Slider sliderBarraDeVida;


    [Header("Status Inimigos")]
    [SerializeField] float vidaAtual;
    [SerializeField] float vidaMaxima = 100f;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
        sliderBarraDeVida.maxValue = vidaMaxima;
        sliderBarraDeVida.value = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevarDano(float dano)
    {
        vidaAtual -= dano;
        sliderBarraDeVida.value -= dano;
        animatorInimigo.SetTrigger("Dano");
        if (vidaAtual <= 0)
        {
            animatorInimigo.SetBool("Morto", true);
            gameObject.layer = 8;
            Destroy(gameObject, 3f);
        }
    }
}
