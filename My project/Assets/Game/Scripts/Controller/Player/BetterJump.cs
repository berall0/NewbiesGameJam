using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float FallMultiply;
    public float lowJumpMultiply;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (rb.velocity.y < 0.1f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiply - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0.1f && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiply - 1) * Time.deltaTime;
        }
    }
 }

