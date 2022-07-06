﻿namespace Abstractions
{
    public interface IStorage
    {
        T GetRepository<T>() where T : IRepository;

        void Save();
    }
}
