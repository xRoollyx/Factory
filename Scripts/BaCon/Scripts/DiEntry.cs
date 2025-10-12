using System;

namespace myProject.Scripts.BaCon.Scripts
{
    public abstract class DiEntry : IDisposable
    {
        protected DiContainer container { get; }
        protected bool isSingleton { get; set; }

        protected DiEntry() { }
        
        protected DiEntry(DiContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>()
        {
            return ((DiEntry<T>)this).Resolve();
        }

        public DiEntry AsSingle()
        {
            isSingleton = true;

            return this;
        }

        public abstract void Dispose();
    }
    
    public class DiEntry<T> : DiEntry
    {
        private Func<DiContainer, T> factory { get; }
        private T _instance;
        private IDisposable _disposableInstance;
        
        public DiEntry(DiContainer container, Func<DiContainer, T> factory) : base(container)
        {
            this.factory = factory;
        }

        public DiEntry(T value)
        {
            _instance = value;

            if (_instance is IDisposable disposableInstance)
            {
                _disposableInstance = disposableInstance;
            }
            
            isSingleton = true;
        }

        public T Resolve()
        {
            if (isSingleton)
            {
                if (_instance == null)
                {
                    _instance = factory(container);
                    
                    if (_instance is IDisposable disposableInstance)
                    {
                        _disposableInstance = disposableInstance;
                    }
                }

                return _instance;
            }

            return factory(container);
        }

        public override void Dispose()
        {
            _disposableInstance?.Dispose();
        }
    }
}