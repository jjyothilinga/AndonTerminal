
/*
*------------------------------------------------------------------------------
* board.c
*
* Board Configuration
*
* 
*
*
*------------------------------------------------------------------------------
*/

/*

/*
*------------------------------------------------------------------------------
* Include Files
*------------------------------------------------------------------------------
*/


#include "board.h"
#include "config.h"
#include "typedefs.h"




/*
*------------------------------------------------------------------------------
* Private Defines
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Private Macros
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Private Data Types
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Public Variables
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Private Variables (static)
*------------------------------------------------------------------------------
*/

static BOOL ledState;

/*
*------------------------------------------------------------------------------
* Public Constants
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Private Constants (static)
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Private Function Prototypes (static)
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Public Functions
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* void BRD_init(void)

* Summary	: This function configures all i/o pin directions
*
* Input		: None
*
* Output	: None
*
*------------------------------------------------------------------------------
*/
void BOARD_init(void)
{

	ADCON0 = 0x00;		// set all analog channels as Digital I/O

	ANSELA = 0;
	ANSELB = 0;
	ANSELC = 0;
	ANSELD = 0;
	ANSELE = 0;


//	OSCCON = 0x60;	// SETTING FOR 8MHz
	OSCCON = 0x70;	// SETTING FOR 16MHz
//	OSCTUNEbits.PLLEN = 1;

	
	HEART_BEAT_DIR 	= PORT_OUT;		// Configure heart beat LED output
	HEART_BEAT 		= SWITCH_OFF;
	// Configure Serial port

	#ifdef  ANDONTERMINAL_V0
	TX1_EN_DIR 			= PORT_OUT;
	#endif
	SER1_TX_DIR 		=  PORT_OUT;		
	SER1_RX_DIR 		=  PORT_IN;


	
	// Configure LCD port 
	
	LCD_D7_DIR = PORT_OUT;		
	LCD_D6_DIR = PORT_OUT;
	LCD_D5_DIR = PORT_OUT;
	LCD_D4_DIR = PORT_OUT;
	LCD_E_DIR  = PORT_OUT;
	LCD_RW_DIR = PORT_OUT;
	LCD_RS_DIR = PORT_OUT;

	//KEYPAD PORT

	#ifdef ANDONTERMINAL_V0

		KEYPAD_DEC_INT_DIR = PORT_IN;
		KEYPAD_BCD3_DIR	= PORT_IN;
		KEYPAD_BCD2_DIR	= PORT_IN;
		KEYPAD_BCD1_DIR	= PORT_IN;
		KEYPAD_BCD0_DIR	= PORT_IN;
	#endif
	#ifdef ANDONTERMINAL_V2
	
		KEYPAD_PORT = PORT_IN;
	#endif

	//LAMP CONTROL

		
	BUZZER_DIR 			= PORT_OUT;			// Configure Tower Lamp controls
	BUZZER 				= SWITCH_OFF;

	LAMP_GREEN_DIR			= PORT_OUT;
	LAMP_GREEN 				= SWITCH_OFF;
	
	LAMP_RED_DIR				= PORT_OUT;
	LAMP_RED 				= SWITCH_OFF;

	LAMP_YELLOW_DIR			= PORT_OUT;
	LAMP_YELLOW 				= SWITCH_OFF;
	

	

}

	



/*
*------------------------------------------------------------------------------
* Private Functions
*------------------------------------------------------------------------------
*/

/*
*  End of device.c
*/
