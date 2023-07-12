using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.SceneManagement;
public class Initial_File : MonoBehaviour{
	//	退出的一瞬间将所有文件清零
	string filePath;
	//	文件信息初始化
	public void OnClickExitButtonDel(){
		for(int i=1;i<=6;i++){
			filePath = Path.Combine(Application.streamingAssetsPath,i+".txt");
			WriteToFile(0);		
		}		
	}
	void Start(){

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


	// string filePath;
	// //	文件信息初始化
	// void Start(){
	// 	for(int i=1;i<=6;i++){
	// 		filePath = Path.Combine(Application.streamingAssetsPath,i+".txt");
	// 		WriteToFile(0);		
	// 	}
	// }
    // private void WriteToFile(int number){
	// 	try{
	// 		// 写入文件
	// 		using (StreamWriter writer = new StreamWriter(filePath)){
	// 			writer.Write(number.ToString());
	// 		}
	// 		Debug.Log("文件写入成功。");
	// 	}
	// 	catch (System.Exception ex){
	// 		Debug.LogError("写入文件时发生错误：" + ex.Message);
	// 	}
  	// }