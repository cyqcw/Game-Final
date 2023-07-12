using UnityEngine;

public class Checkpoint : MonoBehaviour{
  public GameObject[] stars; // 星星对象数组，包含0，1，2，3颗星星对应的游戏对象
  private void Start(){
    // 隐藏Canvas
    gameObject.SetActive(false);
    // 停止暂停游戏时间
    Time.timeScale = 1f;
  }

  public void ShowCanvas(int starCount){
    // 显示Canvas
    gameObject.SetActive(true);
    // 暂停游戏时间
    Time.timeScale = 0f;
    // 显示对应数量的星星
    ShowStars(starCount);
  }

  private void ShowStars(int count){
    // 隐藏所有的星星
    HideAllStars();
    // 根据数量显示对应的星星
    stars[count].SetActive(true);
  }

  private void HideAllStars(){
    // 隐藏所有的星星
    foreach (GameObject star in stars){
      star.SetActive(false);
    }
  }
}
