using UnityEngine;


[CreateAssetMenu(menuName = "Manager/Game Settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string gameVersion = "0.0.0";
    public string GameVersion
    {
        get { return gameVersion; }
    }

    
    [SerializeField] private string nickName = "Escape";
    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999); 
            
            return nickName + value.ToString();
        }
    }
}
