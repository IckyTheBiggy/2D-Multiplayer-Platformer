using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatItemScript : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _text;
    
    public void AssignInfo(string info)
    {
        _text.text = info;
    }
}
