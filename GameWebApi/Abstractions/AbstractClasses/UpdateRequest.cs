using GameWebApi.Abstractions.Interfaces;

namespace GameWebApi.Abstractions.AbstractClasses
{
    public abstract class UpdateRequest<T> : Request, IUpdateRequest<T> where T : class
    {
        public abstract Task<T> Update(T obj);

    }

}
