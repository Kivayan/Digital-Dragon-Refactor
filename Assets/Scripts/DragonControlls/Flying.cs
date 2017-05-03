using UnityEngine;

namespace movementEngine
{
    [RequireComponent(typeof(Distance))]
    public class Flying : MonoBehaviour, IMovement
    {
        private float currentSpeed;
        public float speed = 20;
        public float shiftSpeed = 40f;
        public float riseSpeed = 8.0F;
        public float descentSpeed = -8.0F;
        private float gravity = 0;
        private float endOfStaminaGravity = 60f;

        private bool movementBlocked = false;

        private Vector3 moveDirection = Vector3.zero;

        [Range(20, 150)] public float mouseYSpeed = 50;
        [Range(20, 150)] public float mouseXSpeed = 50;

        private float YRotate = 0;
        private float XRotate = 0;

        private StaminaManager staminaManager;
        public float sprintStaminaCostPerSec;

        void OnEnable()
        {
            staminaManager = GetComponent<StaminaManager>();
        }

        void IMovement.Move()
        {
            if(!movementBlocked)
            {
                Rotate();
            }

            Move();
            DebugInfo();
        }

        private void Move()
        {
            CharacterController controller = GetComponent<CharacterController>();
            if(!movementBlocked)
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;
            moveDirection.y -= gravity;//* Time.deltaTime;

            RiseAndDescend();
            Accelerate();

            controller.Move(moveDirection * Time.deltaTime);
        }

        private void Rotate()
        {
            XRotate += -(Input.GetAxis("Mouse Y") * mouseYSpeed * Time.deltaTime);
            YRotate += Input.GetAxis("Mouse X") * mouseXSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(new Vector3(XRotate, YRotate, 0));
        }

        private void RiseAndDescend()
        {
            if (Input.GetKey(KeyCode.Space))
                moveDirection.y = riseSpeed;
            if (Input.GetKey(KeyCode.LeftControl))
                moveDirection.y = descentSpeed;
        }

        private void Accelerate()
        {
            if (Input.GetKey(KeyCode.LeftShift) && staminaManager.stamina.ContinousSubstract(sprintStaminaCostPerSec))
                currentSpeed = shiftSpeed;
            else
                currentSpeed = speed;
        }

        private void DebugInfo()
        {
            DebugPanel.Log("FlySpeed", "FlightParameters", currentSpeed);
            DebugPanel.Log("gravity", "FlightParameters", gravity);
        }

        void IMovement.EnableGravity()
        {
            gravity = endOfStaminaGravity;
        }

        public void DisableGravity()
        {
            gravity = 0f;
        }

        void IMovement.SetCurrentRotation(Vector3 startingRotation)
        {
            XRotate = startingRotation.x;
            YRotate = startingRotation.y;
            movementBlocked = false;
        }

        Vector3 IMovement.GetCurrentRotation()
        {
            return new Vector3(XRotate, YRotate, transform.rotation.z);
        }

        public void BlockMovement()
        {
            movementBlocked = true;
        }
    }
}