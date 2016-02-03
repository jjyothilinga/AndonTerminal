
typedef enum
{
	UI_IDLE = 0,
	UI_LOGIN,
	UI_LOGOUT,
	UI_STATION ,
	UI_PRDN_QTY ,
	UI_ISSUE ,
	UI_BRK_QUA_MS,
	UI_PART_NO,
	UI_ISSUE_ACK,
	UI_CLASS,
	UI_CLEAR_ISSUE,
	UI_PASSWORD,
	UI_ADMIN_ACTIVITY,
	UI_ISSUE_RESOLVE
	

}UI_STATE;




	
	
enum
{
	UI_MSG_IDLE = 0,
	UI_MSG_LOGIN_LOGOUT,
	UI_MSG_STATION ,
	UI_MSG_PRDN_QTY,
	UI_MSG_ISSUE,
	UI_MSG_BREAKDOWN,
	UI_MSG_QUALITY,
	UI_MSG_MATERIAL_SHORTAGE,
	UI_MSG_PART_NO,
	UI_MSG_ACKNOWLEDGE,
	UI_MSG_CLASS,
	UI_MSG_CLEAR_ISSUES,
	UI_MSG_PASSWORD,
	UI_MSG_ADMIN_ACTIVITY,
	UI_MSG_OTHERS

};
		

enum
{
	ISSUE_0 = 0,
	ISSUE_1 ,
	ISSUE_2,
	ISSUE_3
};



void UI_init(void);
void UI_task(void);
void UI_setState( UI_STATE state);
