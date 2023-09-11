using UnityEngine;
using TMPro;

public class StarWarsTextScroll : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float speed = 1;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        rig.velocity = new Vector2(0, speed);
    }

   
}
