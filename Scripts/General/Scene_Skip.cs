using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene_Skip : MonoBehaviour{
	// AudioSource source_player;
  
  	// public AudioClip click_button_sound;  //  
	// AudioSource source_player;
	void Start(){
		// source_player=GetComponent<AudioSource>();
	}

	//	跳转到主界面
	public void OnClickLoadScene_to_0_main(){
		// source_player.PlayOneShot(click_button_sound);
		SceneManager.LoadScene("0_main");
	}	
	//	跳转到选关界面
	public void OnClickLoadScene_to_1_choose(){
		// source_player.PlayOneShot(click_button_sound);
		SceneManager.LoadScene("1_choose");
	}
	//	退出游戏
	public void OnClickExitScene(){
		// source_player.PlayOneShot(click_button_sound);
		Application.Quit();
	}
	//	跳转到关卡1
	public void OnClickSkipScene_to_level1(){
		// source_player.PlayOneShot(click_button_sound);
		//	关卡1随便跳
		SceneManager.LoadScene("2_level1");
	}
	//	跳转到关卡2
	public void OnClickSkipScene_to_level2(){
		// source_player.PlayOneShot(click_button_sound);
		if(Check_Level_Controller.level_num>0)SceneManager.LoadScene("3_level2");
	}
	//	跳转到关卡3
	public void OnClickSkipScene_to_level3(){
		// source_player.PlayOneShot(click_button_sound);
		if(Check_Level_Controller.level_num>1)SceneManager.LoadScene("4_level3");
	}
	//	点击显示声音slide
	public void OnClickShowSlider(){
		
	}
}
