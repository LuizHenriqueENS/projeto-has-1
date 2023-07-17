using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesTela : MonoBehaviour
{
    
    PJ_Movimentacao jogador;

   private void Start() {
        jogador = FindObjectOfType<PJ_Movimentacao>();
   }
   

   public void Pular(){
        jogador.Pular();
   }
}
