using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[CreateAssetMenu(fileName = "New CutScens", menuName = "CutScens")]
public class Cutscenes : ScriptableObject
{
    public GameObject cutscenImage;
    public Image backGroundDescription;
    public string description;
}
