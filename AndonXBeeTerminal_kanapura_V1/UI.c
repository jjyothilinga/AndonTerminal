#include "board.h"
#include "config.h"
#include "Keypad.h"
#include "lcd.h"
#include "string.h"
#include "ui.h"
#include "app.h"
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
		{
		"STATION:",
		"DEPARTMENT:",
		"ISSUE:",
		"PASSWORD:",
		"ADMIN ACTIVITY:",
		"CLEAR ISSUES",

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







const rom UINT8 keyMap[MAX_KEYS ][MAX_CHAR_PER_KEY] = {
													{'1','-','1','-','1'},{'2','A','B','C','2'},{'3','D','E','F','3'},{'\x0B','\x0B','\x0B','\x0B','\x0B'},
													{'4','G','H','I','4'},{'5','J','K','L','5'},{'6','M','N','O','6'},{'\x0C','\x0C','\x0C','\x0C','\x0C'},
													{'7','P','Q','R','S'},{'8','T','U','V','8'},{'9','W','X','Y','Z'},{'\x08','\x08','\x08','\x08','\x08'},
													{'*','*','*','*','*'},{'0',' ','0',' ','0'},{'\x13','\x13','\x13','\x13','\x13'},{'\x0A','\x0A','\x0A','\x0A','\x0A'}
													};




#pragma idata UI_DATA
UI ui = {0,0,{0},0,0xFF,0,0};
OpenIssue openIssue={{0},-1};
OpenIssue ackIssue={{0},-1};
//#pragma idata



UINT8 mapKey(UINT8 scancode, UINT8 duration);
UINT8 getStation(void);
void getData(void);
void clearUIBuffer(void);
void putUImsg(UINT8 msgIndex);
void putUIdept(UINT8 msgIndex);
void setUImsg( UINT8 msgIndex );
void clearUIInput(void);
void showUImsg( UINT8* msg );


void UI_init(void)
{
	UINT8 i, j;
	UINT8 department_index;
	UINT8 department_data;
	ui.departmentNo = '0';

	LCD_setBackSpace('\x08');	//Indicates LCD driver "\x08" is the symbol for backspace

	setUImsg(UI_STATION);

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
}



void UI_task(void)
{

	UINT8 keypressed = 0xFF;
	UINT8 i;
	UINT8 duration, scancode;
	UINT8 uimsg;

	if(KEYPAD_read(&scancode, &duration) == FALSE)			//Check whether key has been pressed
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

		case UI_STATION:
	
			if( keypressed == '\x08')
			{
				if(ui.bufferIndex > 0 )
				{
					LCD_putChar(keypressed);
					ui.bufferIndex--;
					if( ui.inputIndex > 0 )
						ui.inputIndex--;
				}
	
			}
	
			else if( keypressed == '\x0A')
			{
				if(ui.bufferIndex > 0)
				{
					getStation() ;
					setUImsg(UI_MSG_DEPARTMENT);
					clearUIBuffer();
					ui.bufferIndex = 0;
					ui.state = UI_DEPARTMENT;
					
	
				}
			}
	
			else if( keypressed == '\x13')
			{
	
	
				setUImsg(UI_MSG_PASSWORD);
	
				clearUIBuffer();
	
				ui.state = UI_PASSWORD;
					
				
			}
	
			else if( keypressed == '\x0C')
			{
				APP_getAcknowledgedIssue(&ackIssue);
				if(ackIssue.ID != - 1)
				{
					showUImsg(ackIssue.tag);
					clearUIBuffer();
					clearUIInput();
					ui.state= UI_ISSUE_RESOLVE;
				}
			}	
	
			else if( keypressed == '\x0B')
			{
				APP_getOpenIssue(&openIssue);
				if(openIssue.ID != - 1)
				{
					showUImsg(openIssue.tag);
					clearUIBuffer();
					clearUIInput();
					ui.state= UI_ISSUE_ACK;
				}
			}		
			else
			{
				ui.buffer[ui.bufferIndex] = keypressed;
				LCD_putChar(ui.buffer[ui.bufferIndex]);
				ui.bufferIndex++;
			}

		break;



		case UI_DEPARTMENT:

			if( keypressed == '\x08')
			{
				if(ui.bufferIndex > 0)
				{
					setUImsg(UI_MSG_DEPARTMENT);
					ui.inputIndex = 2;
					clearUIBuffer();
					ui.bufferIndex = 0;
					ui.state = UI_DEPARTMENT;

				}
				else
				{
					setUImsg(UI_MSG_STATION);
					clearUIInput();
					clearUIBuffer();
					ui.state = UI_STATION;
				}
	
			}
			else if( keypressed == '\x0A')
			{
				setUImsg(UI_MSG_ISSUE);
				clearUIBuffer();
				ui.state = UI_ISSUE;
	
			}
			else
			{
				for( i = 0 ; i < MAX_DEPARTMENTS ; i++)
				{
					if( i + '0' == keypressed)
					{		
						putUIdept(i);
						ui.input[ui.inputIndex] = keypressed;
						ui.inputIndex++;
						ui.bufferIndex++;
					}
				}
			}
			break;	



		case UI_ISSUE:

		if( keypressed == '\x08')
		{

			if(ui.bufferIndex > 0 )
			{
				LCD_putChar(keypressed);
				ui.bufferIndex--;
				if( ui.inputIndex > 0 )
					ui.inputIndex--;
			}
			else
			{
				setUImsg(UI_MSG_DEPARTMENT);
				ui.inputIndex = 2;
				clearUIBuffer();
				ui.state = UI_DEPARTMENT;
			}
		}

		else if( keypressed == '\x0A')
		{
			if(ui.bufferIndex > 0)
			{
				getData();
				APP_raiseIssues( ui.input);
		
				setUImsg(UI_MSG_STATION);
				clearUIBuffer();
				clearUIInput();
				ui.state = UI_STATION;
				

			}
		}

		else
		{
		
			ui.buffer[ui.bufferIndex] = keypressed;
			LCD_putChar(ui.buffer[ui.bufferIndex]);
			ui.bufferIndex++;
		}

		break;

		case UI_ISSUE_ACK:

		if( keypressed == '\x08')
		{
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;
		}

		else if( keypressed == '\x0B')
		{
			APP_getOpenIssue(&openIssue);
			if( openIssue.ID != -1 )
			{
				showUImsg(openIssue.tag);
			}

		}
		else if( keypressed == '\x0A')
		{
			APP_acknowledgeIssues(openIssue.ID);
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

		else if( keypressed == '\x0C')
		{
			APP_getAcknowledgedIssue(&ackIssue);
			if( ackIssue.ID != -1 )
			{
				showUImsg(ackIssue.tag);
			}

		}
		else if( keypressed == '\x0A')
		{
			APP_resolveIssues(ackIssue.ID);
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
			APP_clearIssues();
			clearUIBuffer();
			clearUIInput();
			setUImsg(UI_MSG_STATION);
			ui.state = UI_STATION;
		}

		break;

		


		case UI_PASSWORD:
		if( keypressed == '\x08')
		{
		
				setUImsg(UI_MSG_STATION);
				clearUIBuffer();
				clearUIInput();
				ui.state = UI_STATION;
		
		

		}

		else if( keypressed == '\x0A')
		{
			BOOL result = FALSE;
			ui.buffer[ui.bufferIndex] = '\0';
	
		
			 
			result = APP_checkPassword(ui.buffer);	
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

		else 
		{
			ui.buffer[ui.bufferIndex] = keypressed;
			LCD_putChar('*');
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

		case UI_STATION:
		keypressed = keyMap[scancode][0];
		
		if( (ui.bufferIndex >=2 ))
		{
		
			if( (keypressed != '\x08') && (keypressed !='\x0A') )
				keypressed = 0xFF;
		}
		else if( ui.bufferIndex > 0 && ui.bufferIndex )
		{
			if( ( keypressed == '*') ||(keypressed =='\x0B')|| (keypressed =='\x0C')     
					|| (keypressed =='\x13'))
				keypressed = 0xFF;
		}
		else 
		{
			if( keypressed == '*') 
				keypressed = 0xFF;
		}

		break;

		case UI_DEPARTMENT:
		keypressed = keyMap[scancode][0];
		
		if( (ui.bufferIndex >=1 ))
		{
		
			if( (keypressed != '\x08') && (keypressed !='\x0A') )
				keypressed = 0xFF;
		}
		else 
		{
			if ( ( keypressed == '*') || (keypressed > ui.departmentNo) ||(keypressed =='\x0B')|| (keypressed =='\x0C')     
					|| (keypressed =='\x13') )
				keypressed = 0xFF;
		}

		break;


		case UI_ISSUE:

		keypressed = keyMap[scancode][0];

		if( (ui.bufferIndex >=3 ))
		{
		
			if( (keypressed != '\x08') && (keypressed !='\x0A') )
				keypressed = 0xFF;
		}

		else 
		{
			if( (keypressed == '\x0B') || (keypressed == '\x0C') || (keypressed == '\x13') || (keypressed == '*') || (keypressed =='\x0A') )
				keypressed = 0xFF;
		}

		break;
	
		case UI_ISSUE_ACK:

		keypressed = keyMap[scancode][0];

		if( (keypressed != '\x0A') && (keypressed != '\x08') && (keypressed != '\x0B') )
		{
			keypressed = 0xFF;
		}

		break;



		case UI_ISSUE_RESOLVE:

		keypressed = keyMap[scancode][0];

		if( (keypressed != '\x0A') && (keypressed != '\x08') && (keypressed != '\x0C') )
		{
			keypressed = 0xFF;
		}

		break;





		case UI_PASSWORD:
			keypressed = keyMap[scancode][0];
		break;



		case UI_CLEAR_ISSUE:
		keypressed = keyMap[scancode][0];

		if( (keypressed != '\x0A') && (keypressed != '\x08') )
		{
			keypressed = 0xFF;
		}

		break;

		case UI_ADMIN_ACTIVITY:
		keypressed = keypressed = keyMap[scancode][0];
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
/*
	switch( state)
	{
		case UI_STATION:
			
			setUImsg(UI_MSG_STATION);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_STATION;

		break;




		case UI_PART_NO:
			
			setUImsg(UI_MSG_PART_NO);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_PART_NO;

		break;
		case UI_ISSUE:
			
			setUImsg(UI_MSG_ISSUE);
			clearUIBuffer();
			clearUIInput();
			ui.state = UI_ISSUE;

		break;




		default:
		break;
	}
*/
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

	
	LCD_clear();

	i = 0;
	while( msg[i] != '\0')
	{
		LCD_putChar(msg[i]);
		i++;
	}
}


void setUImsg( UINT8 msgIndex )
{
	UINT8 i;

	const rom UINT8 *msg;

	msg = UI_MSG[msgIndex] ;

	LCD_clear();

	i = 0;
	while( msg[i] != '\0')
	{
		LCD_putChar(msg[i]);
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
		LCD_putChar(msg[i]);
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
		LCD_putChar(msg[i]);
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




		