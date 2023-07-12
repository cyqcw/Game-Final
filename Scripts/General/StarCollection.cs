using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class StarCollection : MonoBehaviour{
  private int starNum;  //  所有的星星数
  private string filePath; // 文件路径

  // public AudioClip get_tool_sound;  //  获得东西音效
  // AudioSource source_player;
  // 进入当前场景时
  void Start(){
  // void OnLevelWasLoaded(){
    // 文件路径的获取：场景名称的第七个字符 + .txt
    // source_player=GetComponent<AudioSource>();
    filePath = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name[7] + ".txt");
    // print("save_path:"+filePath);
    // WriteToFile(0);
  }
  private void OnTriggerEnter2D(Collider2D other){
    if (other.CompareTag("Player")){
      // source_player.PlayOneShot(get_tool_sound);
      this.gameObject.SetActive(false);

      
      // 读取文件内容
      string fileContent = ReadFromFile(filePath);
      int number;

      if(int.TryParse(fileContent, out number)){
        print("number:"+number);
        // 将数字加一
        number++;
        if (number > 3){
          number = 3;
        }
        // 写入文件
        WriteToFile(number);
      }
      else{
        Debug.LogError("无法解析文件中的数字。");
      }
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

  private void WriteToFile(int number){
    try{
      // 写入文件
      using (StreamWriter writer = new StreamWriter(filePath)){
        writer.Write(number.ToString());
      }
      Debug.Log("文件写入成功。");
    }
    catch (System.Exception ex){
      Debug.LogError("写入文件时发生错误：" + ex.Message);
    }
  }
}
