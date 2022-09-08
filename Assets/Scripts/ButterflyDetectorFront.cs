using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyDetectorFront : MonoBehaviour
{
    public bool IsTouchingGround { get; private set; }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Ground")
        {
            IsTouchingGround = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTouchingGround = false;
    }
}
