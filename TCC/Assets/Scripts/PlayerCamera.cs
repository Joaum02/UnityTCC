using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float senseX, senseY;
    public Transform orientacao;

    public float rotacaoX, rotacaoY;
    void Start()
    {
        senseX = 400f;
        senseY = 400f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Input do mouse vezes a sensibilidade
        float mouseX = Input.GetAxisRaw("Mouse X") * senseX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * senseY * Time.deltaTime;

        rotacaoX += mouseX;

        rotacaoY -= mouseY;

        // Limitar a rotação na vertical
        rotacaoY = Mathf.Clamp(rotacaoY, -90f, 90f);

        // Rotação da camêra e do player
        transform.rotation = Quaternion.Euler(rotacaoY, rotacaoX, 0);
        orientacao.rotation = Quaternion.Euler(0, rotacaoX, 0);
    }
}
