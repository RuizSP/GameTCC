using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDoorController : DoorController
{
    [SerializeField] private GameObject door;

    // Sobrescrevendo o método OnWallDeactivated para ativar a porta.
    protected override void OnWallDeactivated()
    {
        base.OnWallDeactivated(); // Chamamos o método da classe base para manter o comportamento original.
        if (door != null)
        {
            door.SetActive(true);
        }
    }
}
