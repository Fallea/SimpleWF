using UnityEngine;
using SimpleWF;

public class Demo : MonoBehaviour
{
    public static bool allow4 = false;
    public static int N = 0;
    void Start()
    {
        WorkFlow flow = new WorkFlow("test");
        WFRouter router = flow.StartWith<Step1>().Router();
        router.IfThen<Step1>(() =>
        {
            return N <= 5;
        });

        WFRouter router2 = router.IfThen<Step2>(() =>
          {
              return N > 5;
          }).Router();

        router2.IfThen<Step2>(() =>
        {
            return N <= 20;
        });

        router2.IfThen<Step3>(() =>
        {
            return N > 20;
        });

        flow.Register(end);
        flow.Run();
    }

    void end(bool success)
    {
        Debug.Log("ooooover=" + success);
    }
}

public class Step1 : WFEntity
{
    public override void execute()
    {
        Demo.N += 1;
        Debug.Log("Step 1");
    }

    public override void stop()
    {
        base.stop();
    }
}

public class Step2 : WFEntity
{
    public override void execute()
    {
        Demo.N += 2;
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
