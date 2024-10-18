using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyStarFlying : MonoBehaviour
{
    public RectTransform score;//目標位置
    public float moveSpeed=0;
    public Camera mainCamera;
    public Vector3 po;
    public float distance;

    void Start()
    {
        score = GameObject.Find("EnemyCharacter").GetComponent<RectTransform>();
        mainCamera = Camera.main;
        transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                 .SetLoops(-1, LoopType.Incremental);
    }

    void LateUpdate()
    {
       po = mainCamera.ScreenToWorldPoint(score.transform.position);
       Vector3 targetpostion = new Vector3(po.x,po.y,po.z);
       distance = Vector3.Distance(po,this.gameObject.transform.position);
       MovePath(); 
       //this.transform.position = Vector3.MoveTowards(this.transform.position, targetpostion, moveSpeed*Time.deltaTime);
        if(distance<0.5f)
            Destroy(this.gameObject);
    }

    public void MovePath()//建立一個比較生動的DOTween方式移動星星
    {
            transform.DOMove(po, 0.3f)
            .SetEase(Ease.InOutQuad) // 设置缓动类型，让动画更自然
            .OnComplete(() => {
                // 动画完成后的回调，例如播放得分音效等
                Debug.Log("Star reached the scoreboard!");
            });
    }
}
