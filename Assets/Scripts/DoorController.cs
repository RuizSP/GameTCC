using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Platform;

    // Novo método virtual para ser substituído na classe derivada
    protected virtual void OnWallDeactivated()
    {
        // Adicione aqui qualquer comportamento específico necessário quando a parede for desativada.
    }

    void Start()
    {
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 14)
        {
            Wall.SetActive(false);
            OnWallDeactivated(); // Chamamos o método virtual quando a parede for desativada.
            if (Wall.gameObject.name == "DoorUp" && Platform != null)
            {
                Platform.SetActive(true);
            }
            other.gameObject.SetActive(false);

        }
        
    }
}
