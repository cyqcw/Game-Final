using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour{
  public void GoToStartPage(){
    Time.timeScale = 1f;
    SceneManager.LoadScene("0_main"); // 替换为你的开始页面场景名称
    print("hello");
  }
  public void GoToLevelSelection(){
    Time.timeScale = 1f;
    SceneManager.LoadScene("1_choose"); // 替换为你的选关页面场景名称
  }

  public void GoToNextLevel(){
    Time.timeScale = 1f;
    // 获取当前场景的索引
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    // 加载下一个场景
    SceneManager.LoadScene(currentSceneIndex + 1);
  }
}
