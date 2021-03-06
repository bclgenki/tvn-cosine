﻿using tvn.cosine.ai.learning.framework.api;

namespace tvn.cosine.ai.learning.framework
{ 
    public class StringAttribute : IAttribute
    {
        private StringAttributeSpecification spec;

        private string value;

        public StringAttribute(string value, StringAttributeSpecification spec)
        {
            this.spec = spec;
            this.value = value;
        }

        public string ValueAsString()
        {
            return value.Trim();
        }

        public string Name()
        {
            return spec.GetAttributeName().Trim();
        }

        public override string ToString()
        {
            return value.Trim();
        }
    }
}
