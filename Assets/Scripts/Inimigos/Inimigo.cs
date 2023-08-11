using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Inimigo : MonoBehaviour
{

	[SerializeField] MoldeInimigo m_inimigo;
	[SerializeField] private AIPath aiPath;

	[Header("Configs")]
	public Animator animator;
	public Slider m_SliderBarraDeVida;
	public LayerMask m_Jogador;

	// CACHE
	private Rigidbody2D m_rb;
	private float m_EscalaX;
    
	void Start()
    {
		m_inimigo.m_VidaAtual = m_inimigo.m_VidaMaxima;
		m_SliderBarraDeVida.maxValue = m_inimigo.m_VidaMaxima;
		m_SliderBarraDeVida.value = m_inimigo.m_VidaMaxima;
		
		
		m_EscalaX = transform.localScale.x;
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
	{
		VirarEmDirecaoAoJogador();
	}

	private void VirarEmDirecaoAoJogador()
	{
		if (aiPath.desiredVelocity.x >= 0.01f)
		{
			Vector3 scale = transform.localScale;
			scale.x = m_EscalaX;
			transform.localScale = scale;
		}
		if (aiPath.desiredVelocity.x <= -0.01f)
		{
			Vector3 scale = transform.localScale;
			scale.x = -m_EscalaX;
			transform.localScale = scale;
		}
	}

	public void LevarDano(float dano, float efeitoEmpurrarDuranteDano)
	{
		m_inimigo.m_VidaAtual -= dano;
		m_SliderBarraDeVida.value -= dano;

		animator.SetTrigger("Dano");

		if (m_inimigo.m_VidaAtual <= 0)
		{
			Morrer();
		}
	}

	private void Morrer()
    {
		// _combate.AdicionarPontosDeExperiencia(_experiencia);
		animator.SetBool("Morto", true);
		m_rb.AddForce(new Vector2(0, m_inimigo.m_PularAoMorrer));
		m_SliderBarraDeVida.gameObject.SetActive(false);
		gameObject.layer = 8;
		Destroy(transform.parent.gameObject, 3f);
	}


}
