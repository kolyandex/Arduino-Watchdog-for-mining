#include "timer-api.h"
//All constants are seconds
#define WATCHDOG_TIMEOUT_SEC 15
#define BOOT_TIME 120
#define DELAY_BEFORE_POWERON 10

#define SHORT_PRESS 1
#define LONG_PRESS 5

#define REBOOT_PIN 12
#define POWER_PIN 8

String inputString = "";
boolean stringComplete = false;
int timer_counter = 0;


enum State {Off, Active, Booting, Pwr_btn, Rst_btn, Pwrbtn_hold} state = Off;

void setup()
{
  timer_init_ISR_1Hz(TIMER_DEFAULT);
  pinMode(REBOOT_PIN, OUTPUT);
  pinMode(POWER_PIN, OUTPUT);
  pinMode(13, OUTPUT);
  inputString.reserve(200);
  Serial.begin(9600);
}

void loop()
{
  if (stringComplete)
  {
    if (inputString == "who_are_you?\n") Serial.println("watchdog");

    if (inputString == "im_fine\n" && (state == Booting || state == Active))
    {
      Serial.println("glad_for_you");
      state = Active;
      timer_counter = 0;
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
  state = Rst_btn;
}

void Shutdown()
{
  digitalWrite(POWER_PIN, HIGH);
  state = Pwrbtn_hold;
}

void PowerOn()
{
  digitalWrite(POWER_PIN, HIGH);
  state = Pwr_btn;
}

void timer_handle_interrupts(int timer)
{
  timer_counter++;
  switch (state)
  {
    case (Active):
      digitalWrite(13, !digitalRead(13));
      if (timer_counter >= WATCHDOG_TIMEOUT_SEC)
      {
        timer_counter = 0;
        Reboot();
      }
      break;
    case (Booting):
      Serial.println("how_are_you?");
      if (timer_counter >= BOOT_TIME)
      {
        timer_counter = 0;
        Shutdown();
      }
      break;
    case (Off):
      if (timer_counter >= DELAY_BEFORE_POWERON)
      {
        timer_counter = 0;
        PowerOn();
      }
      break;
    case (Pwr_btn):
      if (timer_counter >= SHORT_PRESS)
      {
        timer_counter = 0;
        digitalWrite(POWER_PIN, LOW);
        state = Booting;
      }
      break;
    case (Rst_btn):
      if (timer_counter >= SHORT_PRESS)
      {
        timer_counter = 0;
        digitalWrite(REBOOT_PIN, LOW);
        state = Booting;
      }
      break;
    case (Pwrbtn_hold):
      if (timer_counter >= LONG_PRESS)
      {
        timer_counter = 0;
        digitalWrite(POWER_PIN, LOW);
        state = Off;
      }
      break;
  }

}
