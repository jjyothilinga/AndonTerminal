#include "device.h"
#include "config.h"
#include "Keypad_driver.h"
#include "lcd_driver.h"
#include "string.h"
#include "ui.h"
#include "ias.h"
#include "eep.h"



typedef struct _UI
{
	UI_STATE state;
	UI_STATE prevState;
	UINT8 buffer[MAX_INPUT_CHARS+1];
	UINT8 bufferIndex;
	UINT8 prevcode;
	UINT8 keyIndex;
	UINT8 input[MAX_INPUT_CHARS+1];
	UINT8 inputIndex;
	UINT8 departmentNo;
}UI;



const rom UINT8 *UI_MSG[]=
		{"    IDEONICS     ANDON TERMINAL",
		"ID:",
		"STATION:",
		"ISSUE:",
		"PART NO:",
		"CLASS:",
		"PASSWORD:",
		"ADMIN ACTIVITY:",
		"CLEAR ISSUES"
		};


const rom UINT8 *UI_CLASS_MSG[]=
		{
		"",
		"MAJOR INJURY",
		"MINOR INJURY",
		"PROPERTY DAMAGE",
		"NEAR MISS"		
		};


UINT8  ui_department[MAX_DEPARTMENTS + 1][16]=
		{"",
		"",
		"",
		"",
		"",
		"",
		"",
		"",
		"",
		"",		
		};




const rom UINT8 keyMap[MAX_KEYS + 1][MAX_CHAR_PER_KEY] = {{0xFF,0xFF,0xFF,0xFF},
													{'1','1','1','1'},{'2','A','B','C'},{'3','D','E','F'},
													{'4','G','H','I'},{'5','J','K','L'},{'6','M','N','O'},
													{'7','P','R','S'},{'8','T','U','V'},{'9','W','X','Y'},

													{'\x08','\x08','\x08','\x08'},{'0','Q','Z','-'},{'\x0A','\x0A','\x0A','\x0A'}
													
													
													
													};




#pragma idata ui_data
UI ui = {0,0,{0},0,0xFF,0,0};
OpenIssue openIssue={{0},-1};
OpenIssue ackIssue={{0},-1};
#pragma idata



UINT8 mapKey(UINT8 scancode, UINT8 duration);
UINT8 getStation(void);
void getData(void);
void clearUIBuffer(void);
void putUImsg(UINT8 msgIndex);
void putUIdept(UINT8 msgIndex);
void putUIclass(UINT8 msgIndex);
void setUImsg( UINT8 msgIndex );
void clearUIInput(void);
void showUImsg( UINT8* msg );


void UI_init(void)
{

	UINT8 i, j;
	UINT8 department_index;
	UINT8 department_data;
	ui.departmentNo = '0';

	setbckspc('\x08');	//Indicates LCD driver "\x08" is the symbol for backspace

	
		

	clearUIBuffer();
	clearUIInput();


//read department data from EPROM and store no of department
	for(i = 0 ; i <= MAX_DEPARTMENTS ;i++)
	{
		department_index = Read_b_eep(DEPARTMENT_START_ADD +(i * 16));
		Busy_eep();
		if(department_index != '\0')
		{
			j = 0;
			department_data = department_index;

			while(department_data != '\0')
			{
				ui_department[i+1][j] = department_data ;
				j++;
				department_data = Read_b_eep((DEPARTMENT_START_ADD +(i * 16))+ j);
				Busy_eep();
			}
			ui_department[i+1][j] = '\0';
			ui.departmentNo ++;
		}
	}

	if (loginStatus() == TRUE)
	{
		setUImsg(UI_MSG_STATION);
		ui.state = UI_STATION;
		rawWriteCommandToLcd(DISPLAY_ON_CUR_BLINK_BIG);
	}
	else
	{
		ui.state = UI_IDLE;
		setUImsg(UI_MSG_IDLE);
		rawWriteCommandToLcd(DISPLAY_ON_CUR_OFF);
	}


}



