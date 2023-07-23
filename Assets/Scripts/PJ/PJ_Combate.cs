using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Combate : MonoBehaviour
{
    PJ_Movimentacao jogador;
    Rigidbody2D _rb2d;

    private int comboLeve = 1;

    [SerializeField] float ritmoAtaques = 2f;
    [SerializeField] Transform alcanceDoAtaque;
    [SerializeField] float raioDosAtaques;
    [SerializeField] LayerMask inimigos;
    private float delayProximoAtaque = 0f;

    private void Start()
    {
        comboLeve = 1;
        jogador = FindObjectOfType<PJ_Movimentacao>();
        _rb2d = FindObjectOfType<Rigidbody2D>();
    }


    public void AtaqueBasico()
    {

        if (_rb2d.velocity.x == 0 && Time.time >= delayProximoAtaque)
        {
            jogador.DefinirVelocidadeHorizontal(0f);

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
                componente.LevarDano(5f);

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
}
