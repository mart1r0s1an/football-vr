using UnityEngine;

[CreateAssetMenu (fileName = "Creat Data", menuName = ("Player Type")), ] 
public class PlayerScriptable : ScriptableObject
{
    [SerializeField] private int playerSpeed;
    [SerializeField] private int kickForce;
    [SerializeField] private int jumpForce;
    [SerializeField] private float gravity;

    public int PlayerSpeed 
    {
        get { return playerSpeed; }
    }

    public int KickForce 
    {
        get { return kickForce; }
    }

    public int JumpForce 
    {
        get { return jumpForce; }
    }

    public float Gravity 
    {
        get { return gravity; }
    }
}