void UI_task(void)
{

	UINT8 keypressed = 0xFF;
	UINT8 i;
	UINT8 duration, scancode;
	UINT8 uimsg;

 	UINT8 material[] = "MATERIAL";
 	UINT8 safety[] = "SAFETY";

	if(GetDataFromKeypadBuffer(&scancode, &duration) == FALSE)			//Check whether key has been pressed
	{
		return;
	}

	
	keypressed = mapKey(scancode,duration);				//Map the key

	if( keypressed == 0xFF)
	{
		return;
	}


	switch(ui.state)
	{

		case UI_IDLE:
		if( keypressed == '\x0E')
		{
				setUImsg(UI_MSG_LOGIN_LOGOUT);
				clearUIBuffer();
				clearUIInput();
				ui.prevState = ui.state;
				ui.input[ui.inputIndex++]='9';
				ui.input[ui.inputIndex++]='9';
				ui.input[ui.inputIndex++]='0';
				ui.state = UI_LOGIN;
				ui.prevState = UI_IDLE;
				rawWriteCommandToLcd(DISPLAY_ON_CUR_BLINK_BIG);
				
		}
		
		
		break;


		case UI_LOGIN:
		if( keypressed == '\x08')
		{
			if(ui.bufferIndex > 0 )
			{
				putCharToLcd(keypressed);
				ui.bufferIndex--;
				if( ui.inputIndex > 2 )
					ui.inputIndex--;
			}
			else
			{
				if( ui.prevState == UI_IDLE)
				{
					setUImsg(UI_MSG_IDLE);
					clearUIBuffer();
					clearUIInput();
					rawWriteCommandToLcd(DISPLAY_ON_CUR_OFF);
					ui.state = ui.prevState;
				}
			}
		}
		else if( keypressed == '\x0A')
		{
			if(ui.bufferIndex > 0)
			{
				setUImsg(UI_MSG_PASSWORD);
	
				clearUIBuffer();
				ui.input[ui.inputIndex++] = '\0';
				ui.prevState = UI_LOGIN;
				ui.state = UI_PASSWORD;
				
			}
		}

		else
		{
			ui.buffer[ui.bufferIndex] = keypressed;
			putCharToLcd(ui.buffer[ui.bufferIndex]);
			ui.input[ui.inputIndex++]= keypressed;
			ui.bufferIndex++;
		}
		break;
		
		case UI_LOGOUT:
		if( keypressed == '\x08')
		{
			if(ui.bufferIndex > 0 )
			{
				putCharToLcd(keypressed);
				ui.bufferIndex--;
				if( ui.inputIndex > 2 )
					ui.inputIndex--;
			}
			else
			{
				if( ui.prevState == UI_STATION)
				{
					setUImsg(UI_MSG_STATION);
					clearUIBuffer();
					clearUIInput();
					ui.state = ui.prevState;
				}
			}
		}
		else if( keypressed == '\x0A')
		{
			if(ui.bufferIndex > 0)
			{
				setUImsg(UI_MSG_PASSWORD);
	
				clearUIBuffer();
				ui.prevState = UI_LOGOUT;
				ui.state = UI_PASSWORD;
				
			}
		}

		else
		{
			ui.buffer[ui.bufferIndex] = keypressed;
			putCharToLcd(ui.buffer[ui.bufferIndex]);
			ui.input[ui.inputIndex++]= keypressed;
			ui.bufferIndex++;
		}
		break;		

		case UI_STATION:

		if( keypressed == '\x08')
		{
			if(ui.bufferIndex > 0 )
			{
				putCharToLcd(keypressed);
				ui.bufferIndex--;
				if( ui.inputIndex > 0 )
					ui.inputIndex--;
			}
			else 
			{
				setUImsg(UI_MSG_PASSWORD);	
				clearUIBuffer();
				clearUIInput();
				ui.prevState = UI_STATION;
				ui.state = UI_PASSWORD;
			}
		}
		else if( keypressed == '0')
		{
			if(ui.bufferIndex > 0 )
			{
				ui.buffer[ui.bufferIndex] = keypressed;
				putCharToLcd(ui.buffer[ui.bufferIndex]);
				ui.bufferIndex++;
			}
			else 
			{
				IAS_getAcknowledgedIssue(&ackIssue);
				if(ackIssue.ID != - 1)
				{
					showUImsg(ackIssue.tag);
					clearUIBuffer();
					clearUIInput();
					ui.state= UI_ISSUE_RESOLVE;
				}

			}
		}

		else if( keypressed == '\x0D')
		{
				IAS_getOpenIssue(&openIssue);
				if(openIssue.ID != - 1)
				{
					showUImsg(openIssue.tag);
					clearUIBuffer();
					clearUIInput();
					ui.state= UI_ISSUE_ACK;
				}
		}
		else if( keypressed == '\x0E')
		{
			if( ui.bufferIndex == 0 )
			{
				setUImsg(UI_MSG_LOGIN_LOGOUT);
				clearUIBuffer();
				clearUIInput();
				ui.input[ui.inputIndex++]='9';
				ui.input[ui.inputIndex++]='9';
				ui.input[ui.inputIndex++]='0';
				ui.prevState = ui.state;
				ui.state = UI_LOGOUT;
			}
				
		}		
		
		else if( keypressed == '\x0A')
		{
			if(ui.bufferIndex > 0)
			{
				setUImsg(UI_MSG_ISSUE);
				getStation();
				clearUIBuffer();
				ui.state = UI_ISSUE;
			}
		}

		else
		{
			ui.buffer[ui.bufferIndex] = keypressed;
			putCharToLcd(ui.buffer[ui.bufferIndex]);
			ui.bufferIndex++;
		}

		break;

	
		case UI_ISSUE:

		if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;

		}

		else
		{

			for(i = 1 ; i <= MAX_DEPARTMENTS ; i++)
			{
				if( (i + '0') == keypressed)
				{
					putUIdept(i);
		
					ui.input[ui.inputIndex]  = i + '0' ;
					ui.inputIndex++;
		
					ui.state = UI_BRK_QUA_MS;
					break;	
				}				
			}

		}
		break;


		case UI_BRK_QUA_MS:

		if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_ISSUE);
			clearUIBuffer();
			ui.state = UI_ISSUE;
			ui.inputIndex = 2;
		}

		else if( keypressed == '\x0A')
		{
			if( strcmp(ui_department[ui.input[ui.inputIndex - 1] - '0'] , material ) == 0 )
			{

				setUImsg(UI_MSG_PART_NO);
				clearUIBuffer();
				ui.state = UI_PART_NO;

			}
			else if( strcmp(ui_department[ui.input[ui.inputIndex - 1] - '0'] , safety ) == 0 )
			{
				setUImsg(UI_MSG_CLASS);
				clearUIBuffer();
				ui.state = UI_CLASS;

			}

			else
			{

					getData();
					IAS_raiseIssues( ui.input,0);
					setUImsg(UI_MSG_STATION);
					clearUIBuffer();
					clearUIInput();
					ui.state = UI_STATION;
			
			}
		}

		break;

		case UI_CLASS:
		if( keypressed == '\x08')
		{

			setUImsg(UI_MSG_ISSUE);
			clearUIBuffer();
			ui.inputIndex = 2;
			ui.state = UI_ISSUE;
			

		}
		else if( keypressed == '\x0A')
		{
			if( ui.bufferIndex > 0 )
			{
	
				getData();
				IAS_raiseIssues( ui.input , 2);
				setUImsg(UI_MSG_STATION);
				clearUIBuffer();
				clearUIInput();
				ui.state = UI_STATION;
			}
		}
		else
		{
			switch(keypressed)
			{
	
				case '1':
				putUIclass(UI_MSG_MAJOR_INJURY_FATALITY);
	
				ui.input[ui.inputIndex]  = '1';
				ui.inputIndex++;
				ui.bufferIndex ++;

				break;


				case '2':
				putUIclass(UI_MSG_MINOR_INJURY);
	
				ui.input[ui.inputIndex]  = '2';
				ui.inputIndex++;
				ui.bufferIndex ++;

				break;


				case '3':
				putUIclass(UI_MSG_PROPERTY_DAMAGE);
	
				ui.input[ui.inputIndex]  = '3';
				ui.inputIndex++;
				ui.bufferIndex ++;
	
				break;

				case '4':
				putUIclass(UI_MSG_NEAR_MISS);
	
				ui.input[ui.inputIndex]  = '4';
				ui.inputIndex++;
				ui.bufferIndex ++;
	
				break;

				
			}

		}

		break;



		case UI_PART_NO:
		if( keypressed == '\x08')
		{
			if(ui.bufferIndex > 0 )
			{
				putCharToLcd(keypressed);
				ui.bufferIndex--;

			}
			else
			{
				setUImsg(UI_MSG_ISSUE);
				clearUIBuffer();
				ui.state = UI_ISSUE;
				ui.inputIndex = 2;
			}

		}
		else if( keypressed == '\x0A')
		{
			if( ui.bufferIndex > 0 )
			{
				getData();
				IAS_raiseIssues( ui.input , 1);
				setUImsg(UI_MSG_STATION);
				clearUIBuffer();
				clearUIInput();
				ui.state = UI_STATION;
			}
		}

		else
		{
			if(scancode == ui.prevcode)
			{
				if(duration < MIN_KEYPRESS_DURATION)
				{
					ui.bufferIndex--;
					ui.buffer[ui.bufferIndex] = keypressed;
					putCharToLcd('\x08');
					putCharToLcd(ui.buffer[ui.bufferIndex]);
					DelayMs(20);
					ui.bufferIndex++;
				}
				else
				{
					ui.buffer[ui.bufferIndex] = keypressed;
					putCharToLcd(ui.buffer[ui.bufferIndex]);
					DelayMs(20);
					ui.bufferIndex++;
				}
			}
			else
			{
				ui.buffer[ui.bufferIndex] = keypressed;
				putCharToLcd(ui.buffer[ui.bufferIndex]);
				DelayMs(20);
				ui.bufferIndex++;
			}
			ui.prevcode = scancode;
		}
		break;

		case UI_ISSUE_ACK:

		if( keypressed == '\x0D')
		{
			IAS_getOpenIssue(&openIssue);
			if( openIssue.ID != -1 )
			{
				showUImsg(openIssue.tag);
			}
		}

		else if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;
		}

		else if( keypressed == '\x0A')
		{
			IAS_acknowledgeIssues(openIssue.ID);
			openIssue.ID = -1;
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;

		}

		break;




		case UI_ISSUE_RESOLVE:

		if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;
		}

		else if( keypressed == '0')
		{
			IAS_getAcknowledgedIssue(&ackIssue);
			if( ackIssue.ID != -1 )
			{
				showUImsg(ackIssue.tag);
			}
		}

		else if( keypressed == '\x0A')
		{
			IAS_resolveIssues(ackIssue.ID);   // 	IAS_resolveIssues( &ackIssue.ID); 
			ackIssue.ID = -1;
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;

		}

		break;




		case UI_CLEAR_ISSUE:

		if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			ui.state = UI_STATION;

		}

		else if( keypressed == '\x0A')
		{
			IAS_clearIssues();
			clearUIBuffer();
			clearUIInput();
			setUImsg(UI_MSG_STATION);
			ui.state = UI_STATION;
		}

		break;

		case UI_PASSWORD:

		if( keypressed == '\x08')
		{
			if( ui.prevState == UI_LOGIN)
			{
				setUImsg(UI_MSG_IDLE);
				clearUIBuffer();
				clearUIInput();
				ui.state = UI_IDLE;
				rawWriteCommandToLcd(DISPLAY_ON_CUR_OFF);
			}
			else if( ui.prevState == UI_LOGOUT || ui.prevState == UI_STATION)
			{
				setUImsg(UI_MSG_STATION);
				clearUIBuffer();
				clearUIInput();
				ui.state = UI_STATION;
			}

		}

		else if( keypressed == '\x0A')
		{
			ui.buffer[ui.bufferIndex] = '\0';
			if( ui.prevState == UI_LOGIN)
			{
				BOOL result = FALSE; 
				result = IAS_login(ui.buffer,ui.input);	
				if( result == TRUE )
				{
					setUImsg(UI_MSG_STATION);
					clearUIBuffer();
					clearUIInput();
					rawWriteCommandToLcd(DISPLAY_ON_CUR_BLINK_BIG);
					ui.state = UI_STATION;

				}
				else
				{
					setUImsg(UI_MSG_IDLE);
					clearUIBuffer();
					clearUIInput();
					ui.state = UI_IDLE;

				}

			}
			else if( ui.prevState == UI_LOGOUT)
			{
				BOOL result = FALSE; 
				result = IAS_logout(ui.buffer,ui.input);	
				if( result == TRUE )
				{
					setUImsg(UI_MSG_IDLE);
					clearUIBuffer();
					clearUIInput();
					rawWriteCommandToLcd(DISPLAY_ON_CUR_OFF);
					ui.state = UI_IDLE;

				}
				else
				{
					setUImsg(UI_MSG_STATION);
					clearUIBuffer();
					clearUIInput();
					ui.state = UI_STATION;

				}		
				
			}
			else if( ui.prevState == UI_STATION )
			{
				BOOL result = FALSE; 
				result = IAS_checkPassword(ui.buffer);	
				if( result == TRUE )
				{
					setUImsg(UI_MSG_ADMIN_ACTIVITY);
					clearUIBuffer();
					clearUIInput();
					ui.state = UI_ADMIN_ACTIVITY;
				}
				else
				{
					setUImsg(UI_MSG_STATION);
					clearUIBuffer();
					clearUIInput();
					ui.state = UI_STATION;

				}
			}	
		}

		else 
		{
			ui.buffer[ui.bufferIndex] = keypressed;
			putCharToLcd('*');
			ui.bufferIndex++;
		}
			
		break;		

		case UI_ADMIN_ACTIVITY:

		if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;
			
		}


		else if( keypressed == '0')
		{
			setUImsg(UI_MSG_CLEAR_ISSUES);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_CLEAR_ISSUE;
		}


		break;
	
		default:
		break;


	}

}


