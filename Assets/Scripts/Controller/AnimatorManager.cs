
using System;
using UnityEngine;

namespace ErfanDeveloper
{
    public class AnimatorManager : MonoBehaviour
    {
        private Animator anims;
        private PlayerManager playerManager;
        private PlayerLookMotionManager playerLookMotion;
        private float snappedHorizontal;
        private float snappedVertical;

        private void Awake()
        {
            anims = GetComponent<Animator>();
            playerManager = GetComponent<PlayerManager>();
            playerLookMotion = GetComponent<PlayerLookMotionManager>();
        }

        public void PlayAnimationWhithOutEootMotion(string targetAnimation,bool isPreformingAction)
        {
            anims.SetBool("isPreformingAction",isPreformingAction);
            anims.SetBool("disabelRoolMotion",true);
            anims.applyRootMotion = false;
            anims.CrossFade(targetAnimation, 0.2f);
        }
        public void ChangeAnimatorValues(float horizontalMovement, float verticalMovement,bool isRuning)
        {
            #region ChangeSnapped

            if (horizontalMovement > 0)
            {
                snappedHorizontal = 1;
            }
            else if (horizontalMovement < 0)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }

            if (verticalMovement > 0)
            {
                snappedVertical = 1;
            }
            else if (verticalMovement < 0)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }

            if (isRuning && snappedVertical > 0) //we dont want to be able to backwards, or run whilst moving backward
            {
                snappedVertical = 2;
            }

            #endregion

            anims.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
            anims.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
            if(playerManager.disabelMotion)
                return;
            Vector3 animationDeltaPosiyion = anims.deltaPosition;
            animationDeltaPosiyion.y = 0;


            Vector3 velocity = animationDeltaPosiyion / Time.deltaTime;
            playerLookMotion.playerRigidbody.drag = 0;
            playerLookMotion.playerRigidbody.velocity = velocity;
            transform.rotation *= anims.deltaRotation;
            
        }
    }
}