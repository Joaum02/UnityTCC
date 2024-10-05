using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerPassos : MonoBehaviour
{
    public float velocidade;
    public float tempo;

    void Start()
    {
        velocidade = 38f;
        tempo = 0.8f;
    }
    void Update()
    {
        transform.localScale += Vector3.one * velocidade * (Time.deltaTime * 0.5f);
        Destroy(gameObject, tempo);
    }
}
