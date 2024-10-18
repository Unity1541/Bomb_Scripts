using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableUI/HP_Parameters")]
public class UI_Parameters : ScriptableObject
{
   public float HP=1.0f;
   public float MP=1.0f;
   public int score;
   public float CD_Time;
}
