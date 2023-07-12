using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  只针对第一关，挂载到第一关的箱子上
public class Level1_Box_Wood_Controller : MonoBehaviour{
    // Start is called before the first frame update
    GameObject open_box;
    GameObject close_box;
    GameObject box_stone;
    void Start(){
        open_box=transform.Find("box_open").gameObject;
        close_box=transform.Find("box_close").gameObject;
        box_stone=transform.Find("stone").gameObject;
    }

    // Update is called once per frame
    void Update(){
        
    }

    // void OnColliderStay2D(Collider2D other){
    //     if(other.gameObject.name=="Player" && Input.GetKey(KeyCode.K)){
    //         //  显示打开的箱子和木材
    //         close_box.SetActive(false);
    //         open_box.SetActive(true);
    //         box_wood.SetActive(true);
    //     }
    // }
    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.name=="Player" && Input.GetKey(KeyCode.J)){
            //  显示打开的箱子和木材
            close_box.SetActive(false);
            open_box.SetActive(true);
            box_stone.SetActive(true);
        }
    }
}
