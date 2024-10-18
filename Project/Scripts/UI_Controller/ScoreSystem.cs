using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
  public UI_Parameters []ui_parameters;
  //public int player_score,enemy_score=0;
  public TextMeshProUGUI  player_textMeshPro;//要用GUI才可以輸入文字
  public TextMeshProUGUI enemy_textMeshPro;
  public EnemyStatus enemyStatus;
  public PlayerStatus playerStatus;
  public Slider []slider;

  private void Start()
  {
      enemyStatus = GameObject.Find("Doreamon").GetComponent<EnemyStatus>();
      playerStatus = GameObject.Find("Player").GetComponentInChildren<PlayerStatus>();
      ui_parameters[0].HP=1.0f;
      ui_parameters[1].HP=1.0f;
      ui_parameters[0].score=0;
      ui_parameters[1].score=0;
  } 
  public void calculate(int score,float HP)//觸發後要執行的方法，可以在Inspector中選擇
  {

        if (playerStatus.isHit==true)
        {
           ui_parameters[0].score+=score;
           enemy_textMeshPro.text = "Score\n" + ui_parameters[0].score.ToString();//輸入特殊符號\n表示換行
           ui_parameters[1].HP-=HP;
           slider[1].value = ui_parameters[1].HP;
        }
        if (enemyStatus.isHit==true)
        {
           ui_parameters[1].score+=score; // 增加分数
           // 更新 TextMeshPro顯示分數
           player_textMeshPro.text = "Score\n" + ui_parameters[1].score.ToString();//輸入特殊符號\n表示換行
           ui_parameters[0].HP-=HP;
           slider[0].value =ui_parameters[0].HP;
        }   
  }
}
