# SimpleWF
Unity简单的workflow

WorkFlow 工作流对象
WFRouter 路由 根据条件指向下一个WFEntity
WFEntity 实体 执行具体操作 基类



样例：
        WorkFlow wf = new WorkFlow("工作流1");//实例化工作流1
        wf.StartWith<Step1>()//以Step1为起点
            .Then<Step2>()//然后执行Step2
            .Router()//增加路由
            .IfThen<Step3>(() => { return N == 1; });//如果N==1 执行Step3
        wf.Run();//运行工作流
        
        public class Step1 : WFEntity
        {
            public override void execute()
            {
                Debug.Log("Step 1");
            }
        }

        public class Step2 : WFEntity
        {
            public override void execute()
            {
                Demo.N += 1;
                Debug.Log("Step 2");
            }
        }

        public class Step3 : WFEntity
        {
            public override void execute()
            {
                Debug.Log("Step 3");
            }
        }
