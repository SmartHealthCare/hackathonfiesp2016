using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Collections.Generic;

[System.Serializable]
public class ListaDeHospitais
{
	public List<Hospital> list;
}
	
[System.Serializable]
public class Hospital
{
	public string id;
	public string name;
	public string time;
	public string time2;
	public string end;
	public string tel;
}

[System.Serializable]
public class Atendimento
{
	public string id;

	public string tipo;
	public string hospitalId;

	public string numeroCarteirinha;
	public string nome;

	public string status;
}

[System.Serializable]
public class Ticket
{
	public string message;
	public string id;
	public string link;
}

public class BusinessService : MonoBehaviour {

	public Atendimento atendimento = new Atendimento();
	public ListaDeHospitais hospitais;

	protected WebClient getWebClient() {
		WebClient cli = new WebClient ();

		cli.Headers.Add(HttpRequestHeader.ContentType, "application/json");

		return cli;
	}

	public void SelecionarOrtopedia() {
		atendimento.tipo = "Ortopedia";

		ListarHospitais ();
	}

	public void SelecionarPediatria() {
		atendimento.tipo = "Pediatria";

		ListarHospitais ();
	}

	public void SelecionarGeral() {
		atendimento.tipo = "Geral";

		ListarHospitais ();
	}

	public void ListarHospitais () {
		string stringJson = getWebClient().DownloadString (@"https://smarthealth.inpaas.com/api/v1/hospitais");

		hospitais = JsonUtility.FromJson<ListaDeHospitais>(stringJson);
	}

	public void SelecionarHospital(string hospital) {
		atendimento.hospitalId = hospital;

	}

	protected Atendimento GetAtendimento(string id) {
		string stringJson = getWebClient().DownloadString (@"https://smarthealth.inpaas.com/api/v1/atendimentos/" + id);

		Atendimento temp = JsonUtility.FromJson<Atendimento>(stringJson);

		return temp;
	}

	public void IniciarAtendimento() {
		string json = JsonUtility.ToJson (atendimento);

		string ticketjson = getWebClient().UploadString (@"https://smarthealth.inpaas.com/api/v1/atendimentos", json);
		Ticket ticket = JsonUtility.FromJson<Ticket>(ticketjson);

		atendimento = GetAtendimento (ticket.id);
		Debug.Log("Senha:" + atendimento.id);
		Debug.Log("Status:" + atendimento.status);

	}

	public void Cheguei() {
		atendimento.status = "Triagem";
		string json = JsonUtility.ToJson (atendimento);

		string ticketjson = getWebClient().UploadString (@"https://smarthealth.inpaas.com/api/v1/atendimentos/" + atendimento.id, "PUT", json);
		Ticket ticket = JsonUtility.FromJson<Ticket>(ticketjson);

		atendimento = GetAtendimento (ticket.id);
		Debug.Log("Senha:" + atendimento.id);
		Debug.Log("Status:" + atendimento.status);
	}

	public void Status() {
		GetAtendimento (atendimento.id);
	}
}
