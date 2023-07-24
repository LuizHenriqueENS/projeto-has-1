using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private Animator animatorInimigo;
    [SerializeField] private Slider sliderBarraDeVida;
    [SerializeField] private float efeitoPuloMorte;


    [Header("Status Inimigos")]
    [SerializeField] float vidaAtual;
    [SerializeField] float vidaMaxima = 100f;


    // CACHE
    private Rigidbody2D _rb;
    private PJ_Movimentacao _jogador;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
        sliderBarraDeVida.maxValue = vidaMaxima;
        sliderBarraDeVida.value = vidaMaxima;

        _rb = GetComponent<Rigidbody2D>();
        _jogador = FindObjectOfType<PJ_Movimentacao>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevarDano(float dano, float efeitoEmpurrarDuranteDano)
    {
        vidaAtual -= dano;
        sliderBarraDeVida.value -= dano;
        if (_jogador.LadoQueJogadorEstaOlhando() == new Vector3(1, 1, 1))
        {
            _rb.AddForce(new Vector2(-efeitoEmpurrarDuranteDano, 0), ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(new Vector2(efeitoEmpurrarDuranteDano, 0), ForceMode2D.Impulse);
        }
        animatorInimigo.SetTrigger("Dano");
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        animatorInimigo.SetBool("Morto", true);
        _rb.AddForce(new Vector2(0, efeitoPuloMorte));
        sliderBarraDeVida.gameObject.SetActive(false);
        gameObject.layer = 8;
        Destroy(gameObject, 3f);
    }
}
