using System;
using System.Collections.Generic;

namespace SimpleWF
{
    public class WFRouter : IEntity
    {
        public IEntity next { get; private set; }
        public bool finish { get; private set; }
        List<WFEntity> nexts = new List<WFEntity>();
        List<Func<bool>> conditions = new List<Func<bool>>();
        WorkFlow Flow;

        public WFRouter()
        {
            finish = false;
        }

        public void bindFlow(WorkFlow flow)
        {
            Flow = flow;
        }

        public void stop()
        {
            finish = false;
        }

        public void update()
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i].Invoke())
                {
                    finish = true;
                    next = nexts[i];
                }
            }
        }

        public T IfThen<T>(Func<bool> func)
            where T : WFEntity, new()
        {
            T item = Flow.CreatEntity(typeof(T)) as T;
            nexts.Add(item);
            conditions.Add(func);
            return item;
        }

        public void reset()
        {
            finish = false;
        }
    }
}
