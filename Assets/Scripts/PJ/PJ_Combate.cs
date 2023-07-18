using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Combate : MonoBehaviour
{
    PJ_Movimentacao jogador;
    Rigidbody2D _rb2d;

    private int comboLeve = 1;

    [SerializeField] float ritmoAtaques = 2f;
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
                    jogador.animator.SetTrigger("Ataque Leve 1");
                    comboLeve++;
                    break;
                case 2:
                    jogador.animator.SetTrigger("Ataque Leve 2");
                    comboLeve++;
                    break;
                case 3:
                    jogador.animator.SetTrigger("Ataque Leve 3");
                    comboLeve = 1;
                    break;
            }
            delayProximoAtaque = Time.time + 1f / ritmoAtaques;
        }
    }

    public void ResetarCombo(){
        comboLeve = 1;
    }
}
