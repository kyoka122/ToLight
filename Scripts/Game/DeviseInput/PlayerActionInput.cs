using UnityEngine.InputSystem;
    
namespace Game.DeviseInput
{
    public static class PlayerActionInput
    {
        public static bool onActionButton { get; private set; }=false;
        public static bool activeLight { get; private set; }=true;
        public static float lateralMovement { get; private set; } = 0;
        public static float depthMovement { get; private set; } = 0;

        public class PlayerActionSettings
        {
            private MyInputActionMap _myInputActionMap;

            public PlayerActionSettings()
            {
                _myInputActionMap = new MyInputActionMap();
                _myInputActionMap.Action.LateralMovement.started += OnMoveLateral;
                _myInputActionMap.Action.LateralMovement.performed += OnMoveLateral;
                _myInputActionMap.Action.LateralMovement.canceled += OffMoveLateral;

                _myInputActionMap.Action.DepthMovement.started += OnMoveDepth;
                _myInputActionMap.Action.DepthMovement.performed += OnMoveDepth;
                _myInputActionMap.Action.DepthMovement.canceled += OffMoveDepth;
            
                //TODO: 押してる間点灯に変更
                //_myInputActionMap.Action.ActionButton.started += SwitchActionButton;
                _myInputActionMap.Action.ActionButton.started += OnActionButton;
                _myInputActionMap.Action.ActionButton.canceled += OffActionButton;

                _myInputActionMap.Action.LightSwitch.started += SwitchLightActive;
            
                _myInputActionMap.Enable();
            }

            private void OnMoveLateral(InputAction.CallbackContext context)
            {
                lateralMovement = context.ReadValue<float>();
            }
        
            private void OffMoveLateral(InputAction.CallbackContext context)
            {
                lateralMovement = 0;
            }
        
            private void OnMoveDepth(InputAction.CallbackContext context)
            {
                depthMovement = context.ReadValue<float>();
            }
        
            private void OffMoveDepth(InputAction.CallbackContext context)
            {
                depthMovement = 0;
            }
        
            private void OnActionButton(InputAction.CallbackContext context)
            {
                onActionButton = true;
            }
            
            private void OffActionButton(InputAction.CallbackContext context)
            {
                onActionButton = false;
            }
            
            private void SwitchActionButton(InputAction.CallbackContext context)
            {
                onActionButton = !onActionButton;
            }
            
            private void SwitchLightActive(InputAction.CallbackContext context)
            {
                activeLight = !activeLight;
            }
        }
        
    }
}