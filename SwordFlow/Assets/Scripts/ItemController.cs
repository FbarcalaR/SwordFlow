using System;
using UnityEngine;
namespace SwordFlowScripts
{
    public class ItemController : MonoBehaviour
    {
        public Rigidbody2D shield;
        public float radius = 0.5f;
        public Camera cam;
        public bool isOnRightHand = false;
        public float rotationAngleOffset = 0f;
        public float positionAngleOffset = 0f;
        public float facingRightRotation = 180f;
        public Transform shieldCenter;
        public float offsetPositionCenterX = 0f;
        public float offsetPositionCenterY = 0f;
        public CharacterController2D playerController;

        private Collider2D Collider;
        private Vector2 mousePos;
        private bool facingRight = false;

        private void Start()
        {
            Collider = GetComponentInChildren<Collider2D>();
            if(!isOnRightHand)
                Collider.enabled = false;
        }
        void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1) && !isOnRightHand)
            {
                Collider.enabled = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && !isOnRightHand)
            {
                Collider.enabled = false;
            }
        }

        void FixedUpdate()
        {
            Vector2 shieldCenterPos = new Vector2(shieldCenter.position.x + GetOffsetPositionX(), shieldCenter.position.y + offsetPositionCenterY);
            shield.position = shieldCenterPos;
            shield.rotation = 0f;

            if ((Input.GetKey(KeyCode.Mouse0) && isOnRightHand) || (Input.GetKey(KeyCode.Mouse1) && !isOnRightHand))
            {
                shieldCenterPos = new Vector2(shieldCenter.position.x, shieldCenter.position.y);
                Vector2 playerToMouse = mousePos - shieldCenterPos;
                float flatAngleToMouse = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
                float module = playerToMouse.magnitude > radius ? radius : playerToMouse.magnitude;

                float angleShieldPos = flatAngleToMouse - positionAngleOffset * Mathf.Deg2Rad;
                Vector2 newShieldPos = new Vector2(-module * Mathf.Sin(angleShieldPos), module * Mathf.Cos(angleShieldPos));

                shield.position = newShieldPos + shieldCenterPos;
                shield.rotation = flatAngleToMouse * Mathf.Rad2Deg + rotationAngleOffset;
                if (!facingRight)
                {
                    shield.rotation += facingRightRotation;
                }

            }
            else
            {
                if (playerController.IsFacingRight() != facingRight) Flip();
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            shield.transform.Rotate(0f, 180f, 0f);
        }

        private float GetOffsetPositionX()
        {
            if (playerController.IsFacingRight())
            {
                return offsetPositionCenterX;
            }
            return -offsetPositionCenterX;
        }
    }
}