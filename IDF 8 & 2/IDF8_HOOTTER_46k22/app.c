
/* ------------------------------------------------
	header files
------------------------------------------------ */

#include "app.h"
#include "communication.h"
#include "config.h"
#include "board.h"



typedef  struct _APP
{
	UINT8 hooterstate;
	UINT16 hooterCount;

}APP;

#pragma idata APPDATA
APP app ={0};
#pragma idata

/*------------------------------------------------------------
public function
------------------------------------------------------------*/


UINT8 APP_comCallBack( far UINT8 *rxPacket, far UINT8* txCode,far UINT8** txPacket);





void APP_init()
{
	
	COM_init(CMD_SOP , CMD_EOP ,RESP_SOP , RESP_EOP , APP_comCallBack);

}

void APP_task(void)
{

	if(app.hooterstate == 1)
	{
		if(app.hooterCount == 0)
		{
			HOOTER ^= 1;
			app.hooterCount = HOOTER_DUTY_CYCLE;
  		} 
		else
		app.hooterCount --;		
	}
	else
	{
		HOOTER =0;
		app.hooterCount = 0;
	}
}
	
UINT8 APP_comCallBack( far UINT8 *rxPacket, far UINT8* txCode,far UINT8** txPacket)
{

	UINT8 i;

	UINT8 rxCode = rxPacket[0];
	UINT8 length = 0;

	switch(rxCode)
	{
		case CMD_SET_HOOTER:
		
			app.hooterstate =1;
			*txCode = CMD_SET_HOOTER; 
			break;
		
		case CMD_RESET_HOOTER:
		
			app.hooterstate = 0;
			*txCode = CMD_RESET_HOOTER;	
			break;

		default:break;			
		}

	return length;

}
	
		

		
