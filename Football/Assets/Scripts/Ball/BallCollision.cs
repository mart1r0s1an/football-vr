using System;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
   private Dictionary<string, Action<Collider>> _collisionDictionary = new Dictionary<string, Action<Collider>>();
   
   private void Start()
   {
      _collisionDictionary.Add("OurGoal", OnTriggerGoal);
      _collisionDictionary.Add("OpponentGoal", OnTriggerGoalOpponent);
   }

   private void OnTriggerGoal(Collider collider)
   {
      Debug.Log("it was goal");
   }

   private void OnTriggerGoalOpponent(Collider collider)
   {
      
   }

   private void OnTriggerStay(Collider other)
   {
      Action<Collider> action = _collisionDictionary[other.gameObject.tag];

      if (action != null)
      {
         action(other);
      }
   }
}
