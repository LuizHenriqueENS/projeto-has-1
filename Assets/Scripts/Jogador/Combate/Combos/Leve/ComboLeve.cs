using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboLeve : MonoBehaviour
{

	Jogador m_jogador;

	private bool m_PodeAtacar = true;
	private int m_ContagemCombo = 0;

	[Header("Configurações ataques")]
	[SerializeField] private Transform m_PosicaoAtaque;
	[SerializeField] private float m_RaioAtaquesLeves;
	[SerializeField] private LayerMask m_inimigos;


	void Start()
	{
		m_jogador = GetComponent<Jogador>();
	}

	public void IniciarCombo()
	{
		if (m_PodeAtacar && m_ContagemCombo <= 3)
		{
			m_jogador.animator.SetTrigger("Proximo ataque");
			m_ContagemCombo++;
		}
		if (m_ContagemCombo > 3)
		{
			m_PodeAtacar = false;
			Invoke(nameof(DelayAtaque),.0f);
		}

	}
	
	public void CausarDano()
	{
		Collider2D[] inimigos = Physics2D.OverlapCircleAll(m_PosicaoAtaque.position, m_RaioAtaquesLeves, m_inimigos);

		foreach(var inimigo in inimigos)
		{
			var s = inimigo.GetComponent<Inimigo>();
			s.LevarDano(1000, 0);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(m_PosicaoAtaque.position, m_RaioAtaquesLeves);
	}

	void DelayAtaque()
	{
		m_PodeAtacar = true;
		m_ContagemCombo= 0;
	}
}
