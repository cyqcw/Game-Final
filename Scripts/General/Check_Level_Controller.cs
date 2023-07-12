using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
// using UnityEngine.Color;
//  控制关卡星数的显示，挂载到grid上面
public class Check_Level_Controller : MonoBehaviour
{
  //	公有静态变量，不确定安全性，后续可以进行封装
  public TMP_Text text;

  private string filePath; // 文件路径
  public static int star_num_sum = 0; //	获得的星星总数
  public static int level_num=0;  //	通关数0-5
  public static int[] level_star_num = { 0, 0, 0, 0, 0, 0 };  //new int[6];	//	各关获得的星星数

  private string ReadFromFile(string filePath){
    string fileContent = "";

    try{
      // 从文件中读取内容
      using (StreamReader reader = new StreamReader(filePath)){
        fileContent = reader.ReadLine();
      }
    }catch (System.Exception ex){
      Debug.LogError("读取文件时发生错误：" + ex.Message);
    }
    return fileContent;
  }

  //	初始时将所有都先渲染
  void Start(){
    level_num=0;
    star_num_sum=0;
    for (int i = 1; i <= 6; i++){
      filePath = Path.Combine(Application.streamingAssetsPath, i + ".txt"); // 构建文件路径
      level_star_num[i - 1] = int.Parse(ReadFromFile(filePath));
      if(level_star_num[i-1]>0)level_num+=1;
      star_num_sum+=level_star_num[i-1];
    }
    text.text=star_num_sum+"/18";
    //	使用transform而不是gameObject
    for (int i = 0; i < 6; i++){
      //	获取按钮
      GameObject btn1 = transform.Find((i + 1) + "_Button").gameObject; //	按钮1-6
                                                                        //	此关已通
      if (i<level_num){
        //	已通关的关卡有星星记录
        int StarNum = level_star_num[i];
        GameObject star_image = btn1.transform.Find("Star_GameObject").transform.Find(StarNum+"_star_image").gameObject;
        star_image.SetActive(true);
        // if (StarNum == 0){
        //   GameObject star_image = btn1.transform.Find("Star_GameObject").transform.Find("0_star_image").gameObject;
        //   star_image.SetActive(true);
        // }
        // else if (StarNum == 1){
        //   GameObject star_image = btn1.transform.Find("Star_GameObject").transform.Find("1_star_image").gameObject;
        //   star_image.SetActive(true);
        // }
        // else if (StarNum == 2){
        //   GameObject star_image = btn1.transform.Find("Star_GameObject").transform.Find("2_star_image").gameObject;
        //   star_image.SetActive(true);
        // }
        // else if (StarNum == 3)
        // {
        //   GameObject star_image = btn1.transform.Find("Star_GameObject").transform.Find("3_star_image").gameObject;
        //   star_image.SetActive(true);
        // }
        //	已通过的关卡改变颜色
        Image image = (Image)btn1.GetComponent("Image");//.
        image.color = new Color(0, 230 / 255f, 231 / 255f);
      }
      //	此关不通
      else if (i > level_num){
        GameObject lock_image = btn1.transform.Find("lock_image").gameObject;
        lock_image.SetActive(true);
      }
      //	当前关卡解锁，默认是0星
      else
      {
        GameObject star_image = btn1.transform.Find("Star_GameObject").transform.Find("0_star_image").gameObject;
        star_image.SetActive(true);
      }
    }
  }


  //	再看用户点击的是哪一个



  //	如果没有上锁，就可以跳转






  // [Header("最高可挑战的关卡")]
  // private int challengingLevel;

  // /// <summary>
  // /// 选择框
  // /// </summary>
  // private Transform select;

  // private int currentLevelNum;

  // void Awake()
  // {
  // 	//找到选择框
  // 	select = GameObject.FindWithTag("Select").transform;
  // }

  // void Start(){
  // 	challengingLevel = Singleton.GetInstance().levelAndStars.Count + 1;
  // 	InitUI();
  // }

  // public void InitUI(){
  // 	//初始化UI
  // 	for (int i = 0; i < transform.childCount; i++){
  // 		//找到第i个子对象（关卡）
  // 		Transform child = transform.GetChild(i);
  // 		Transform stars = child.Find("stars");
  // 		//更改子对象关卡编号
  // 		child.Find("Text").GetComponent<Text>().text = (i+1).ToString();
  // 		//如果当前关卡编号小于最高可挑战关卡编号，就解锁
  // 		if (i < challengingLevel){
  // 			//把小锁图片设为非激活
  // 			child.Find("lockImage").gameObject.SetActive(false);
  // 			//让关卡按钮可以点击
  // 			child.GetComponent<Button>().interactable = true;
  // 			//将解锁后的关卡编号文字颜色变亮
  // 			child.Find("Text").GetComponent<Text>().color = new Color32(255, 234, 149,255);
  // 			//最新解锁的关卡就解锁以上内容直接continue
  // 			if (i + 1 == challengingLevel){
  // 				continue;
  // 			}

  // 			//将星星背景框激活
  // 			stars.gameObject.SetActive(true);
  // 			//根据各个关卡获得的星星数激活指定数量的星星
  // 			switch (Singleton.GetInstance().levelAndStars[i + 1]){
  // 				case 1:
  // 					stars.GetChild(0).gameObject.SetActive(true);
  // 					break;
  // 				case 2:
  // 					stars.GetChild(0).gameObject.SetActive(true);
  // 					stars.GetChild(2).gameObject.SetActive(true);
  // 					break;
  // 				case 3:
  // 					for (int j = 0; j < 3; j++){
  // 						stars.GetChild(j).gameObject.SetActive(true);
  // 					}
  // 					break;

  // 				default:
  // 					break;
  // 			}
  // 		}
  // 	}
  // }

  // //点击关卡按钮时响应此方法（传入的是关卡编号）
  // public void CheckLevelByClick(int levelNum){
  // 	//获取当前选择的关卡
  // 	Transform currentLevel = transform.GetChild(levelNum - 1);
  // 	//选择框的父对象变更为当前选中的关卡
  // 	select.SetParent(currentLevel);
  // 	//选择框放到最底层
  // 	select.SetSiblingIndex(0);
  // 	//选择框位置变更到当前选择的关卡
  // 	select.localPosition = Vector3.zero;
  // 	//保存当前选中的关卡编号（这里也可以直接用单例对象接收）
  // 	currentLevelNum = levelNum;
  // }
  // //点击对号按钮时执行此方法
  // public void OnClickLoadScene(){
  // 	//将当前关卡编号保存在单例中
  // 	Singleton.GetInstance().levelNum = currentLevelNum;
  // 	//跳转场景
  // 	SceneManager.LoadScene("LevelComplete");
  // }
}




