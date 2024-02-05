using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class ItemSO : ScriptableObject
{
    public enum Grade
    {
        G1,
        G2,
        G3,
        G4,
        G5
    }
    
    public string Name;
    public Sprite ItemSprite;
    public Grade ItemGrade;
}
