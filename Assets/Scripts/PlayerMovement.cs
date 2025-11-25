namespace DoofusGame 
{
    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            
            Vector3 velocity = rb.linearVelocity;
            velocity.x = horizontal * moveSpeed;
            velocity.z = vertical * moveSpeed;  
            
            
            rb.linearVelocity = velocity;
        }
    }
}
