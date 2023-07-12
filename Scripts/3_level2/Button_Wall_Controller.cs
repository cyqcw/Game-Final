using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Wall_Controller : MonoBehaviour{
    //  两个需要干掉的对象
    //  红色按钮原来的位置是（-3,0.2,0）,按下按钮后移动到（-3,0.1,0）
    public GameObject wall1;
    public GameObject wall2;
    private bool have_wall=true;
    public AudioClip press_button_sound;   //  移动音效
    AudioSource source_player;

    // Start is called before the first frame update
    void Start(){
        source_player=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name.StartsWith("box")){
            //  set false
            wall1.SetActive(false);
            wall2.SetActive(false);
            transform.position=new Vector2(transform.position.x,transform.position.y-0.1f);
            source_player.PlayOneShot(press_button_sound);
        }
    }
    private void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.name.StartsWith("box")){
            //  set true
            wall1.SetActive(true);
            wall2.SetActive(true);
            transform.position=new Vector2(transform.position.x,transform.position.y+0.1f);
            source_player.PlayOneShot(press_button_sound);
        }
    }
}
