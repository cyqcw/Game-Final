using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//  控制扶梯移动
public class Platform_Controller : MonoBehaviour{
    //
    //【1.68】【0.1】【-2.1】【-4.28】
    //
    //  用另一个脚本负责记录处于触发器区域内的Box数量
    //  此脚本用于移动

    public int initial_layer=0; //  两个platform不同，左边那个是0,右边那个是2？3
    public float time_wait=1.0f;    //  等待时间
    private float time_timer;

    
    float[] layer_position={1.68f,0.1f,-2.1f,-4.28f};   //  记录每一层的高度,高的正

    ArrayList box_name=new ArrayList(); //  记录所有在触发区域内的box名字

    public AudioClip move_platform_sound;   //  移动音效
    AudioSource source_player;
    //  --------------------------------------------------------------
    // public float maxHeight=3.0f; //  最高到哪
    // public float minHeight=-5.0f; //  最低到哪
    public float move_speed=1.0f;
    // bool upToward=true;
    // Transform transform;
 
    // Start is called before the first frame update
    void Start(){
        // transform=gameObject.GetComponent<Transform>();
        source_player=GetComponent<AudioSource>();
        time_timer=0;
        //  不知道transform为什么会差一点
        for(int i=0;i<4;i++){
            layer_position[i]+=0.4f;
        }
        //  x位置在哪就是那，不管x轴
        transform.position=new Vector2(transform.position.x,layer_position[initial_layer]);
    }

    // Update is called once per frame
    void Update(){
        int count=box_name.Count;   //  记录箱子数
        
        if(time_timer>0){   //  计时
            time_timer-=Time.deltaTime;
            if(time_timer<0.01f){
                source_player.PlayOneShot(move_platform_sound,0.7f);
            }
        }else{
            int may_layer=initial_layer+count;  //    应有的层
            if(may_layer>3)may_layer=3; //  最多只能到这了！！！，再多的箱子也没有用
            //  太高了，需要往下
            if(transform.position.y-layer_position[may_layer]>0.01f){
                transform.position-=new Vector3(0.0f,move_speed*Time.deltaTime,0.0f);
            }
            //  太低了，需要往上
            else if(transform.position.y-layer_position[may_layer]<-0.01f){
                transform.position+=new Vector3(0.0f,move_speed*Time.deltaTime,0.0f);
            }            
        }


        // if(upToward){
        //     if(transform.position.y<maxHeight){
        //         transform.position+=new Vector3(0.0f,move_speed*Time.deltaTime,0.0f);
        //     }else upToward=false;
        // }
        // else{
        //     if(transform.position.y>minHeight){
        //         transform.position-=new Vector3(0.0f,move_speed*Time.deltaTime,0.0f);
        //     }else upToward=true;
        // }
    }

    //  记录箱子进入触发器
    void OnTriggerEnter2D(Collider2D other){
        //  有Box进来，加入ArrayList
        if(other.gameObject.name.StartsWith("box")){
            box_name.Add(other.gameObject.name);
            // System.Threading.Thread.Sleep(1000);
            time_timer=time_wait;
        }
    }
    //  记录箱子出触发器
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.name.StartsWith("box")){
            //  从ArrayList中删除
            box_name.Remove(other.gameObject.name);
            // System.Threading.Thread.Sleep(1000);
            time_timer=time_wait;
        }
    }
}

/*   
//  第一个（-5,3）
//  第二个（-5，-0.8）
//  自动移动的代码

    public float maxHeight=3.0f; //  最高到哪
    public float minHeight=-5.0f; //  最低到哪
    public float move_speed=1.0f;
    bool upToward=true;
    Transform transform;
 
    // Start is called before the first frame update
    void Start(){
        transform=gameObject.GetComponent<Transform>();
        //  x位置在哪就是那，不管x轴
        transform.position=new Vector2(transform.position.x,minHeight);
    }

    // Update is called once per frame
    void Update(){
        if(upToward){
            if(transform.position.y<maxHeight){
                transform.position+=new Vector3(0.0f,move_speed*Time.deltaTime,0.0f);
            }else upToward=false;
        }
        else{
            if(transform.position.y>minHeight){
                transform.position-=new Vector3(0.0f,move_speed*Time.deltaTime,0.0f);
            }else upToward=true;
        }
    }

*/
