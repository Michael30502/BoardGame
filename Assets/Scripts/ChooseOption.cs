using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChooseOption : MonoBehaviour
{
     public static int Choose(int currentOption, int n,Gamepad gamepad)
    {
        

            if (InputManager.InputLeft(gamepad))
            {
                currentOption--;
            Input.ResetInputAxes();


        }
        if (InputManager.InputRight(gamepad))
            {
            currentOption++;
            Input.ResetInputAxes();

        }

        if (currentOption > n-1)
            {
            currentOption = 0;
            }
            else if (currentOption < 0)
            {
            currentOption = n-1;
            }

        return currentOption;

        }

    public static bool Select( Gamepad gamepad)
    {

        if (InputManager.InputSelect(gamepad))
        {
            Input.ResetInputAxes();

            return true;
        }
        return false;


    }
    public static bool Cancel(Gamepad gamepad)
    {
        if (InputManager.InputCancel(gamepad))
        {
            Input.ResetInputAxes();

            return true;
        }
        return false;

    }

}
   



