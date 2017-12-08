using System;
using System.Collections.Generic;
using IMR;

namespace SimpleWF
{
    public class WorkFlowModel : DataModel
    {
        public const string CMD_NAME = "cmd_name";
        public const string CMD_STARTWITH = "cmd_startwith";
        public const string CMD_CREATENTITY = "cmd_createntity";
        public const string CMD_CREATROUTER = "cmd_creatrouter";
        public const string CMD_START = "cmd_start";
        public const string CMD_STOP = "cmd_stop";
        public const string CMD_REGISTER = "cmd_register";
        public const string CMD_UNREGISTER = "cmd_unregister";

        public Dictionary<string, WFEntity> entities = new Dictionary<string, WFEntity>();
        public List<IEntity> allsteps = new List<IEntity>();
        public WFRouter router;
        public WFEntity entity;
        public WFEntity startItem;
        public IEntity curItem;
        public DataCmd tmpCmd;
        public string Name;
        public Action<bool> finishAction;

        public bool switchFlag = false;
    }
}