UINT8 mapKey(UINT8 scancode, UINT8 duration)
{
	UINT8 keypressed = 0xFF;
	switch(ui.state)
	{
		case UI_IDLE:
		if(scancode == 0x0E)
		{
			keypressed = '\x0E';
		}
			 
		break;

		case UI_LOGIN:

		if(scancode == ui.prevcode)
		{
			if(duration < MIN_KEYPRESS_DURATION )
			{
				ui.keyIndex++;
				if(ui.keyIndex == 4)
					ui.keyIndex = 0;
			}
			else
			{
				ui.keyIndex = 0;
			}
		}
		else
		{
			ui.keyIndex = 0;

		}

		keypressed = keyMap[scancode][ui.keyIndex];

		if( scancode == 0x0E || scancode == 0x0D)
			keypressed = 0xFF;

		break;



		case UI_LOGOUT:

		if(scancode == ui.prevcode)
		{
			if(duration < MIN_KEYPRESS_DURATION )
			{
				ui.keyIndex++;
				if(ui.keyIndex == 4)
					ui.keyIndex = 0;
			}
			else
			{
				ui.keyIndex = 0;
			}
		}
		else
		{
			ui.keyIndex = 0;

		}

		keypressed = keyMap[scancode][ui.keyIndex];

		if( scancode == 0x0E || scancode == 0x0D)
			keypressed = 0xFF;

		break;

		case UI_STATION:
	
		if (scancode == 0x0D)
		{
			keypressed = '\x0D';
		}
		else if(scancode == 0x0E)
		{
			keypressed = '\x0E';
		} 
		else
		{
			keypressed = keyMap[scancode][0];
			if( (ui.bufferIndex >=2 ) && (keypressed != '\x08') && (keypressed !='\x0A'))
				keypressed = 0xFF;
		}


		break;


		case UI_ISSUE:

		keypressed = keyMap[scancode][0];

		if((keypressed > ui.departmentNo)  && 
			(keypressed != '\x08') && 
			(keypressed !='\x0A')    ) 
			keypressed = 0xFF;




		break;

		case UI_BRK_QUA_MS:

		keypressed = keyMap[scancode][0];

		if( (keypressed != '\x0A') && (keypressed != '\x08') )
		{
			keypressed = 0xFF;
		}

		break;



		case UI_PART_NO:

		if(scancode == ui.prevcode)
		{
			if(duration < MIN_KEYPRESS_DURATION )
			{
				ui.keyIndex++;
				if(ui.keyIndex == 4)
					ui.keyIndex = 0;
			}
			else
			{
				ui.keyIndex = 0;
			}
		}
		else
		{
			ui.keyIndex = 0;

		}

		keypressed = keyMap[scancode][ui.keyIndex];
		if( keypressed == '\x0C' || keypressed == '\x0B' )
			keypressed = 0xFF;

		break;

		case UI_ISSUE_ACK:

		if (scancode == 0x0D)
		{
			keypressed = '\x0D';
		}

		else
		{
			keypressed = keyMap[scancode][0];
	
			if( (keypressed != '\x0A') && (keypressed != '\x08') )
					
			{
				keypressed = 0xFF;
			}
		}



		break;


		case UI_ISSUE_RESOLVE:

	
		keypressed = keyMap[scancode][0];

		if( (keypressed != '\x0A') && (keypressed != '\x08') &&  (keypressed != '0') ) // (keypressed == '\x08') 
			{
					keypressed = 0xFF;
		
			}
		break;


		case UI_CLASS:

		keypressed = keyMap[scancode][0];
		
		if( (ui.bufferIndex >=1 ))
		{
		
			if( (keypressed != '\x08') && (keypressed !='\x0A') )
				keypressed = 0xFF;
		}
		else
		{
			if( (keypressed != '\x08') && (keypressed !='\x0A') && ( (keypressed != '1') && (keypressed !='2') )
						&&( (keypressed != '3') && (keypressed !='4') ) )
				keypressed = 0xFF;
		}

		break;

		case UI_PASSWORD:
			keypressed = keyMap[scancode][0];
		break;



		case UI_CLEAR_ISSUE:

		keypressed = keyMap[scancode][0];

		if( scancode == 0x0E || scancode == 0x0D)
			keypressed = 0xFF;

		if( (keypressed != '\x0A') && (keypressed != '\x08') )
		{
			keypressed = 0xFF;
		}

		break;

		case UI_ADMIN_ACTIVITY:
		keypressed = keypressed = keyMap[scancode][0];

		if( scancode == 0x0E || scancode == 0x0D)
			keypressed = 0xFF;

		if( (keypressed != '0') && (keypressed !='\x08') && (keypressed != '\x0A') )
			keypressed = 0xFF;
		break;



		default:
		break;

	}

	return keypressed;
}

