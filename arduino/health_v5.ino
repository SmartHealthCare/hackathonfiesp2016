//SMART HEALTH
#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>
//#include <DallasTemperature.h>

//Configuração
int ledPin = 13;
int sensorPin = 0;
double alpha = 0.75;
int period = 20;
double change = 0.0;
int tentativa = 0;

const char* ssid = "WIFI_FIESP";
const char* password = "fiesp@2016";
//const char* ssid = "HACKATHON";
//const char* password = "hackathon@2016";

//https://smarthealth.inpaas.com/api/v1/data
const char* host = "smarthealth-130552708.sa-east-1.elb.amazonaws.com"; //Não clocar o http
const int httpsPort = 80; //443 se https

// Se for https, usar SHA1 fingerprint como certificado
const char* fingerprint = "72 41 b4 21 cc fc c3 60 1c e6 d3 59 9d 9d 7e 28 43 b8 37 2b";

WiFiServer server(80);

void setup ()
{
//  pinMode (ledPin, OUTPUT);
//  digitalWrite(ledPin, LOW);
  Serial.begin (115200);
  delay(10);
 
  // Connect to WiFi network
  Serial.println();
  Serial.println();
  Serial.print("Conectando a rede ");
  Serial.println(ssid);
 
  WiFi.begin(ssid, password);
 
  while (WiFi.status() != WL_CONNECTED) {
    tentativa = tentativa + 1;
    delay(500);
    Serial.print(".");
    if (tentativa > 50)
    {
      tentativa = 0;
      Serial.println("");
      Serial.println("Verifique a sua rede WiFi");
    }
  }
  Serial.println("");
  Serial.println("WiFi conectado!");
  Serial.println("");
  
  // Start the server
  server.begin();
  Serial.println("Servidor iniciado");
 
  // Print the IP address
  Serial.print("Use esta URL para conectar: ");
  Serial.print("http://");
  Serial.print(WiFi.localIP());
  Serial.println("/");
}

void loop ()
{

    //Sensor
    static double oldValueCoracao = 0;
    static double oldChange = 0;
    int rawValueCoracao = analogRead (sensorPin);
    double valueCoracao = alpha * oldValueCoracao + (1 - alpha) * rawValueCoracao;
    
   // Serial.print (rawValueCoracao);
    //Serial.print (",");
    //Serial.println (valueCoracao);
    oldValueCoracao = valueCoracao;
    delay (period);

//Enviando dados
  String url = "/api/v1/data";
  String postData = "temperatura=35,9;coracao=" + String(valueCoracao);

  Serial.println (">>>>>  " + postData + "  <<<<<");

  //Conectando a nosso servidor
  //WiFiClientSecure client;
  WiFiClient client;
  Serial.println("Conexao com o servidor SmartHealth");
  Serial.print("Conectando a ");
  Serial.println(host);
  Serial.println("");
  while (!client.connect(host, httpsPort)) {
    Serial.println("Erro de conexao");
//    return;
}

/*
  if (client.verify(fingerprint, host)) {
    Serial.println("Certificado OK");
  } else {
    Serial.println("Certificado incorreto");
  }
*/

  Serial.println("Conectado ao Servidor!");
  Serial.println("");
  
  Serial.print("Requisitando URL: ");
  Serial.println(url);

  client.print(String("POST ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "User-Agent: Hackathon\r\n" +
               "Content-Type: text/plain\r\n" +
               "Content-Length: " + postData.length() + "\r\n" +
               "Connection: close\r\n\r\n" +
               postData + "\r\n");

  Serial.println("Requisicao enviada");

  Serial.println("Recebendo resposta");
  Serial.println("");

  while (client.connected()) {
    /*
    String line = client.readStringUntil('\n');
    Serial.println(line);
    if (line == "\r") {
      Serial.println("headers received");
      break;
    }
    */
    Serial.write(client.read());
    delay(10);
  }
  Serial.println("");
  String line = client.readStringUntil('\n');
  Serial.println(line);
  Serial.println("Fechando conexao");

  //Conectando ao servidor Konker
  //WiFiClientSecure client;
  WiFiClient client;
  Serial.println("Conexao com o servidor Konker");
  Serial.print("Conectando a ");
  Serial.println(host);
  Serial.println("");
  while (!client.connect(host, httpsPort)) {
    Serial.println("Erro de conexao");
//    return;
}

/*
  if (client.verify(fingerprint, host)) {
    Serial.println("Certificado OK");
  } else {
    Serial.println("Certificado incorreto");
  }
*/

  Serial.println("Conectado ao Servidor!");
  Serial.println("");
  
  Serial.print("Requisitando URL: ");
  Serial.println(url);

  client.print(String("POST ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "User-Agent: Hackathon\r\n" +
               "Content-Type: text/plain\r\n" +
               "Content-Length: " + postData.length() + "\r\n" +
               "Connection: close\r\n\r\n" +
               postData + "\r\n");

  Serial.println("Requisicao enviada");

  Serial.println("Recebendo resposta");
  Serial.println("");

  while (client.connected()) {
    /*
    String line = client.readStringUntil('\n');
    Serial.println(line);
    if (line == "\r") {
      Serial.println("headers received");
      break;
    }
    */
    Serial.write(client.read());
    delay(10);
  }
  Serial.println("");
  String line = client.readStringUntil('\n');
  Serial.println(line);
  Serial.println("Fechando conexao");
}
