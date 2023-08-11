using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorCombate : MonoBehaviour
{
	ComboLeve ComboLeve;

    void Start()
    {
        ComboLeve = GetComponent<ComboLeve>();
    }

	public void AtaqueLeve()
	{
		ComboLeve.IniciarCombo();
	}

}
