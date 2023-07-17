using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PJ_ControleAcoes : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string acao;
    PJ_Movimentacao jogador;


    private void Start()
    {
        jogador = FindObjectOfType<PJ_Movimentacao>();
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        switch (acao)
        {
            case "Pular":
                jogador.Pular();
                break;
            case "Ataque Leve":
                Debug.Log("Ataque Leve");
                break;
        }
    }
}
