using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JogadorControleBotoes : MonoBehaviour, IPointerDownHandler
{

	[SerializeField] Acoes acoes;

	JogadorMovimentacao m_movimentoJogador;
	JogadorCombate m_CombateJogador;

	// Start is called before the first frame update
	void Start()
	{
		m_movimentoJogador = FindAnyObjectByType<JogadorMovimentacao>();
		m_CombateJogador = FindAnyObjectByType<JogadorCombate>();
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		switch(acoes)
		{
			case Acoes.Pular:
				m_movimentoJogador.Pular();
				break;
			case Acoes.AtaqueLeve:
				m_CombateJogador.AtaqueLeve();
				break;
			default:
				break;
		}
	}

	private enum Acoes
	{
		Nenhuma,
		Pular,
		AtaqueLeve,
	}
}
