namespace SimpleWF
{
    public abstract class WFEntity : IEntity
    {
        public IEntity next { get; private set; }
        public bool finish { get; private set; }
        WorkFlow Flow;

        public WFEntity()
        {
            finish = false;
        }

        public void bindFlow(WorkFlow flow)
        {
            Flow = flow;
        }

        public virtual void stop()
        {
            finish = false;
        }

        public void update()
        {
            execute();
            finish = true;
        }

        public abstract void execute();

        public T Then<T>()
            where T : WFEntity, new()
        {
            T item = Flow.CreatEntity(typeof(T)) as T;
            next = item;
            return item;
        }

        public WFRouter Router()
        {
            WFRouter item = Flow.CreatRouter();
            next = item;
            return item;
        }

        public void reset()
        {
            finish = false;
        }
    }
}
