//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//using System.Net;
//using System.Collections.Generic;
//
//
//
//[Serializable]
//
//public class RootObject
//{
//	public string id;
//	public string name;
//	public string time;
//	public string time2;
//	public string end;
//	public string tel;
//}
//
//
//
//
//public class testJSON : MonoBehaviour {
//
//	public Dictionary <int,string> hospitais;
//	public List<string> cadaHospital;
//	public List<string> cadaInfo;
//
//	JSONObject jsonObj;
//
//
//
//
////	public void TestToJson ()
////	{
////
////		WebClient client = new WebClient();
////		RootObject myHospital =  new RootObject();
////		myHospital.id = "171";
////		myHospital.name = "Fulano";
////		myHospital.tel = "666Calabocaenaochateia";
////		string json = JsonUtility.ToJson(myHospital);
////		myHospital = JsonUtility.FromJson<RootObject>(json);
//////		Debug.Log (myHospital.name);
////
////	}
////
////
////	public void Test(){
////		WebClient otherClient = new WebClient();
////		RootObject myHospital =  new RootObject();
////		string stringJson = otherClient.DownloadString (@"http://chipinfor1.tempsite.ws/hackathon/api/places.json");
////		stringJson = stringJson.Replace ("},","");
////		string[] oneHospital = stringJson.Split ('{');
//////		Debug.Log (onePatient[3]);
////
////		for (int i = 0; i < oneHospital.Length; i++) {
//////			Debug.Log (oneHospital[i]);
////		}
////	}
//
//
//	public void SplitTexts(){
//	
//		//PEGA JSON DA WEB//
//
//		WebClient otherClient = new WebClient();
//		RootObject myHospital =  new RootObject();
//		string stringJson = otherClient.DownloadString (@"http://chipinfor1.tempsite.ws/hackathon/api/places.json");
//		string json = JsonUtility.ToJson(myHospital);
//
//		Debug.Log (json);
//
//
//
//
////TIRA CHAVES DO JSON//
////		stringJson = stringJson.Replace ("},","");
////		Debug.Log (stringJson);
//
//
//		//SEPARA AS INFOS POR HOSPITAL E ADICIONA TODOS EM UMA LISTA//
////		string[] oneHospital = stringJson.Split ('{');
////		for (int i = 0; i < oneHospital.Length; i++) {
////			cadaHospital.Add (oneHospital [i]);
////			Debug.Log ("HOSPITAL: " + i + cadaHospital[i]);
//
////		}
//			
//
//
//		//SEPARA AS INFOS DE CADA HOSPITAL//
//
////		for (int i = 0; i < (cadaHospital.Count)*6; i++) {
////			string [] eachInfo = cadaHospital[i].Split (',');
////			cadaInfo.Add (eachInfo [i]);
////			Debug.Log (cadaInfo[i]);
////		}
//
////		for (int i = 0; i < oneHospital.Length; i++) {
////
////			string[] eachHospital = oneHospital[i].Split(',');
////			hospitais.Add (i, eachHospital[i]);
////			Debug.Log (hospitais[i]);
////		}
//	}
//
//}
