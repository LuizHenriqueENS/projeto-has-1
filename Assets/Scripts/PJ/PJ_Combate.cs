using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PJ_Combate : MonoBehaviour
{

    // cache
    PJ_Movimentacao jogador;
    Rigidbody2D _rb2d;

    // variaveis privadas
    private int comboLeve = 1;
    private float delayProximoAtaque = 0f;
    private bool podeAtacar = true;
    private float cacheAtaqueBasico;

    [Header("Status do Jogador")]
    [SerializeField] private float _vidaAtual;
    [SerializeField] private float _vidaMaxima = 100f;
    [SerializeField] private float _energiaAtual;
    [SerializeField] private float _energiaMaxima = 100f;

    [SerializeField] private int _nivelJogador = 1;

    [Header("Ataque")]
    [SerializeField] float danoAtaqueBasico = 5f;
    [SerializeField] private float gastoDeEnergiaAtaqueLeve1 = 5f;
    [SerializeField] private float gastoDeEnergiaAtaqueLeve2 = 10f;
    [SerializeField] private float gastoDeEnergiaAtaqueLeve3 = 15f;
    [SerializeField] private float danoExtraComboAtaqueLeve2 = 10f;
    [SerializeField] private float danoExtraComboAtaqueLeve3 = 15f;

    [Header("Configurações do Jogador")]
    [SerializeField] float ritmoAtaques = 2f;
    [SerializeField] float raioDosAtaques;
    [SerializeField] Transform alcanceDoAtaque;
    [SerializeField] LayerMask inimigos;
    [SerializeField] Slider sliderBarraDeVida;
    [SerializeField] Slider sliderBarraDeEnergia;


    float moverDuranteAtaque;


    [Header("Efeitos no inimigo")]

    [SerializeField] private float efeitoEmpurrarDuranteDano;

    private void Start()
    {
        // configurações iniciais
        comboLeve = 1;
        sliderBarraDeEnergia.maxValue = _energiaMaxima;
        sliderBarraDeEnergia.value = _energiaMaxima;
        _energiaAtual = _energiaMaxima;

        sliderBarraDeVida.maxValue = _vidaMaxima;
        sliderBarraDeVida.value = _vidaMaxima;
        _vidaAtual = _vidaMaxima;

        cacheAtaqueBasico = danoAtaqueBasico;
        // caches iniciais
        jogador = GetComponent<PJ_Movimentacao>();
        _rb2d = GetComponent<Rigidbody2D>();
    }


    public void AtaqueBasico()
    {

        if (_rb2d.velocity.x == 0 && podeAtacar && (_energiaAtual >= gastoDeEnergiaAtaqueLeve3 || _energiaAtual >= gastoDeEnergiaAtaqueLeve2 || _energiaAtual >= gastoDeEnergiaAtaqueLeve1))
        {
            podeAtacar = false;
            jogador.DefinirVelocidadeHorizontal(0f);
            if (gameObject.transform.localScale == new Vector3(1, 1, 1))
            {
                _rb2d.AddForce(new Vector2(-moverDuranteAtaque, 0));
            }
            else
            {
                _rb2d.AddForce(new Vector2(moverDuranteAtaque, 0));
                //transform.position += new Vector3(moverDuranteAtaque,0,0);
            }

            switch (comboLeve)
            {
                case 1:
                    jogador.animator.SetBool("AtaqueLeve1", true);
                    _energiaAtual -= gastoDeEnergiaAtaqueLeve1;
                    sliderBarraDeEnergia.value = _energiaAtual;
                    comboLeve++;
                    break;
                case 2:
                    jogador.animator.SetBool("AtaqueLeve1", false);
                    jogador.animator.SetBool("AtaqueLeve2", true);
                    _energiaAtual -= gastoDeEnergiaAtaqueLeve2;
                    danoAtaqueBasico += danoExtraComboAtaqueLeve2;
                    sliderBarraDeEnergia.value = _energiaAtual;
                    comboLeve++;
                    break;
                case 3:
                    jogador.animator.SetBool("AtaqueLeve2", false);
                    jogador.animator.SetBool("AtaqueLeve3", true);
                    _energiaAtual -= gastoDeEnergiaAtaqueLeve3;
                    danoAtaqueBasico += danoExtraComboAtaqueLeve3;
                    sliderBarraDeEnergia.value = _energiaAtual;
                    comboLeve++;
                    break;
            }
            delayProximoAtaque = Time.time + 1f / ritmoAtaques;
        }
    }

    public void AtacarInimigosNoRange()
    {
        var inimigosNoAlcance = Physics2D.OverlapCircleAll(alcanceDoAtaque.position, raioDosAtaques, inimigos);
        foreach (var inimigo in inimigosNoAlcance)
        {
            var componente = inimigo.GetComponent<Inimigo>();
            if (inimigo.gameObject.layer == 7)
            {
                componente.LevarDano(danoAtaqueBasico, efeitoEmpurrarDuranteDano);
            }
        }
    }
    public void ResetarCombo()
    {

        comboLeve = 1;
        danoAtaqueBasico = cacheAtaqueBasico;
        jogador.animator.SetBool("AtaqueLeve1", false);
        jogador.animator.SetBool("AtaqueLeve2", false);
        jogador.animator.SetBool("AtaqueLeve3", false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(alcanceDoAtaque.position, raioDosAtaques);
    }

    public void DefinirPodeAtacar()
    {
        podeAtacar = true;
    }
}
