using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Joystick controlLeftRight;
    public float rateGas;
    public bool isWheelieInput;
    bool isGas;

    public void OnClickGas(bool _isActive)
    {
        if (_isActive)
        {
            rateGas = 1;
        }
        else
        {
            rateGas = 0;
        }
        isGas = _isActive;

    }
    public void OnClickBrake(bool _isActive)
    {
        if (_isActive)
        {
            rateGas = -.8f;
        }
        else
        {
            rateGas = 0;
        }
    }

    public void OnClickAirborneUp(bool _isActive)
    {
        if (_isActive)
        {
            isWheelieInput = true;
            rateGas = 1;
        }
        else
        {
            isWheelieInput = false;
            rateGas = 0;
        }
        
    }

    public void OnClickAirborneDown()
    {

    }

}
