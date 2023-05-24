
using UnityEngine;

namespace ErfanDeveloper
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Refrens Object")]
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private Camera cameraObject;
        [SerializeField] private GameObject player;
        [SerializeField] private float cameraSmoothTime = 0.2f;
        [Header("Rotation")]
        [SerializeField] private float maximumPivotAngle = 15;
        [SerializeField] private float minimumPivotAngle = -15;
        private Vector3 cameraRotation;
        private Quaternion targetRotation;
        private float lookAmountVertical;
        private float lookAmountHorizotal;
        
        private InputManager inputManager;
        
        private Vector3 cameraFollowVelocity = Vector3.zero;
        private Vector3 targetPosition;
        
        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
        }

        public void HandleAllCameraMovement()
        {
            FollowPlayer();
            RotateCamera();
        }

        private void FollowPlayer()
        {
            targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position,
                ref cameraFollowVelocity,cameraSmoothTime * Time.deltaTime);


            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            lookAmountVertical += inputManager.horizontalCameraInput;
            lookAmountHorizotal += inputManager.verticalCameraInput;
            lookAmountHorizotal = Mathf.Clamp(lookAmountHorizotal, minimumPivotAngle, maximumPivotAngle);

            cameraRotation = Vector3.zero;
            cameraRotation.y = lookAmountVertical;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
            transform.rotation = targetRotation;


            if (inputManager.quickTurn)
            {
                inputManager.quickTurn = false;
                lookAmountVertical = lookAmountVertical + 180;
                cameraRotation.y = cameraRotation.y + 180;
                transform.rotation = targetRotation;
                //in futul, add smooth turn
            }
            
            
            cameraRotation = Vector3.zero;
            cameraRotation.x = lookAmountHorizotal;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
            cameraPivot.localRotation = targetRotation;
        }
    }
}