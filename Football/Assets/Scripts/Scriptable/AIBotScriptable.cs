using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu (fileName = "Creat Data", menuName = ("AI Bot Type")), ] 
    
    public class AIBotScriptable : ScriptableObject
    {
        [SerializeField] private int botSpeed;
        [SerializeField] private int kickForce;
        [SerializeField] private float gravity;

        public int BotSpeed
        {
            get { return botSpeed; }
        }

        public int KickForce 
        {
            get { return kickForce; }
        }

        public float Gravity 
        {
            get { return gravity; }
        }
    }
}
