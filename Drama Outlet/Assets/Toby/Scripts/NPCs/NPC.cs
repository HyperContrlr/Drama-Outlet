using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public partial class NPCAI
{
    [System.Serializable]
    public struct NPC
    {
        [Flags] public enum Preference {Mask = 1, Makeup = 2, Costume = 4, Prop = 8, MaskPlus = 16, MakeupPlus = 32, CostumePlus = 64, PropPlus = 128}
        public enum Personality {Window_Shopper, Big_Spender, Average_Shopper, Celebrity, Karen, Thief}
        public Personality personality;
        public Preference preference;
        public SpriteRenderer spriteRenderer;
        public Animator animator;
        public float amountToBuy;
        public float maxAmountToBuy;
        public float minAmountToBuy;
        public bool hasBoughtSomething;
        public float money;
        public float speed;
        public float speedMax;
        public float speedMin;
        public bool unhappy;
    }
}
