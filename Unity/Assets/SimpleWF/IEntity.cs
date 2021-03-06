﻿namespace SimpleWF
{
    public interface IEntity
    {
        IEntity next { get; }
        bool finish { get; }
        void reset();
        void update();
        void stop();
        void bindFlow(WorkFlow flow);
    }
}