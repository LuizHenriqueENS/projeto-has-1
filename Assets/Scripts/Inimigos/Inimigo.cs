using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] Animator animatorInimigo;


    [Header("Status Inimigos")]
    [SerializeField] float vida;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevarDano(float dano)
    {
        vida -= dano;
        animatorInimigo.SetTrigger("Dano");
        if (vida <= 0)
        {
            animatorInimigo.SetBool("Morto", true);
            gameObject.layer = 8;
            Destroy(gameObject, 3f);
        }
    }
}
