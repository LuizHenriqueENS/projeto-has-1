using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorInimigos : MonoBehaviour
{
    
    [SerializeField] GameObject[] inimigos;
    [SerializeField] Transform[] locaisSurgimento;
    [SerializeField] float tempoRespawn = 3f;
    private float delaySurgimento = 10f;
    void Start()
    {
        InstanciarInimigo();
    }

    void Update()
    {
        if(Time.time >= (delaySurgimento)){
            InstanciarInimigo();

            delaySurgimento = Time.time + tempoRespawn;
        }
    }

    void InstanciarInimigo(){
         Instantiate(inimigos[Random.Range(0, inimigos.Length)], locaisSurgimento[Random.Range(0, locaisSurgimento.Length)]);
    }
}
