using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Scriptable
{
    [CreateAssetMenu (fileName = "Creat Data", menuName = ("AI Bot Type")), ] 
    
    public class AIBotScriptable : ScriptableObject
    {
        [SerializeField] private float botSpeed;
        [SerializeField] private float patrollingRunSpeed;
        [SerializeField] private int kickForce;
        [SerializeField] private float gravity;

        public float BotSpeed
        {
            get { return botSpeed; }
            set{}
        }

        public float PatrollingRunSpeed {
            get
            {
                return patrollingRunSpeed;
            }
            set
            {
                
            } 
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