void UI_setState( UI_STATE state)
{
	switch( state)
	{
		case UI_STATION:
			
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;

		break;

		default:
		break;
	}
}



UINT8 getStation(void)
{
	UINT8 i,station = 0;

	if( ui.bufferIndex == 1 )
	{
		ui.input[ui.inputIndex] = '0';
		ui.inputIndex++;
		ui.input[ui.inputIndex] = ui.buffer[0];
		ui.inputIndex++;
	}

	else
	{
		ui.input[ui.inputIndex] = ui.buffer[0];
		ui.inputIndex++;
		ui.input[ui.inputIndex] = ui.buffer[1];
		ui.inputIndex++;
	}

	station = (ui.input[0]-'0')*10 + (ui.input[1]-'0');

	return station;
}


void getData(void)
{
	UINT8 i;

	for( i = 0; i< ui.bufferIndex; i++)
	{
		ui.input[ui.inputIndex] = ui.buffer[i];
		ui.inputIndex++;
		
	}
	ui.input[ui.inputIndex] = '\0';
	ui.inputIndex++;

	if( ui.inputIndex >= MAX_INPUT_CHARS )
		ui.inputIndex = 0;
}


void clearUIBuffer(void)
{
	memset(ui.buffer,0, MAX_INPUT_CHARS);
	ui.bufferIndex = 0;
	ui.keyIndex = 0;
	ui.prevcode = 0xFF;

}


