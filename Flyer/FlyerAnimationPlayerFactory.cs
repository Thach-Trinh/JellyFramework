using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JellyFramework.FlyerSystem
{
    public class FlyerAnimationPlayerFactory : MonoBehaviour
    {
        [SerializeField] private FlyerAnimationPlayer[] players;
        public FlyerAnimationPlayer GetPlayer(FlyerAnimationStyle style) => Array.Find(players, p => p.Style == style);
    }
}

