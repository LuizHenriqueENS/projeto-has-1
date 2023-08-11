using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Inimigo", order = 0)]
public class MoldeInimigo : ScriptableObject
{
	

	[Header("Status")]
	[Space]
	public float m_VidaMaxima;
	public float m_Dano;
	public float m_VidaAtual;

	[Header("Ao morrer...")]
	public float m_PularAoMorrer;
	public int m_ExpParaOJogador;

}
