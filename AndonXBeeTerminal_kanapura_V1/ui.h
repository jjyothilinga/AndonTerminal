

typedef enum
{
	UI_STATION = 0,
	UI_DEPARTMENT,
	UI_ISSUE ,
	UI_ISSUE_ACK,
	UI_CLEAR_ISSUE,
	UI_PASSWORD,
	UI_ADMIN_ACTIVITY,
	UI_BRK_QUA_MS,
	UI_ISSUE_RESOLVE
	

}UI_STATE;

enum
{
	UI_MSG_ISSUE_1 = 1,
	UI_MSG_ISSUE_2,
	UI_MSG_ISSUE_3,
	UI_MSG_ISSUE_4,
	UI_MSG_ISSUE_5,
	UI_MSG_ISSUE_6,
	UI_MSG_ISSUE_7,
	UI_MSG_ISSUE_8,
	UI_MSG_ISSUE_9
};


	
	
enum
{
	UI_MSG_STATION = 0,
	UI_MSG_DEPARTMENT,
	UI_MSG_ISSUE,
	UI_MSG_PASSWORD,
	UI_MSG_ADMIN_ACTIVITY,
	UI_MSG_CLEAR_ISSUES

	
};
		

enum
{
	ISSUE_0 = 0,
	ISSUE_1 ,
	ISSUE_2,
	ISSUE_3
};

extern const rom UINT8 *UI_MSG[];
extern UINT8  ui_department[][16];

void UI_init(void);
void UI_task(void);
void UI_setState( UI_STATE state);
void UI_updateDepartment(UINT8 *buffer);
