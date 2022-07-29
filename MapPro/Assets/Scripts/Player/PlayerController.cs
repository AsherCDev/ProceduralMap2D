using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Transform _vfx;
    
    private Vector2 _move;
    private float _angle;
    void Start()
    {
        _vfx = transform.GetChild(0);
        _rb = GetComponent<Rigidbody2D>();
        _angle = -2;
        EventManager.instance.UpdateChunks();
    }

    void Update()
    {
        // Inputs
        _move = new Vector2(Input.GetAxisRaw("H"), Input.GetAxisRaw("V"));
    }

    private void FixedUpdate()
    {
        Move(_move);
    }
    
    private void Move(Vector2 input)
    {
        if (Mathf.Abs(input.x) > 0.05 || Mathf.Abs(input.y) > 0.05)
        {
            // Calculates and executes x and y speeds from user inputs
            float angle = Mathf.Atan2(input.y, input.x);
            input = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            _rb.MovePosition(_rb.position + input * Constant.Player.moveSpeed * Time.fixedDeltaTime);
            
            // If the movement direction changes, start rotating toward the new direction
            if (angle != _angle)
            {
                StopAllCoroutines();
                StartCoroutine(Rotate(new Vector3(0, 0, angle * Mathf.Rad2Deg - 90), Constant.Player.rotationTime));
                _angle = angle;
            }
            Dynamic.Player.UpdatePosition(transform.position);
        }
        else
        {
            _rb.velocity = new Vector2(0, 0);
        }

    }
    
    IEnumerator Rotate(Vector3 byAngles, float inTime)
    {
        // Rotates the vfx sprite over time
        var fromAngle = _vfx.rotation;
        var toAngle = Quaternion.Euler(byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            _vfx.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
