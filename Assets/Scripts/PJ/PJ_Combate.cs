using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Combate : MonoBehaviour
{

    // cache
    PJ_Movimentacao jogador;
    Rigidbody2D _rb2d;

    // variaveis privadas
    [SerializeField]private int comboLeve = 1;
    private float delayProximoAtaque = 0f;
    [SerializeField]private bool podeAtacar = true;

    [Header("Status do Jogador")]
    [SerializeField] float vidaAtual;
    [SerializeField] float vidaMaxima = 100f;
    [SerializeField] int nivelJogador = 1;

    [Header("Ataque")]
    [SerializeField] float danoAtaqueBasico = 5f;


    [Header("Configurações do Jogador")]
    [SerializeField] float ritmoAtaques = 2f;
    [SerializeField] float raioDosAtaques;
    [SerializeField] Transform alcanceDoAtaque;
    [SerializeField] LayerMask inimigos;
    [SerializeField] float moverDuranteAtaque;


    [Header("Efeitos no inimigo")]

    [SerializeField] private float efeitoEmpurrarDuranteDano;

    private void Start()
    {
        comboLeve = 1;
        jogador = GetComponent<PJ_Movimentacao>();
        _rb2d = GetComponent<Rigidbody2D>();
    }


    public void AtaqueBasico()
    {

        if (_rb2d.velocity.x == 0 && podeAtacar)
        {
            podeAtacar = false;
            jogador.DefinirVelocidadeHorizontal(0f);
            if(gameObject.transform.localScale == new Vector3(1,1,1)){
                _rb2d.AddForce(new Vector2(-moverDuranteAtaque, 0));
            } else{
                _rb2d.AddForce(new Vector2(moverDuranteAtaque, 0));
                //transform.position += new Vector3(moverDuranteAtaque,0,0);
            }

            switch (comboLeve)
            {
                case 1:
                    jogador.animator.SetBool("AtaqueLeve1", true);
                    comboLeve++;
                    break;
                case 2:
                    jogador.animator.SetBool("AtaqueLeve1", false);
                    jogador.animator.SetBool("AtaqueLeve2", true);
                    comboLeve++;
                    break;
                case 3:
                    jogador.animator.SetBool("AtaqueLeve2", false);
                    jogador.animator.SetBool("AtaqueLeve3", true);
                    comboLeve = 1;
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
        jogador.animator.SetBool("AtaqueLeve1", false);
        jogador.animator.SetBool("AtaqueLeve2", false);
        jogador.animator.SetBool("AtaqueLeve3", false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(alcanceDoAtaque.position, raioDosAtaques);
    }

    public void DefinirPodeAtacar(){
        podeAtacar =true;
    }
}
