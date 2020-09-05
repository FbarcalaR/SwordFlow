using System;
using UnityEngine;
namespace SwordFlowScripts
{
    public class MeleeItem : MonoBehaviour
    {
        public float radius = 0.5f;
        public Camera cam;
        public CharacterController2D playerController;
        public string inputButtonName = "Mouse 0";
        public float angleOffsetOrientation = 0f;

        private Collider2D Collider;
        private Vector2 mousePos;
        private Vector3 initialPosition;
        private Vector3 initialEulerAngles;

        private void Start()
        {
            initialPosition = new Vector3(
                transform.localPosition.x, 
                transform.localPosition.y, 
                transform.localPosition.z
            );
            initialEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z
            );
            Collider = GetComponentInChildren<Collider2D>();
            if (Collider != null) Collider.enabled = false;
        }
        void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetButtonDown(inputButtonName))
            {
                if (Collider != null) Collider.enabled = true;
            }
            else if (Input.GetButtonUp(inputButtonName))
            {
                if (Collider != null) Collider.enabled = false;
            }
        }

        void FixedUpdate()
        {
            if (Input.GetButton(inputButtonName))
            {
                Vector2 newPos = new Vector2(mousePos.x, mousePos.y) - (Vector2)playerController.transform.position;
                newPos.Normalize();

                var xScale = playerController.IsFacingRight() ? 1 : -1;
                newPos.Scale(new Vector2(xScale * radius, radius));
                transform.localPosition = new Vector2(newPos.x, newPos.y);

                float newAngle = Mathf.Atan2(newPos.y, newPos.x);
                transform.localEulerAngles = new Vector3(0f, 0f, newAngle * Mathf.Rad2Deg + angleOffsetOrientation);
                    
            }
            else
            {
                transform.localEulerAngles = initialEulerAngles;
                transform.localPosition = initialPosition;
            }
        }
    }
}