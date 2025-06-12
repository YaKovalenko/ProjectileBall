using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Services.InputService
{
    public class InputService : IInputService
    {
        private InputControls _controls = new();

        private bool _pointerPressed;
        private bool _pointerHeld;
        private bool _pointerReleased;

        public InputService()
        {
            _controls.Enable();
            
            _controls.Gameplay.Shoot.started += OnPointerStarted;
            _controls.Gameplay.Shoot.canceled += OnPointerCanceled;
        }

        private void OnPointerStarted(InputAction.CallbackContext ctx)
        {
            _pointerPressed = true;
            _pointerHeld = true;
        }

        private void OnPointerCanceled(InputAction.CallbackContext ctx)
        {
            _pointerReleased = true;
            _pointerHeld = false;
        }

        public void LateUpdate()
        {
            _pointerPressed = false;
            _pointerReleased = false;
        }

        public bool IsPointerPressed() => _pointerPressed;
        public bool IsPointerHeld() => _pointerHeld; 
        public bool IsPointerReleased() => _pointerReleased; 

        public Vector2 GetPointerPosition()
        {
            return Mouse.current != null
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current?.primaryTouch.position.ReadValue() ?? Vector2.zero;
        }

        public bool TryGetClickedObject(out GameObject obj)
        {
            obj = null;
            if (!IsPointerPressed())
                return false;

            Ray ray = Camera.main.ScreenPointToRay(GetPointerPosition());
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                obj = hit.collider.gameObject;
                return true;
            }
            return false;
        }
    }
}