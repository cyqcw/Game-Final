using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Control : MonoBehaviour{
  // 供外部使用的变量
  //---------------------------------------------
  public static bool wake_enemy=false;  //  是否唤醒敌人
  
  public static bool get_key=false; //  是否获得key
  public static bool get_wood=false;  //  是否获得wood
  public static bool get_stone=false; //  是否获得石头
  //------------------------------------------------

  //  星星相关
  public GameObject star_prefab;  //  星星的预制体或对象？

  //  音乐相关
  //-------------------------------------------
  public AudioClip dead_sound;    //  死亡音效
  public AudioClip whistle_sound;  //  吹哨音效
  public AudioClip jump_sound;  //  跳跃音效
  public AudioClip get_tool_sound;  //  获得东西音效
  // public AudioClip walk_sound;  //  行走音效
  AudioSource source_player;
  //-------------------------------------------
  
  Rigidbody2D rigidbody2d; // 刚体
  private BoxCollider2D boxCollider2D; // 碰撞体
  Animator animator;
  private float horizontal;   //  水平方向上的移动分量
  private float vertical;     //  竖直方向上的移动分量
  public float run_speed = 10;  //  速度
  public float jump_speed = 10; // 跳跃速度
  public float climb_speed = 3;
  private bool isGround; // 地面判断标志

  public float xMax = 10.82f, xMin = -10.82f, yMax = 10f, yMin = -5f; // 活动范围

  
  private bool isLadder;  //  靠近梯子
  private bool isClimbing; 

  // Start is called before the first frame update
  void Start(){
    get_key=false;
    get_stone=false;
    get_wood=false;
    wake_enemy=false;
    rigidbody2d = GetComponent<Rigidbody2D>();
    boxCollider2D = GetComponent<BoxCollider2D>();
    animator=transform.Find("character").gameObject.GetComponent<Animator>();
    source_player=GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void FixedUpdate(){
    PlayAnime();
    Run();
    climb();
  }

  void Update(){
    CheckGrounded();
    Jump();
    boundary();
    climbCheck();
  }

  //  播放动画的方法
  //  2023.6.6 修改，全部修改为bool触发
  void PlayAnime(){
    horizontal = 0;
    horizontal = Input.GetAxis("Horizontal");   //  获取水平方向分量变化

    //  持续按键说明左右移动，激活触发器
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
      animator.SetBool("BoolRunTrigger",true);
      // source_player.PlayOneShot(walk_sound,1.0f);   //  播放音效
      // animator.SetTrigger("RunTrigger");
    }else{
      animator.SetBool("BoolRunTrigger",false);
    }
    //  按w键爬，旁边必须有梯子，当在梯子上时还是爬的动作
    //  首次需要按w后面就不需要
    if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))&& isClimbing){
      animator.SetBool("BoolClimbTrigger",true);
      // animator.SetTrigger("ClimbTrigger");
    }else if(!isClimbing){  //  如果不在梯子上的话
      animator.SetBool("BoolClimbTrigger",false);
      // animator.ResetTrigger("ClimbTrigger");
      // animator.SetTrigger("ClimbBackTrigger");  
    }


    // if (Input.GetButtonDown("Jump") && isGround){
    if((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space)) && isGround){
      animator.SetTrigger("JumpTrigger");
    }

    //  方向和移动
    if (horizontal > 0.01 || horizontal < -0.01){
      //  改变人物的方向
      int y_direction = (int)transform.Find("character").rotation.y;
      if (horizontal < 0 && y_direction == 0){
        // print("this:"+y_direction);
        transform.Find("character").rotation = Quaternion.Euler(new Vector3(0, 180, 0));
      }
      else if (horizontal > 0 && y_direction == 1){
        transform.Find("character").rotation = Quaternion.Euler(new Vector3(0, 0, 0));
      }
    }
  }

  // 重构左右移动方法
  // Author 范周喆
  // Date 5.19
  // 2023.6.6 from cui
  void Run(){
    float moveDir = Input.GetAxis("Horizontal"); // 获取水平方向的速度单位矢量
    // // 玩家的速度矢量，水平方向是水平方向的单位速度矢量乘以速率，竖直方向保持本来的速度
    // Vector2 playerVel = new Vector2(moveDir*run_speed,rigidbody2d.velocity.y); // 
    // rigidbody2d.velocity=playerVel;//+new Vector2( 0.0f,rigidbody2d.velocity.y);
    //  这种方法不会卡墙
    rigidbody2d.position+=new Vector2(moveDir*run_speed*0.025f,0.0f);
    // rigidbody2d.MovePosition(rigidbody2d.position+Vector2.right*moveDir*run_speed*Time.deltaTime);
  }



  //   检测地面
  void CheckGrounded(){
    isGround = boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
  }

  // 重构跳跃方法
  // Author 范周喆
  // Date 5.19
  void Jump(){
    //  Jump事件，要对跳跃事件进行控制
    if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Space))&& isGround){
      Vector2 jumpVel = new Vector2(0.0f, jump_speed); // 跳跃产生的速度矢量
      rigidbody2d.velocity = Vector2.up * jumpVel;
      source_player.PlayOneShot(jump_sound,1.0f);   //  播放音效
    }
  }

  // 边缘检测
  //  针对第二关进行修改
  //  2023.6.6 from cui
  void boundary(){
    if (rigidbody2d != null){
      rigidbody2d.position = new Vector2(
        //  考虑到第一关不需要对y轴进行限制，而第二关掉落时让player回到原位
        Mathf.Clamp(rigidbody2d.position.x, xMin, xMax),rigidbody2d.position.y);
        // Mathf.Clamp(rigidbody2d.position.y, yMin, yMax));
        //  掉落回到原位
        if(rigidbody2d.position.y<yMin){
          rigidbody2d.position=new Vector2(-10,3);
          source_player.PlayOneShot(dead_sound); 
        }
    }
  }

  void climbCheck(){
    vertical = Input.GetAxis("Vertical");
    if (isLadder){
      isClimbing = true;
    }
  }


  void climb(){
    if (isClimbing){
      rigidbody2d.gravityScale = 0f;
      rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, vertical * climb_speed);
    }
    else{
      rigidbody2d.gravityScale = 3f;
    }
  }

  // modify date 6.6 from cui 
  private void OnTriggerEnter2D(Collider2D other){
    if (other.CompareTag("Ladder")){
      isLadder = true;
    }
    //  碰到假墙，假墙false
    if(other.gameObject.name=="ground_fake"){
      other.gameObject.SetActive(false);
      source_player.PlayOneShot(get_tool_sound);
    }
    //  碰到钥匙,阐明一个星星的预制体
    if(other.gameObject.name=="key"){
      other.gameObject.SetActive(false);  //  钥匙拿到
      get_key=true;
      transform.Find("inner_key").gameObject.SetActive(true); //  挂到头上
      //  创建一个星星
      GameObject star_key=Instantiate(star_prefab);
      star_key.name="star_key";
      star_key.transform.position=other.transform.position;
      star_key.transform.position+=new Vector3(2.0f,0.0f,0.0f);
      source_player.PlayOneShot(get_tool_sound);
      // other.transform.Find("star".gameObject.SetActive(true));
    }
    //  获得木材
    if(other.gameObject.name=="wood"){
      other.gameObject.SetActive(false);
      get_wood=true;
      transform.Find("wood").gameObject.SetActive(true);
      source_player.PlayOneShot(get_tool_sound);
      //  自己头上加一个木材
    }
    if(other.gameObject.name=="stone"){
      other.gameObject.SetActive(false);
      get_stone=true;
      transform.Find("stone").gameObject.SetActive(true);
      source_player.PlayOneShot(get_tool_sound);
      //  头上挂个石头
    }
    //  选择重开
    if(other.gameObject.name=="RestartButton"){
      SceneManager.LoadScene("1_choose");
    }
    //  星星
    if(other.gameObject.name.StartsWith("star")){
      source_player.PlayOneShot(get_tool_sound);
    }

  }

  // add date 6.6 from cui
  //  吹哨，trigger stay
  private void OnTriggerStay2D(Collider2D other){
    if(other.gameObject.name=="whistle"){
      if(Input.GetKey(KeyCode.J)){
        print("吹哨");
        source_player.PlayOneShot(whistle_sound,1.0f);   //  播放音效
        if(!wake_enemy){
          wake_enemy=true;
          //  创建一个星星的预制体
          GameObject star_whistle=Instantiate(star_prefab);
          // GameObject star_key=GameObject.Instantiate(Resources.Load("star.prefab")) as GameObject;other.transform.Quaternion.identity
          star_whistle.name="star_whistle";
          star_whistle.transform.position=other.transform.position;
          star_whistle.transform.position+=new Vector3(2.0f,0.0f,0.0f);          
        }
      }
    }
  }

  private void OnTriggerExit2D(Collider2D other){
    if (other.CompareTag("Ladder")){
      isLadder = false;
      isClimbing = false;
    }
  }
  
}
