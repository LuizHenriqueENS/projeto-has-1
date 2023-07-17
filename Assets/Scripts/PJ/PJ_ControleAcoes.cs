using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PJ_ControleAcoes : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string acao;
    PJ_Movimentacao movimentacao;
    PJ_Combate combate;


    private void Start()
    {
        movimentacao = FindObjectOfType<PJ_Movimentacao>();
        combate = FindObjectOfType<PJ_Combate>();
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        switch (acao)
        {
            case "Pular":
                movimentacao.Pular();
                break;
            case "Ataque Leve":
                combate.AtaqueBasico();
                break;
        }
    }
}
