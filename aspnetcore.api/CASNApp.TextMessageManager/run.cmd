set TxtMsgMgrJobName=%WEBJOBS_NAME%
if "%TxtMsgMgrJobName%"=="" set TxtMsgMgrJobName=%1
CASNApp.TextMessageManager.exe %TxtMsgMgrJobName%