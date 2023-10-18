using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public delegate void Fallen();
    public static event Fallen OnFallen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFallen();
        Destroy(gameObject);
    }
}
