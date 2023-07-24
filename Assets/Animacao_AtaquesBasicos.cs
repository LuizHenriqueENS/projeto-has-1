using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacao_AtaquesBasicos : StateMachineBehaviour
{
    PJ_Combate combate;
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combate = FindObjectOfType<PJ_Combate>();
        combate.DefinirPodeAtacar();
    }
}
