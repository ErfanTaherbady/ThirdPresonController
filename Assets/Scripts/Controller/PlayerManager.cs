
using UnityEngine;

namespace ErfanDeveloper
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerCamera playerCamera;
        private InputManager inputManager;
        private Animator anims;
        private PlayerLookMotionManager playerLookMotionManager;

        [Header("Player Flags")] public bool disabelMotion;
        public bool isPreformingAction;
        public bool isPreformingQuickTurn;
        private void Awake()
        {
            playerCamera = FindObjectOfType<PlayerCamera>();
            inputManager = GetComponent<InputManager>();
            playerLookMotionManager = GetComponent<PlayerLookMotionManager>();
            anims = GetComponent<Animator>();
        }

        private void Update()
        {
            disabelMotion = anims.GetBool("disabelRoolMotion");
            inputManager.HandelAllInput();
            isPreformingAction = anims.GetBool("isPreformingAction");
            isPreformingQuickTurn = anims.GetBool("isPreformingQuickTurn");
        }

        private void FixedUpdate()
        {
            playerLookMotionManager.HandleAllLocomotion();
        }

        private void LateUpdate()
        {
            playerCamera.HandleAllCameraMovement();
        }
    }
}