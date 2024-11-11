using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
   public static WayPointManager instance;
   public Waypoints waypoints;

   void Awake()
   {
      instance = this;
   }
}
