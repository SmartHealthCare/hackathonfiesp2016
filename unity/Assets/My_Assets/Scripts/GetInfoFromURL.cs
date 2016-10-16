//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//using System.Net;
//
//
//public class GetInfoFromURL : MonoBehaviour {
//	
//	public string nome;
//	public int carteiraPlano;
//	public string stringId;
//	public InputField nameInputField;
//	public InputField carteiraInputField;
//	public static GetInfoFromURL info;
//
//	public Text textoNome;
//	public Text textoCarteira;
//
//
//	public void Test ()
//	{
//		WebClient client = new WebClient();
//
//		string stringJson = client.DownloadString (@"http://chipinfor1.tempsite.ws/hackathon/api/places.json");
//
//		Debug.Log (stringJson);
//
//		PatientData myPatient;
//
//		myPatient = JsonUtility.FromJson<PatientData>(stringJson);
//
//
////		string[] hospitais = stringJson.Split (',');
//		Debug.Log (myPatient);
//
//	}
//
//
//
//
//	void Awake(){
//
//		if (info == null) {
//
//			DontDestroyOnLoad (gameObject);
//			info = this;
//
//		} else if (info != this) {
//		
//			Destroy (gameObject);
//		
//		}
//	}
//
//	public void SaveData(){
//
//		BinaryFormatter bf = new BinaryFormatter();
//		FileStream file = File.Create(Application.persistentDataPath + "/appInfo.dat");
//
//		PatientData patientData = new PatientData();
//		patientData.name = nameInputField.text;
//		patientData.id = carteiraInputField.text;
//		bf.Serialize(file,patientData);
//
//		file.Close();
//	}
//
//
//	public void LoadData(){
//
//		if (File.Exists (Application.persistentDataPath + "/appInfo.dat")) {
//		
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream file = File.Open (Application.persistentDataPath + "/appInfo.dat", FileMode.Open);
//			PatientData patientData = (PatientData) bf.Deserialize (file);
//			file.Close ();
//
//			nome = patientData.name;
//			stringId = patientData.id;
//
//			textoNome.text = "Nome do Paciente: " + nome;
//			textoCarteira.text = "Carteira numero:" + carteiraPlano;
//
//		}
//
//	}
//}
//
//
//[Serializable]
//
//class PatientData {
//
//	public string id;
//	public string name;
//	public string time2;
//	public string end;
//	public string tel;
//
//}