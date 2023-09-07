using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu (fileName = "Creat Data", menuName = ("AI Bot Type")), ] 
    
    public class AIBotScriptable : ScriptableObject
    {
        [SerializeField] private float botSpeed;
        [SerializeField] private int kickForce;
        [SerializeField] private float gravity;

        public float BotSpeed
        {
            get { return botSpeed; }
            set{}
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
