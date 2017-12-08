using System;
using IMR;

namespace SimpleWF
{
    public class WorkFlowRender : DataRender<WorkFlowModel>
    {
        public override void cmdupdate()
        {
            base.cmdupdate();

            model.tmpCmd = nextCmd();
            if (model.tmpCmd.Cmd == WorkFlowModel.CMD_NAME)
            {
                model.Name = model.tmpCmd.Params[0].ToString();
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_STARTWITH)
            {
                model.startItem = CreatEntity((Type)model.tmpCmd.Params[0], (WorkFlow)model.tmpCmd.Params[1]) as WFEntity;
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_CREATENTITY)
            {
                model.entity = CreatEntity((Type)model.tmpCmd.Params[0], (WorkFlow)model.tmpCmd.Params[1]);
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_CREATROUTER)
            {
                CreatRouter((WorkFlow)model.tmpCmd.Params[0]);
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_REGISTER)
            {
                model.finishAction += model.tmpCmd.Params[0] as Action<bool>;
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_UNREGISTER)
            {
                model.finishAction -= model.tmpCmd.Params[0] as Action<bool>;
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_START)
            {
                start();
            }
            else if (model.tmpCmd.Cmd == WorkFlowModel.CMD_STOP)
            {
                stop();
            }
        }

        void start()
        {
            stop();
            model.switchFlag = true;
        }

        void stop()
        {
            for (int i = 0; i < model.allsteps.Count; i++)
            {
                model.allsteps[i].stop();
            }
            model.curItem = model.startItem;
            model.switchFlag = false;
        }

        WFEntity CreatEntity(Type type, WorkFlow flow)
        {
            if (model.entities.ContainsKey(type.Name))
            {
                return model.entities[type.Name];
            }

            WFEntity entity = Activator.CreateInstance(type) as WFEntity;
            entity.bindFlow(flow);
            model.entities.Add(type.Name, entity);
            model.allsteps.Add(entity);
            return entity;
        }

        void CreatRouter(WorkFlow flow)
        {
            model.router = new WFRouter();
            model.router.bindFlow(flow);
            model.allsteps.Add(model.router);
        }

        void notify(bool success)
        {
            stop();
            if (model.finishAction != null)
            {
                model.finishAction.Invoke(success);
            }
        }

        public override void update()
        {
            base.update();

            if (!model.switchFlag)
                return;

            if (model.curItem == null)
                return;

            if (!model.curItem.finish)
            {
                try
                {
                    model.curItem.update();
                }
                catch (Exception e)
                {
                    notify(false);
                }
            }
            else
            {
                model.curItem = model.curItem.next;
                if (model.curItem == null)
                {
                    notify(true);
                }
                else
                {
                    model.curItem.reset();
                }
            }
        }
    }
}
