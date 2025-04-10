using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{


     public  static bool InputSelect(Gamepad gamepad) {
        if (gamepad!=null)
        {
            return (gamepad.buttonSouth.isPressed);

        }
        else return (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space));
    }

    public static bool  InputCancel(Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.buttonEast.isPressed);

        }
        else return (Input.GetKeyUp(KeyCode.Backspace) || Input.GetKeyUp(KeyCode.Escape));
    }


    public static bool InputLeft(Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.leftStick.left.isPressed || gamepad.dpad.left.isPressed);

        }
        else return (Input.GetKeyUp(KeyCode.LeftArrow)||(Input.GetKeyUp(KeyCode.A)));
    }

    public static bool InputRight( Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.leftStick.right.isPressed|| gamepad.dpad.right.isPressed);

        }
        else return (Input.GetKeyUp(KeyCode.RightArrow) || (Input.GetKeyUp(KeyCode.D)));
    }


}
