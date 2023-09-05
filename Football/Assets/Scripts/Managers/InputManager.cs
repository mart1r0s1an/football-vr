/*
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputAxisMap _axisMap;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple InputManager instances detected. Keeping the first one.");
            Destroy(gameObject);
        }
        
        _axisMap = new InputAxisMap();

        // Register all of the input axes that we need to use in our game.
        _axisMap.AddAxis("Horizontal", KeyCode.W, KeyCode.S);
        _axisMap.AddAxis("Vertical", KeyCode.A, KeyCode.D);
    }

    public void Update()
    {
        // Update the state of all of the input axes.
        _axisMap.Update();

        // Use the input axes to control the game.
        float horizontalInput = _axisMap.GetAxis("Horizontal");
        float verticalInput = _axisMap.GetAxis("Vertical");

        // Move the player based on the horizontal and vertical input.
        player.transform.position += new Vector3(horizontalInput, 0, verticalInput);
    }
}
*/
