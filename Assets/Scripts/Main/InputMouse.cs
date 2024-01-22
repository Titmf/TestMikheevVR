using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class InputMouse : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;

    private const string XAxisName = "Mouse X";
    private const string YAxisName = "Mouse Y";
    private void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        freeLookCamera.m_XAxis.m_InputAxisName = "";
        freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = Input.GetAxis(XAxisName);
            freeLookCamera.m_YAxis.m_InputAxisValue = Input.GetAxis(YAxisName);
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }
    }
}