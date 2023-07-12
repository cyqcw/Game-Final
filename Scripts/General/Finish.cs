using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Finish : MonoBehaviour{
  public Checkpoint checkpoint; //  显示通关后的而星星数
  private BoxCollider2D boxCollider2D; // 碰撞体
  AudioSource source_player;
  
  public AudioClip get_star_sound;  //  获得星星
  public AudioClip open_door_sound;  //  行走音效
  // private bool indoor = false;
  // public bool opendoor = true;
  GameObject open_door;
  GameObject close_door;
  bool can_to_next=false;
  // Start is called before the first frame update
  void Start(){
    boxCollider2D = GetComponent<BoxCollider2D>();
    open_door=transform.Find("open_door").gameObject;
    close_door=transform.Find("close_door").gameObject;
    source_player=GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update(){
    // FinishLevel();
  }

  //  modify date 6.6 from cui
  private void OnTriggerEnter2D(Collider2D other){
    //  已经获得钥匙
    if (other.gameObject.name=="Player" && Player_Control.get_key){
      print("Open Door");
      open_door.SetActive(true);
      close_door.SetActive(false);
      //  第一次来
      if(!can_to_next)source_player.PlayOneShot(open_door_sound,1.0f);
      can_to_next=true;
      
      // if(Input.GetKeyDown("w")){
      //   SceneManager.LoadScene("1_choose");
      // }
    }
  }
  private void OnTriggerStay2D(Collider2D other){
    if(can_to_next && Input.GetKey(KeyCode.W)){
      // 获取当前场景的名称
      string sceneName = SceneManager.GetActiveScene().name;
      // 读取StreamingAssets文件夹下的1.txt文件路径
      string filePath = Path.Combine(Application.streamingAssetsPath, sceneName[7] + ".txt");
      int star_num = int.Parse(ReadFromFile(filePath));
      if(star_num>3)star_num=3;
      // 玩家抵达终点，显示Canvas并结算星星数
      checkpoint.ShowCanvas(star_num); //
      source_player.PlayOneShot(get_star_sound);
      // SceneManager.LoadScene("1_choose");
    }
  }
  private string ReadFromFile(string filePath){
    string fileContent = "";

    try{
      // 从文件中读取内容
      using (StreamReader reader = new StreamReader(filePath)){
        fileContent = reader.ReadLine();
      }
    }
    catch (System.Exception ex){
      Debug.LogError("读取文件时发生错误：" + ex.Message);
    }
    return fileContent;
  }

  // void FinishLevel(){
  //   if () && can_to_next){
  //     print("finish");
      
  //   }
  // }
}
