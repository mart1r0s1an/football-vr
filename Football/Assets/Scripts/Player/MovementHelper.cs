using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementHelper : MonoBehaviour
{
    private XRRig _xROrigin;
    private CharacterController _characterController;
    private CharacterControllerDriver _driver;

    private void Start()
    {
        _xROrigin = GetComponent<XRRig>();
        _characterController = GetComponent<CharacterController>();
        _driver = GetComponent<CharacterControllerDriver>();
    }

    private void Update()
    {
        UpdateCharacterController();
    }

    protected virtual void UpdateCharacterController()
    {
        if (_xROrigin == null ||_characterController == null)
            return;

        var height = Mathf.Clamp(_xROrigin.CameraInOriginSpaceHeight, _driver.minHeight, _driver.maxHeight);

        Vector3 center = _xROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + _characterController.skinWidth;

        _characterController.height = height;
        _characterController.center = center;
    }
}
