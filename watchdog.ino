#include "timer-api.h"

#define WATCHDOG_TIMEOUT_SEC 15
#define BOOT_TIME 100
#define DELAY_BEFORE_POWERON 5

#define SHORT_PRESS 10000
#define LONG_PRESS 1000000

#define REBOOT_PIN 12
#define POWER_PIN 8

String inputString = "";
boolean stringComplete = false;
int watchdog_timeout_counter = 0;
int boot_time_counter = 0;
int delay_power_counter = 0;

enum State {PowerOff, Active, Rebooting} state = PowerOff;

void StartTimer()
{
  timer_init_ISR_1Hz(TIMER_DEFAULT);
}

void StopTimer()
{
  timer_stop_ISR(TIMER_DEFAULT);
}

void setup()
{
  StartTimer();
  pinMode(REBOOT_PIN, OUTPUT);
  pinMode(POWER_PIN, OUTPUT);
  pinMode(13, OUTPUT);
  inputString.reserve(200);
  Serial.begin(9600);
  Serial.println("Hello, world!");
}

void loop()
{
  if (stringComplete)
  {
    if (inputString == "who_are_you?\n") Serial.println("watchdog");

    if (inputString == "im_fine\n")
    {
      Serial.println("glad_for_you");
      state = Active;
      watchdog_timeout_counter = 0;
    }


    inputString = "";
    stringComplete = false;
  }
}

void serialEvent()
{
  while (Serial.available())
  {
    char inChar = (char)Serial.read();
    inputString += inChar;
    if (inChar == '\n') stringComplete = true;
  }
}

void Reboot()
{
  digitalWrite(REBOOT_PIN, HIGH);
  StopTimer();
  delay(SHORT_PRESS);
  StartTimer();
  digitalWrite(REBOOT_PIN, LOW);
  state = Rebooting;
}

void Shutdown()
{
  digitalWrite(POWER_PIN, HIGH);
  StopTimer();
  delay(LONG_PRESS);
  StartTimer();
  digitalWrite(POWER_PIN, LOW);
  state = PowerOff;
}

void PowerOn()
{
  digitalWrite(POWER_PIN, HIGH);
  StopTimer();
  delay(SHORT_PRESS);
  StartTimer();
  digitalWrite(POWER_PIN, LOW);
  state = Rebooting;
}

void timer_handle_interrupts(int timer)
{

  switch (state)
  {
    case (Active):
      watchdog_timeout_counter++;
      digitalWrite(13, !digitalRead(13));
      if (watchdog_timeout_counter >= WATCHDOG_TIMEOUT_SEC)
      {
        Reboot();
        watchdog_timeout_counter = 0;
      }
      break;
    case (Rebooting):
      Serial.println("how_are_you?");
      boot_time_counter++;
      if (boot_time_counter >= BOOT_TIME)
      {
        Shutdown();
        boot_time_counter = 0;
      }
      break;
    case (PowerOff):
      Serial.println("how_are_you?");
      delay_power_counter++;
      if (delay_power_counter >= DELAY_BEFORE_POWERON)
      {
        PowerOn();
        delay_power_counter = 0;
      }
      break;
  }

}
