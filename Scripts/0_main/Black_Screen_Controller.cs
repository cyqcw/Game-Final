using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//黑屏，用一张全黑图片覆盖屏幕，调整透明度使用curve。
public class Black_Screen_Controller : MonoBehaviour{
 public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;
    RawImage rawImage;  //  都挂在图片下，独立
    void Awake(){
        rawImage=GetComponent<RawImage>();
        rawImage.enabled=true;
    }
    void Start(){
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        if (rawImage.color.a < 0.05f){
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }
    void Update(){
        if(sceneStarting)StartScene();
    }
 
    void StartScene(){
        // FadeToClear();
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        if (rawImage.color.a < 0.05f){
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }

    //  这个大概率用不到，不调用的话，
    //  前面的足够了
    void EndScene(){
        rawImage.enabled=true;
        // FadeToBlack();
        print("using");
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
        // if (rawImage.color.a > 0.95f){
        //     SceneManager.LoadScene(1);
        // }
    }
    void OnDestroy(){
        // EndScene();
    }
}