#include <Ultrasonic.h>
#include <dht11.h>
Ultrasonic ultrasonic(12, 11);
Ultrasonic ultrasonic2(8                   );
dht11 DHT;               // Объявление переменной класса dht11
#define DHT11_PIN 10     // Датчик DHT11 подключен к цифровому пину номер 4
void setup() {
  Serial.begin(19200);
}
void loop()
{
  int chk = DHT.read(DHT11_PIN);
  Serial.print("1_");
  Serial.println(ultrasonic.distanceRead(CM)); 
  delay(1000);
  switch (chk){
  case DHTLIB_OK:  
    break;
  case DHTLIB_ERROR_CHECKSUM:
    Serial.println("Checksum error, \t");
    break;
  case DHTLIB_ERROR_TIMEOUT:
    Serial.println("Time out error, \t");
    break;
  default:
    Serial.println("Unknown error, \t");
    break;
  }

  Serial.print("2_");
  Serial.println(DHT.temperature,1);
  delay(1000);
  Serial.print("3_");
  Serial.println(DHT.humidity, 1);
  delay(1000);
  Serial.print("4_");
  Serial.println(ultrasonic2.distanceRead(CM)); 
  delay(1000);
}
