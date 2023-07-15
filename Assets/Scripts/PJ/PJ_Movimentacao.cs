using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Movimentacao : MonoBehaviour
{

    float movimentacaoHorizontal = 0f;
    [SerializeField] float velocidadePJ = 30f;
    Rigidbody2D rb;
    [SerializeField] Joystick joystick;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = FindObjectOfType<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(joystick.Horizontal >= .2f){
            movimentacaoHorizontal = velocidadePJ;
        } else if(joystick.Horizontal <= -.2f){
            movimentacaoHorizontal = -velocidadePJ;
        } else{
            movimentacaoHorizontal = 0f;
        }

        rb.velocity = new Vector2(movimentacaoHorizontal * Time.deltaTime, rb.velocity.y);
    }
}
