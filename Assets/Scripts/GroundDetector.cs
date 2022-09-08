using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GroundDetector : MonoBehaviour
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
}