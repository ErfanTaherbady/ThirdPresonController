
using UnityEngine;
using UnityEngine.Serialization;

namespace ErfanDeveloper
{
    public class InputManager : MonoBehaviour
    {
        private PlayerController playerController;
        private AnimatorManager animatorManager;
        private Animator anims;
        private PlayerManager playerManager;

        [Header("Player Movement")] [SerializeField]
        private float verticalMovementInput;

        [SerializeField] private float horizontalMovementInput;
        private Vector2 movementInput;

        [Header("Camera Rotation")] public float verticalCameraInput;
        public float horizontalCameraInput;
        private Vector2 cameraInput;

        [Header("Button Input")] public bool runInput;
        [FormerlySerializedAs("quichTurn")] public bool quickTurn;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            anims = GetComponent<Animator>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            if (playerController == null)
            {
                playerController = new PlayerController();

                playerController.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerController.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerController.PlayerMovement.Run.performed += i => runInput = true;
                playerController.PlayerMovement.Run.canceled += i => runInput = false;
                playerController.PlayerMovement.QuicTurn.performed += i => quickTurn = true;
            }

            playerController.Enable();
        }

        private void OnDisable()
        {
            playerController.Disable();
        }

        public void HandelAllInput()
        {
            HandelMovementInput();
            HandelCameraInput();
            HandelQuickTurn();
        }

        private void HandelMovementInput()
        {
            horizontalMovementInput = movementInput.x;
            verticalMovementInput = movementInput.y;
            animatorManager.ChangeAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);
        }

        private void HandelCameraInput()
        {
            horizontalCameraInput = cameraInput.x;
            verticalCameraInput = cameraInput.y;
        }

        public void HandelQuickTurn()
        {
            if (playerManager.isPreformingAction)
                return;
            if (quickTurn)
            {
                anims.SetBool("isPreformingQuickTurn",true);
                animatorManager.PlayAnimationWhithOutEootMotion("Quick Turn", true);
            }
        }
    }
}