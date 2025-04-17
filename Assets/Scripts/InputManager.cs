using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{


     public  static bool InputSelect(Gamepad gamepad) {
        if (gamepad!=null)
        {
            return (gamepad.buttonSouth.isPressed);

        }
        else return (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space));
    }

    public static bool  InputCancel(Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.buttonEast.isPressed);

        }
        else return (Input.GetKeyUp(KeyCode.Backspace) || Input.GetKeyUp(KeyCode.Escape) || Input.GetKey(KeyCode.W));
    }


    public static bool InputLeft(Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.leftStick.left.isPressed || gamepad.dpad.left.isPressed);

        }
        else return (Input.GetKey(KeyCode.LeftArrow)||(Input.GetKey(KeyCode.A)));
    }

    public static bool InputRight( Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.leftStick.right.isPressed|| gamepad.dpad.right.isPressed);

        }
        else return (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)));
    }
     public static bool InputRevers( Gamepad gamepad) {
        if (gamepad != null)
        {
            return (gamepad.buttonWest.isPressed);

        }
        else return (Input.GetKey(KeyCode.C));
    }


}
