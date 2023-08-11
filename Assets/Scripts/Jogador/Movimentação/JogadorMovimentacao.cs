using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class JogadorMovimentacao : MonoBehaviour
{

	#region Instancias
	Joystick m_joystick;
	Jogador m_jogador;
	#endregion

	#region Checagens
	private bool m_OlhandoParaDireita = false;
	private bool m_EstaNoChao;
	private float m_tempoChecarChao;
	[SerializeField] Transform m_ChecarChao;
	[SerializeField] LayerMask m_LayerChao;
	#endregion

	[Header("Status do jogador")]
	[SerializeField] private float m_velocidadeHorizontal;
	[SerializeField] private float m_alturaPulo;

	#region Constantes

	const float k_RaioSolo = .2f;

	#endregion

	void Start()
	{
		m_joystick = FindObjectOfType<Joystick>();
		m_jogador = GetComponent<Jogador>();
	}

	// Update is called once per frame
	void Update()
	{
		Andar();
	}
	private void FixedUpdate()
	{
		if (Time.time > m_tempoChecarChao)
			ChecarSePodePular();
	}

	public void Pular()
	{
		if (m_EstaNoChao)
		{
			m_jogador.rb2D.AddForce(new Vector2(0, m_alturaPulo), ForceMode2D.Impulse);
			m_jogador.animator.SetBool("Pular", true);
			m_tempoChecarChao = Time.time + 0.5f;
		}
	}
	private void ChecarSePodePular()
	{
		m_EstaNoChao = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_ChecarChao.position, k_RaioSolo, m_LayerChao);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Debug.Log(colliders[i].gameObject);
				m_EstaNoChao = true;
				m_jogador.animator.SetBool("Pular", false);
			}
		}
	}
	private void Andar()
	{
		#region Lógica para movimentação
		if (m_joystick.Horizontal >= .1f)
		{
			m_jogador.VelocidadeHorizontal(m_joystick.Horizontal * m_velocidadeHorizontal * Time.deltaTime);
			m_jogador.animator.SetFloat("Movendo", m_joystick.Horizontal);
		}
		else if (m_joystick.Horizontal <= -.1f)
		{
			m_jogador.VelocidadeHorizontal(m_joystick.Horizontal * m_velocidadeHorizontal * Time.deltaTime);
			m_jogador.animator.SetFloat("Movendo", Mathf.Abs(m_joystick.Horizontal));
		}
		else
		{
			m_jogador.VelocidadeHorizontal(0);
			m_jogador.animator.SetFloat("Movendo", m_joystick.Horizontal);
		}
		#endregion

		#region Lógica para virar

		if (m_joystick.Horizontal > 0 && !m_OlhandoParaDireita)
			Flip();
		else if (m_joystick.Horizontal < 0 && m_OlhandoParaDireita)
			Flip();
		#endregion
	}
	private void Flip()
	{
		m_OlhandoParaDireita = !m_OlhandoParaDireita;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(m_ChecarChao.position, k_RaioSolo);
	}
}