void clearUIInput(void)
{
	memset((UINT8*)ui.input,0, MAX_INPUT_CHARS);
	ui.inputIndex = 0;
}




void showUImsg( UINT8* msg )
{
	UINT8 i;

	
	clearLCD();

	i = 0;
	while( msg[i] != '\0')
	{
		putCharToLcd(msg[i]);
		i++;
	}
}


void setUImsg( UINT8 msgIndex )
{
	UINT8 i;
	UINT8 msg[32];

	clearLCD();
	strcpypgm2ram(msg,UI_MSG[msgIndex]);

	i = 0;
	while( msg[i] != '\0')
	{
		putCharToLcd(msg[i]);
		i++;
	}

}


void putUImsg(UINT8 msgIndex)
{
	UINT8 i;

	const rom UINT8 *msg;

	msg = UI_MSG[msgIndex] ;

	i = 0;
	while( msg[i] != '\0')
	{
		putCharToLcd(msg[i]);
		i++;
	}
}

/*--------------------------------------
display the department name
-----------------------------------------*/
void putUIdept(UINT8 msgIndex)
{
	UINT8 i;

	UINT8 *msg;

	msg = ui_department[msgIndex] ;

	i = 0;
	while( msg[i] != '\0')
	{
		putCharToLcd(msg[i]);
		i++;
	}
}
/*--------------------------------------
display the class name of SAFTEY
-----------------------------------------*/
void putUIclass(UINT8 msgIndex)
{
	UINT8 i;

	const rom UINT8 *msg_class;

	msg_class = UI_CLASS_MSG[msgIndex] ;

	i = 0;
	while( msg_class[i] != '\0')
	{
		putCharToLcd(msg_class[i]);
		i++;
	}
}


void UI_updateDepartment(UINT8 *buffer)
{
UINT8 i ,j ;
UINT8 department_data;
UINT8 department_index = (buffer[0] - '0') - 1 ;
i = j =0;

	while(buffer[i+1] != '\0')
	{
		Write_b_eep((DEPARTMENT_START_ADD +( (department_index * 16))+ i) ,buffer[i+1] );
		Busy_eep();
		i++;
	}
	Write_b_eep((DEPARTMENT_START_ADD +(department_index * 16))+ i , '\0');
	Busy_eep();

	department_data = Read_b_eep(DEPARTMENT_START_ADD +(department_index * 16));
	Busy_eep();

	while(department_data != '\0')
	{
		ui_department[department_index +1][j] = department_data ;
		j++;
		department_data = Read_b_eep((DEPARTMENT_START_ADD +(department_index * 16))+ j);
		Busy_eep();
	}
	ui_department[department_index +1][j] = '\0';



}
		