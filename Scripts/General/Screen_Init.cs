using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Init : MonoBehaviour{
    private void Awake(){
    int height = Camera.main.pixelHeight;
    int width = Camera.main.pixelWidth;
    print(width);
    print(height);

    // 是编辑模式下固定的屏幕宽度和屏幕高度,Screen.Height和Screen.Width是实际屏幕高度和实际屏幕宽度
    Camera.main.orthographicSize = Camera.main.orthographicSize * 890 / 395 * Screen.height / Screen.width;
  }
}
