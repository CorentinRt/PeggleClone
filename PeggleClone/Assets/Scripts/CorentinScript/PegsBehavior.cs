using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegsBehavior : MonoBehaviour
{
    // Fields
    [Header("Gestion collision")]
    [SerializeField] private bool _hasBeenTouched;

    [SerializeField] private float _preDestructionTime;
    [SerializeField] private float _currentPreDestructionTime;

    [Header("Gestion Visual")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _spriteTouched;

    // Properties


    // Methods

    private void DestructPeggle()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _hasBeenTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balle") && !_hasBeenTouched)
        {
            _hasBeenTouched = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("stay");
        if(collision.gameObject.CompareTag("Balle") && _hasBeenTouched)
        {
            _currentPreDestructionTime += Time.deltaTime;
            
            if(_currentPreDestructionTime >= _preDestructionTime)
            {
                DestructPeggle();
            }
        }
    }

}
