﻿using tvn.cosine.api;
using tvn.cosine.collections;
using tvn.cosine.collections.api;
using tvn.cosine.text;
using tvn.cosine.text.api;
using tvn.cosine.ai.learning.framework;

namespace tvn.cosine.ai.learning.inductive
{
    public class DecisionListTest : IStringable
    {
        // represents a single test in the Decision List
        private IMap<string, string> attrValues;

        public DecisionListTest()
        {
            attrValues = CollectionFactory.CreateInsertionOrderedMap<string, string>();
        }

        public void add(string nta, string ntaValue)
        {
            attrValues.Put(nta, ntaValue);

        }

        public bool matches(Example e)
        {
            foreach (string key in attrValues.GetKeys())
            {
                if (!(attrValues.Get(key).Equals(e.getAttributeValueAsString(key))))
                {
                    return false;
                }
            }
            return true;
            // return e.targetValue().Equals(targetValue);
        }

        public DataSet matchedExamples(DataSet ds)
        {
            DataSet matched = ds.emptyDataSet();
            foreach (Example e in ds.examples)
            {
                if (matches(e))
                {
                    matched.add(e);
                }
            }
            return matched;
        }

        public DataSet unmatchedExamples(DataSet ds)
        {
            DataSet unmatched = ds.emptyDataSet();
            foreach (Example e in ds.examples)
            {
                if (!(matches(e)))
                {
                    unmatched.add(e);
                }
            }
            return unmatched;
        }

        public override string ToString()
        {
            IStringBuilder buf = TextFactory.CreateStringBuilder();
            buf.Append("IF  ");
            foreach (string key in attrValues.GetKeys())
            {
                buf.Append(key + " = ");
                buf.Append(attrValues.Get(key) + " ");
            }
            buf.Append(" DECISION ");
            return buf.ToString();
        }
    } 
}
