using System;
using IMR;

namespace SimpleWF
{
    public class WorkFlow : Interaction<WorkFlowModel, WorkFlowRender>
    {
        public string name
        {
            get
            {
                return model.Name;
            }
        }

        public bool running
        {
            get
            {
                return model.switchFlag;
            }
        }

        public WorkFlow(string _name)
        {
            sendCmd(WorkFlowModel.CMD_NAME, _name);
        }

        public T StartWith<T>() where T : WFEntity, new()
        {
            sendCmd(WorkFlowModel.CMD_STARTWITH, typeof(T), this);
            return model.startItem as T;
        }

        public WFEntity CreatEntity(Type type)
        {
            sendCmd(WorkFlowModel.CMD_CREATENTITY, type, this);
            return model.entity;
        }

        public WFRouter CreatRouter()
        {
            sendCmd(WorkFlowModel.CMD_CREATROUTER, this);
            return model.router;
        }

        public void Run()
        {
            sendCmd(WorkFlowModel.CMD_START);
        }

        public void Stop()
        {
            sendCmd(WorkFlowModel.CMD_STOP);
        }

        public void Register(Action<bool> action)
        {
            sendCmd(WorkFlowModel.CMD_REGISTER, action);
        }

        public void UnRegister(Action<bool> action)
        {
            sendCmd(WorkFlowModel.CMD_UNREGISTER, action);
        }
    }
}
