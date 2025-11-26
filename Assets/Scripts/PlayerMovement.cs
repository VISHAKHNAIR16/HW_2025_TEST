namespace DoofusGame
{
    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;

        [Header("Jump Settings")]
        public float jumpForce = 2.2f;
        private bool jumpRequested = false;

        [Header("Ground Check Settings")]
        public float groundCheckRadius = 0.3f;
        public float groundCheckYOffset = 0.55f;
        public LayerMask groundLayer;

        private Rigidbody rb;
        private bool isGrounded = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            if (GameSettingsLoader.Settings != null && GameSettingsLoader.Settings.player_data != null)
            {
                moveSpeed = GameSettingsLoader.Settings.player_data.speed;
            }
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpRequested = true;
            }
        }

        void FixedUpdate()
        {
            if (GameOverManager.Instance != null && GameOverManager.Instance.IsGameOver())
            {
                rb.linearVelocity = Vector3.zero;
                jumpRequested = false;
                return;
            }


            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 moveDir = new Vector3(horizontal, 0f, vertical);


            if (moveDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }


            Vector3 velocity = rb.linearVelocity;
            velocity.x = moveDir.x * moveSpeed;
            velocity.z = moveDir.z * moveSpeed;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);


            Vector3 groundCheckPos = transform.position + Vector3.down * groundCheckYOffset;
            isGrounded = Physics.CheckSphere(groundCheckPos, groundCheckRadius, groundLayer);


            if (jumpRequested && isGrounded)
            {
                Jump();
            }
            jumpRequested = false;
        }

        private void Jump()
        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }


    }
}
