using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public struct NPC
    {
        public enum Preference {Mask, Makeup, Costume, Prop, MaskPlus, MakeupPlus, CostumePlus, PropPlus}
        public Preference preference;
        public Sprite sprite;
        public Animator animator;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
