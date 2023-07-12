using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//  挂载到sound Button上
//  点击事件用到自己身上的方法，所以自己挂载自己点击事件上面，不用update
public class Sound_Controller : MonoBehaviour{
    public void OnClick(){
        GameObject btn=transform.Find("Sound_Slider").gameObject;
        //  更新
        if(btn.activeInHierarchy){
            btn.SetActive(false);
        }else btn.SetActive(true);
    }
}
