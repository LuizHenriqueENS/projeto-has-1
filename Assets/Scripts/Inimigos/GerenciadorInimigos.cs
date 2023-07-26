using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorInimigos : MonoBehaviour
{

    [SerializeField] GameObject[] inimigos;
    [SerializeField] Transform[] locaisSurgimento;
    [SerializeField] float tempoRespawn = 3f;
    private float delaySurgimento = 3f;
    void Start()
    {
        InstanciarInimigo();
    }

    void Update()
    {
        if (QuantidadeInimigos() <= 0)
        {
            InstanciarInimigo();
            delaySurgimento = Time.fixedTime + tempoRespawn;
        }
        if (Time.time >= delaySurgimento)
        {
            InstanciarInimigo();

            delaySurgimento = Time.time + tempoRespawn;
        }
    }

    void InstanciarInimigo()
    {
       // bool quantidade = QuantidadeInimigos() >= 3 ? true : false;
        if (QuantidadeInimigos() <= 3)
        {
            Instantiate(inimigos[Random.Range(0, inimigos.Length)], locaisSurgimento[Random.Range(0, locaisSurgimento.Length)]);
        }
    }

    private static int QuantidadeInimigos()
    {
        return FindObjectsOfType<Inimigo>().Length;
    }
}
