using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Movimentacao : MonoBehaviour
{

    private float _movimentacaoHorizontal = 0f;
    [SerializeField] float velocidadePJ = 30f;
    [SerializeField] float alturaPulo = 200f;
    private bool podePular = false;


    [SerializeField] Transform checarChao;
    [SerializeField] LayerMask layerChao;
    [SerializeField] Joystick joystick;
    [SerializeField] Animator animator;
    private Rigidbody2D _rb;
    private bool _olhandoParaDireita = true;
    bool chao;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        _rb = FindObjectOfType<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimentar
        MoverHorizontalmente();
    }

    private void MoverHorizontalmente()
    {
        if (joystick.Horizontal >= .1f)
        {
            _movimentacaoHorizontal = velocidadePJ;
            animator.SetFloat("Movendo", Mathf.Abs(_movimentacaoHorizontal));
        }

        else if (joystick.Horizontal <= -.1f)
        {
            _movimentacaoHorizontal = -velocidadePJ;
            animator.SetFloat("Movendo", Mathf.Abs(_movimentacaoHorizontal));
        }
        else
        {
            _movimentacaoHorizontal = 0f;
            animator.SetFloat("Movendo", Mathf.Abs(_movimentacaoHorizontal));

        }

        // FLIP
        if (_movimentacaoHorizontal > 0 && !_olhandoParaDireita)
            Flip();
        else if (_movimentacaoHorizontal < 0 && _olhandoParaDireita)
            Flip();

        _rb.velocity = new Vector2(_movimentacaoHorizontal * Time.deltaTime, _rb.velocity.y);
    }

    private void Flip()
    {
        _olhandoParaDireita = !_olhandoParaDireita;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

    public void Pular()
    {
        if (podePular)
        {
            animator.SetBool("Pular", true);
            _rb.AddForce(new Vector2(0, alturaPulo));
            podePular = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            podePular = true;
            animator.SetBool("Pular", false);
        }
    }
}
