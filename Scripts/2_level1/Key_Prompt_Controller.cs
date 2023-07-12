using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Prompt_Controller : MonoBehaviour{
    //  传入玩家的信息获取位置
    public GameObject player;
    bool close_prompt=false;

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        //  没有关闭的时候才判断
        if(!close_prompt){
            if(player.transform.position.y>0){
                close_prompt=true;
                this.gameObject.SetActive(false);
            }       
        }

    }
}
