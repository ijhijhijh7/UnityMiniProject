using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAssist: MonoBehaviour
{
    [SerializeField] float fallMultiplier = 4f;
    [SerializeField] float lowJumpMultiplier = 7f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.C))
        {
            // C ≈∞∏¶ ∂√¿ª ∂ß ≥∑¿∫ ¡°«¡
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
