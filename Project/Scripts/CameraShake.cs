using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set;}
    //當你將 Instance 定義為 static 時，這個屬性屬於 CameraShake 類別本身，而不是該類別的某個實例。
    //這意味著你可以在不創建 CameraShake 物件的情況下，直接通過 CameraShake.Instance 訪問它
    //私有化構造函數：防止外部代碼通過 new 操作符創建新的實例。
    //唯讀屬性：公開一個唯讀的靜態屬性來訪問這個唯一的實例。
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public float shakerTimer;
    //以下是進一步調整camerashake平滑，有沒有用沒關係
    [Header("進一步調整平滑震動")]
    public float startIntensity;
    public float totalShakeTime;
    void Awake()
    {
        Instance = this;//把這個目前自身的實例，腳本塞到Instance
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ControllCameraShake(float intensity, float timer)
    {
        shakerTimer = timer;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        //以下新增調整相機平滑震動
        startIntensity = intensity;
        totalShakeTime = timer;    
    }

    void Update()
    {
        if(shakerTimer>0)
        {
            shakerTimer-=Time.deltaTime;
            if(shakerTimer<=0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                //cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
                //以下是採用平滑方式，有沒有沒差
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity,0,1-(shakerTimer / totalShakeTime));
            }
        }
       

    }
}
