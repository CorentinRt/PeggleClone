using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public delegate void Fallen();
    public static event Fallen OnFallen;

    bool _proxi;
    public bool activateProxi {  get => _proxi; set => _proxi = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "endTrigger")
        {
            OnFallen();
            Destroy(gameObject);
        }
    }
}
