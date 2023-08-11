using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadoSurgimentoInimigos : MonoBehaviour
{

	[field: SerializeField] Vector3[] pontosSurgimento;

	[SerializeField] GameObject[] inimigos;
	[SerializeField] private float _tempoEntreSurgimentos;
	private float _proximoSurgimento;


	private void Update()
	{
		if (QuantidadeInimigos() <= 0)
		{
			InstanciarInimigo();
			_proximoSurgimento = Time.fixedTime + _tempoEntreSurgimentos;
		}
		if (Time.time >= _proximoSurgimento)
		{
			InstanciarInimigo();

			_proximoSurgimento = Time.time + _tempoEntreSurgimentos;
		}
	}

	void InstanciarInimigo()
	{
		// bool quantidade = QuantidadeInimigos() >= 3 ? true : false;
		if (QuantidadeInimigos() <= 3)
		{
			Instantiate(inimigos[Random.Range(0, inimigos.Length)], pontosSurgimento[Random.Range(0, pontosSurgimento.Length)], Quaternion.identity);
		}
	}

	private static int QuantidadeInimigos()
	{
		return FindObjectsOfType<Inimigo>().Length;
	}
	private void OnDrawGizmos()
	{
		foreach (var item in pontosSurgimento)
		{

			Gizmos.DrawSphere(item, .4f);
		}
	}
}
