#ifndef CONFIG_H
#define CONFIG_H

/*
*------------------------------------------------------------------------------
* config.h
*
*/


#define DEVICE_ADDRESS			(0x04)

//#define __FACTORY_CONFIGURATION__



//COM module configuration
#define __BCC_XOR__
#define __RESPONSE_ENABLED__
//#define __LOOP_BACK__
#define BROADCAST_ADDRESS		0xFA
#define CMD_SOP	0xAA
#define CMD_EOP 0xBB
#define RESP_SOP	0xCC
#define RESP_EOP	0xDD


enum
{
	RX_PACKET_SIZE = 64,
	TX_PACKET_SIZE = 64
};

#define 	RECEIVER_MAX_PAKET_SIZE		(RX_PACKET_SIZE)	
#define 	TRANSMITTER_MAX_PAKET_SIZE	(TX_PACKET_SIZE)



enum
{
	MAX_INPUT_CHARS = 31,
	MAX_KEYS = 12,
	MAX_CHAR_PER_KEY = 4,
	MIN_KEYPRESS_DURATION = 40 

	
};


//#define _LCD_TEST_

// Enable for external eeprom access
// Comment out this line if internal eeprom access required
//#define EEP_EXTERRNAL


/*
*------------------------------------------------------------------------------
* Public Data Types
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Public Variables (extern)
*------------------------------------------------------------------------------
*/

/*
*------------------------------------------------------------------------------
* Public Constants (extern)
*------------------------------------------------------------------------------
*/


/*
*------------------------------------------------------------------------------
* Public Function Prototypes (extern)
*------------------------------------------------------------------------------
*/

#endif
/*
*  End of config.h
*/



