using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
	public Rigidbody2D rb2D;
	public Animator animator;

	void Start()
	{
		// Definir FPS para 60
		Application.targetFrameRate = 60;

		rb2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	public void VelocidadeHorizontal(float velocidade)
	{
		rb2D.velocity = new Vector2(velocidade, rb2D.velocity.y);
	}
}
