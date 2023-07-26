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
    [SerializeField]private float _regeneracaoEnergia;
    [SerializeField] private int _nivelJogador = 1;
    [SerializeField] private int _quantidadeExperiencia;

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
    [SerializeField] private float moverDuranteAtaque;

    [SerializeField] List<int> _expPorNivel;


    [Header("Efeitos no inimigo")]

    [SerializeField] private float efeitoEmpurrarDuranteDano;


    private float _tempo = 1f;
    private float _tempoRecarga = 1f;

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

    private void Update()
    {
        RegenegarEnergia();
        SistemaLevelUp();
    }

    private void SistemaLevelUp()
    {
        for (int i = 0; i < _expPorNivel.Count; i++)
        {
            if (_quantidadeExperiencia < _expPorNivel[i])
            {
                _nivelJogador = i + 1;
                break;
            }
            else
            {
                _nivelJogador = _expPorNivel.Count;
            }
        }
    }

    private void RegenegarEnergia()
    {
        if(_rb2d.velocity.x == 0 && _rb2d.velocity.y == 0){
            if (Time.time >= _tempoRecarga && _energiaAtual < _energiaMaxima)
        {
            _energiaAtual += _regeneracaoEnergia * 2;
            _tempoRecarga = Time.fixedTime + 0.5f;
        }
        }
        if (Time.time >= _tempoRecarga && _energiaAtual < _energiaMaxima)
        {
            _energiaAtual += _regeneracaoEnergia;
            _tempoRecarga = Time.fixedTime + 0.5f;
        }
        sliderBarraDeEnergia.value = _energiaAtual;
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
            }

            switch (comboLeve)
            {
                case 1:
                    jogador.animator.SetBool("AtaqueLeve1", true);
                    _energiaAtual -= gastoDeEnergiaAtaqueLeve1;

                    comboLeve++;
                    break;
                case 2:
                    jogador.animator.SetBool("AtaqueLeve1", false);
                    jogador.animator.SetBool("AtaqueLeve2", true);
                    _energiaAtual -= gastoDeEnergiaAtaqueLeve2;
                    danoAtaqueBasico += danoExtraComboAtaqueLeve2;
                    comboLeve++;
                    break;
                case 3:
                    jogador.animator.SetBool("AtaqueLeve2", false);
                    jogador.animator.SetBool("AtaqueLeve3", true);
                    _energiaAtual -= gastoDeEnergiaAtaqueLeve3;
                    danoAtaqueBasico += danoExtraComboAtaqueLeve3;
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

    public void AdicionarPontosDeExperiencia(int quantidade){
        _quantidadeExperiencia += quantidade;
    }
}
