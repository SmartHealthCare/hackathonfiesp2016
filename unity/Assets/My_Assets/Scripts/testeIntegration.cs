using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using MiniJSON;
using System.Net;
using System.Collections.Generic;
using UnityEngine.UI;


[System.Serializable]
public class Hospitais : IEnumerable<string> {
	
	public List<string> nome;

	public IEnumerator <string> GetEnumerator () {
	
	
		return nome.GetEnumerator ();

	}

	IEnumerator IEnumerable.GetEnumerator(){
	
		return GetEnumerator ();

	}

}


public class testeIntegration : MonoBehaviour {

	//public object[] hotel;
	public IList hoteisRJ;
	public IList hoteisNY;
	public Dictionary<int,string> nomeDeHoteisRJ;
	public Dictionary<int,string> nomeDeHoteisNY;




	void Start(){
	
		nomeDeHoteisRJ = new Dictionary<int,string>();
		nomeDeHoteisNY = new Dictionary<int,string>();

		ChamadaRio ();
		ChamadaNY ();

//		DontDestroyOnLoad (this);
	
	
	}



	public void FindRJTexts(){

		Text[] textos = GameObject.FindObjectsOfType<Text> ();

		for (int i = 0; i < textos.Length; i++) {

			if (textos [i] != null) {

				textos [i].text = "Vai porra!";
//				textos [i].text = nomeDeHoteisRJ [i];

//				textos [i].text = PlayerPrefs.GetString ("hoteisRJ"+ i.ToString());
			}

		}
	}


	public void FindNYTexts(){

		Text[] textos = GameObject.FindObjectsOfType<Text> ();

		for (int i = 0; i < textos.Length; i++) {

			if (textos [i] != null) {
				textos [i].text = nomeDeHoteisNY [i];
//				textos [i].text = PlayerPrefs.GetString ("hoteisNY"+ i.ToString());

			}

		}
	}


	public void ChamadaRio(){

		WebClient wc = new WebClient ();

		string stringJsonRio = wc.DownloadString ("Assets/Hoteis/rio.json");


		string[] hoteisRJ = stringJsonRio.Split (',');
		Text[] textos = GameObject.FindObjectsOfType<Text> ();
	
		for (int i = 0; i < textos.Length; i++) {
			
			if(textos[i] != null || hoteisRJ[i] != null){


				nomeDeHoteisRJ.Add (i, hoteisRJ [i].Replace("\"",""));
//				PlayerPrefs.SetString (hoteisRJ[i],hoteisRJ[i]);
//				textos [i].text = PlayerPrefs.GetString(hoteisRJ [i]);
			}
		}
	}

	public void ChamadaNY(){

		WebClient wc = new WebClient ();

		string stringJsonNY = wc.DownloadString ("Assets/Hoteis/ny.json");
		string[] hoteisNY = stringJsonNY.Split (',');


		Text[] textos = GameObject.FindObjectsOfType<Text> ();

		for (int i = 0; i < textos.Length; i++) {

			if(textos[i] != null || hoteisNY[i] != null){

				nomeDeHoteisNY.Add (i, hoteisNY [i].Replace("\"",""));
//				PlayerPrefs.SetString (hoteisNY[i],hoteisNY[i]);
//				textos [i].text = PlayerPrefs.GetString(hoteisNY [i]);
			}

		}
	}

}
