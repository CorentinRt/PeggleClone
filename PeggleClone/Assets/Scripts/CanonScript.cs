using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanonScript : MonoBehaviour
{
    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Transform _ballSpawningPoint;
    [SerializeField] InputActionReference _inputLaunch;
    bool _canShoot = true;
    Vector3 _currentDirection;

    [Header("Force")]
    [SerializeField] [Range(0,100f)]  float _horizontalForce;
    [SerializeField] [Range(0,100f)]  float _verticalForce;

    private void OnEnable()
    {
        BallScript.OnFallen += ResetCanShoot;
    }

    private void Update()
    {
        //projection de la balle
        bool launchInput = _inputLaunch.action.WasPressedThisFrame();
        if (launchInput  && _canShoot)
        {
            _canShoot = false;

            GameObject ball = Instantiate(_ballPrefab, _ballSpawningPoint.position, Quaternion.identity);
            Rigidbody2D ballRB2D = ball.GetComponent<Rigidbody2D>();
            ballRB2D.velocity = new Vector2(_currentDirection.x * _horizontalForce, _currentDirection.y * _verticalForce);
        }

        print(_currentDirection);
    }
    void FixedUpdate()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Regarde curseur
        _currentDirection = cursorPosition - transform.position;
        _currentDirection.Normalize();
        float angle = Mathf.Atan2(_currentDirection.y, _currentDirection.x) * Mathf.Rad2Deg;

        Quaternion playerRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 270f));
        transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, 1f);
    }

    void ResetCanShoot() => _canShoot = true;
}
