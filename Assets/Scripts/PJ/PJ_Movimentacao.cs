using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Movimentacao : MonoBehaviour
{

    float movimentacaoHorizontal = 0f;
    [SerializeField] float velocidadePJ = 30f;
    Rigidbody2D rb;
    [SerializeField] Joystick joystick;
    bool olhandoParaDireita = true;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = FindObjectOfType<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimentar
        if (joystick.Horizontal >= .1f)
            movimentacaoHorizontal = velocidadePJ;
        else if (joystick.Horizontal <= -.1f)
            movimentacaoHorizontal = -velocidadePJ;
        else
            movimentacaoHorizontal = 0f;

        // FLIP
        if (movimentacaoHorizontal > 0 && !olhandoParaDireita)
            Flip();
        else if (movimentacaoHorizontal < 0 && olhandoParaDireita)
            Flip();

        rb.velocity = new Vector2(movimentacaoHorizontal * Time.deltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        olhandoParaDireita = !olhandoParaDireita;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

    public void Pular()
    {
        rb.AddForce(new Vector2(0, 200f));
    }
}
