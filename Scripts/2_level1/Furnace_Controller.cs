using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace_Controller : MonoBehaviour{
    public GameObject star_prefab;  //  这个最好是预制体
    AudioSource source_player;
    public AudioClip burn_sound; //  熔炉燃烧音效
    public AudioClip forge_sound;   //  锻造音效

    float timer=5.0f;   //  计时器控制显示时间
    bool fire_wood=false; 
    // public GameObject key;  //  直接是key对象
    // Start is called before the first frame update
    void Start(){
        source_player=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if(timer>0){
            if(fire_wood){
                timer-=Time.deltaTime;
            }
        }else{
            transform.Find("fire_text").gameObject.SetActive(false);
        }
        
    }
    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.name=="Player" && Input.GetKey(KeyCode.J)){
            //  获得了木材
            if(Player_Control.get_wood){
                source_player.PlayOneShot(burn_sound);
                transform.Find("close_text").gameObject.SetActive(false);
                if(timer>0){
                    transform.Find("fire_text").gameObject.SetActive(true);
                }
                fire_wood=true;
                transform.Find("furnace_close").gameObject.SetActive(false);
                transform.Find("furnace_open").gameObject.SetActive(true);
                //  获得了石头
                if(Player_Control.get_stone){
                    //  钥匙
                    transform.Find("key").gameObject.SetActive(true);
                    source_player.PlayOneShot(forge_sound);
                    // //  星星
                    // GameObject star=Instantiate(star_prefab);
                    // star.name="star_3";
                    // star.transform.position=other.transform.position;
                    // star.transform.position+=new Vector3(2.0f,0.0f,0.0f);
                }
            }
        }
    }
}
