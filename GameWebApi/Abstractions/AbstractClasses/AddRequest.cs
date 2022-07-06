using GameWebApi.Abstractions.Interfaces;

namespace GameWebApi.Abstractions.AbstractClasses
{
    public abstract class AddRequest<T> : Request where T : class
    {
        public abstract T GetNewObject();
    }
}
