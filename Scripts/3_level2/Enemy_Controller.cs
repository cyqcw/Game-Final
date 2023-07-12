using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Controller : MonoBehaviour{
    // Start is called before the first frame update
    // public Player_Control script;   //  传入挂载脚本的对象
    public AudioClip dead_sound;    //  死亡音效
    
    AudioSource source_player;  //  
    
    Rigidbody2D rigidbody2d;//

    public GameObject player;   //  拖入玩家的对象
    public float left_distance=4.0f;
    public float right_distance=9.0f;
    bool left=true; //  首先先向左移
    public float initial_speed=1.0f;
    private float move_speed;
    Animator animator;  //  动画


    float timer;
    // Vector2 initial_player_position;
    // void Start(){
    //     wake_enemy=player.GetComponent<Player_Control>().wake_enemy;
    // }
    
    void Start(){
        timer=5.0f;
        move_speed=initial_speed;
        source_player=GetComponent<AudioSource>();
        rigidbody2d=GetComponent<Rigidbody2D>();
        //  记录player的初始位置
        // initial_player_position=new Vector2(player.transform.position.x,player.transform.position.y);
        animator = transform.Find("character").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        //  根据距离选择是否要加速
        if(Math.Abs(player.transform.position.x-transform.position.x)<4f){
            if(Math.Abs(player.transform.position.y-transform.position.y)<0.2f){
                move_speed=3*initial_speed;
            }
        }else{
            move_speed=initial_speed;
            // animator.SetBool("BoolRunTrigger",false);
        }
        // wake_enemy=player.GetComponent<Player_Control>().wake_enemy;
        if(Player_Control.wake_enemy){
            //  文字提示5s后消失
            if(timer>0){
                transform.Find("enemy_sleep").gameObject.SetActive(false);
                transform.Find("enemy_wake").gameObject.SetActive(true);
                timer-=Time.deltaTime;           
            }else{
                transform.Find("enemy_wake").gameObject.SetActive(false);
            }

            animator.SetBool("BoolRunTrigger",true);
            int y_direction=(int)transform.Find("character").rotation.y;
            if(left){
                //  转向
                if(y_direction!=1){
                    transform.Find("character").rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                //  移动
                if(transform.position.x>left_distance){
                    transform.position-=new Vector3(move_speed*Time.deltaTime,0.0f,0.0f);
                }else left=false;
            }
            else{
                //  转向
                if(y_direction!=0){
                    transform.Find("character").rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                //  移动
                if(transform.position.x<right_distance){
                    transform.position+=new Vector3(move_speed*Time.deltaTime,0.0f,0.0f);
                }else left=true;
            }
        }
    }

    // void OnColliderEnter2D(Collider2D other){
    //     print("This");
    //     //  碰到玩家
    //     if(other.gameObject.name=="Player"){
    //         // Rigidbody2D rigidbody2d=other.GetComponent<Rigidbody2D>();
    //         print("catch");
    //         // rigidbody2d.position=new Vector2(-10,3);
    //         other.transform.position=new Vector2(-10,3);
    //     }
    // }
    //  这里要用Collision不用Collider
    void OnCollisionEnter2D(Collision2D other){
        print("This");
        //  碰到玩家
        if(other.gameObject.name=="Player"){
            source_player.PlayOneShot(dead_sound);
            // Rigidbody2D rigidbody2d=other.GetComponent<Rigidbody2D>();
            print("catch");
            // rigidbody2d.position=new Vector2(-10,3);
            other.transform.position=new Vector2(-10,3);
        }
    }
}
