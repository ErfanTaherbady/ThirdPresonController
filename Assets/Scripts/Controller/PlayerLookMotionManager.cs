
using UnityEngine;
namespace ErfanDeveloper
{
    public class PlayerLookMotionManager : MonoBehaviour
    {
        [HideInInspector] public Rigidbody playerRigidbody;
        private InputManager inputManager;
        private PlayerManager playerManager;

        [Header("Camera Transform")] [SerializeField]
        private Transform playerCamera;

        [Header("Movement Speed")] [SerializeField]
        private float rotationSpeed = 3.5f;

        [SerializeField] private float quickturnSpeed = 8;

        [Header("Rotation Varaibles")] private Quaternion targetRotation; //The Place We Want To Rotate
        private Quaternion playerRotation; //The Place We Are Now, Constantly changing

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerManager = GetComponent<PlayerManager>();
        }

        public void HandleAllLocomotion()
        {
            HandelRotation();
            //HandelFalling();
        }

        private void HandelRotation()
        {
            targetRotation = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (inputManager.verticalCameraInput != 0 || inputManager.horizontalCameraInput != 0)
            {
                transform.rotation = playerRotation;
            }

            if (playerManager.isPreformingQuickTurn)
            {
                playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, quickturnSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }
        }
    }
}