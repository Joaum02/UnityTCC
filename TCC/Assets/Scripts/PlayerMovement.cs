using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidade;

    [SerializeField] private float forcaPulo;

    [SerializeField] private float alturaPlayer;
    
    [SerializeField] private LayerMask chao;

    [SerializeField] private float friccao;
    
    [SerializeField] private bool noChao;

    [SerializeField] private Transform orientacao;

    [SerializeField] private float horizontal, vertical;

    [SerializeField] private Vector3 direcao;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject scanPrefab;

    [SerializeField] private GameObject passoPrefab;

    private GameObject passo;

    void Start()
    {
        velocidade = 25f;
        forcaPulo = 6f;
        alturaPlayer = 2f;
        friccao = 2.5f;

        passo = null;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = friccao;
    }

    void FixedUpdate()
    {
        Move();
        LimitarVelocidade();
        Pulo();
        Passos();

        if(Input.GetKey(KeyCode.Mouse0))
            Scanner();
    }

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Andar sempre na direção da camêra
        direcao = orientacao.forward * vertical + orientacao.right * horizontal;

        rb.AddForce(direcao.normalized * velocidade, ForceMode.Force);
    }

    bool NoChao()
    {
        noChao = Physics.Raycast(transform.position, Vector3.down, alturaPlayer * 0.5f + 0.2f, chao);
        return noChao;
    }

    void LimitarVelocidade()
    {
        Vector3 vel = direcao;

        if(vel.magnitude > velocidade)
        {
            Vector3 limiteVel = vel.normalized * velocidade;
            rb.velocity = new Vector3(limiteVel.x, 0, limiteVel.z); 
        }
    }

    void Pulo()
    {
        if(Input.GetKey(KeyCode.Space) && NoChao())
        {
            // Reseta a velocidade para pular sempre na mesma altura
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
        }
    }

    void Scanner()
    {
        Instantiate(scanPrefab, orientacao.position, Quaternion.identity);
    }

    void Passos()
    {
        if(rb.velocity != Vector3.zero && NoChao() && passo == null)
        {
            passo = Instantiate(passoPrefab, orientacao.position, Quaternion.identity);
        }
    }
}
