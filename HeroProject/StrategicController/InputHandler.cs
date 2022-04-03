using UnityEngine;

namespace StrategicManagement
{
    public static class InputHandler
    {    
        #region Mouse
        public static bool IsLeftMouseButtonDown()
        {
            return Input.GetButtonDown("LeftMouseButton");
        }

        public static bool IsLeftMouseButtonUp()
        {
            return Input.GetButtonUp("LeftMouseButton");
        }

        public static bool IsRightMouseButtonDown()
        {
            return Input.GetButtonDown("RightMouseButton");
        }
        #endregion

        #region Buttons
        public static bool IsHoldButtonDown()
        {
            return Input.GetButtonDown("Hold");
        }

        public static bool IsShiftButtonDown()
        {
            return Input.GetButtonDown("Shift");
        }

        public static bool IsShiftButtonUp()
        {
            return Input.GetButtonUp("Shift");
        }

        public static bool IsStopButton()
        {
            return Input.GetButtonDown("Stop");
        }

        public static bool IsAttackButtonDown()
        {
            return Input.GetButtonDown("Attack");
        }
        #endregion
    }
}