﻿using tvn.cosine.collections;
using tvn.cosine.collections.api;
using tvn.cosine.ai.probability.api;
using tvn.cosine.ai.probability.proposition.api;

namespace tvn.cosine.ai.probability.proposition
{
    public abstract class AbstractProposition : IProposition
    {
        private ISet<IRandomVariable> scope = CollectionFactory.CreateSet<IRandomVariable>();
        private ISet<IRandomVariable> unboundScope = CollectionFactory.CreateSet<IRandomVariable>();

        public AbstractProposition()
        { }

        public virtual ISet<IRandomVariable> getScope()
        {
            return scope;
        }

        public virtual ISet<IRandomVariable> getUnboundScope()
        {
            return unboundScope;
        }

        public abstract bool holds(IMap<IRandomVariable, object> possibleWorld);

        protected virtual void addScope(IRandomVariable var)
        {
            scope.Add(var);
        }

        protected virtual void addScope(ICollection<IRandomVariable> vars)
        {
            scope.AddAll(vars);
        }

        protected virtual void addUnboundScope(IRandomVariable var)
        {
            unboundScope.Add(var);
        }

        protected virtual void addUnboundScope(ICollection<IRandomVariable> vars)
        {
            unboundScope.AddAll(vars);
        }
    } 
}
