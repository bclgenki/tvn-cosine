﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using tvn.cosine.ai.learning.framework;
using tvn.cosine.ai.logic.fol.domain;

namespace tvn.cosine.ai.learning.knowledge
{
    /**
     * @author Ciaran O'Reilly
     * 
     */
    public class FOLDataSetDomain : FOLDomain
    {
        //
        private static readonly Regex allowableCharactersRegEx = new Regex("[^a-zA-Z_$0-9]");
        //
        private DataSetSpecification dataSetSpecification;
        private string trueGoalValue = null;
        // Default example prefix, see pg679 of AIMA
        private string examplePrefix = "X";
        private IList<string> descriptionPredicateNames = new List<string>();
        private IList<string> descriptionDataSetNames = new List<string>();
        private IDictionary<string, string> dsToFOLNameMap = new Dictionary<string, string>();

        //
        // PUBLIC METHODS
        //
        public FOLDataSetDomain(DataSetSpecification dataSetSpecification, string trueGoalValue)
        {
            this.dataSetSpecification = dataSetSpecification;
            this.trueGoalValue = trueGoalValue;
            constructFOLDomain();
        }

        public string getDataSetTargetName()
        {
            return dataSetSpecification.getTarget();
        }

        public string getGoalPredicateName()
        {
            return getFOLName(dataSetSpecification.getTarget());
        }

        public string getTrueGoalValue()
        {
            return trueGoalValue;
        }

        public IList<string> getDescriptionPredicateNames()
        {
            return descriptionPredicateNames;
        }

        public IList<string> getDescriptionDataSetNames()
        {
            return descriptionDataSetNames;
        }

        public bool isMultivalued(string descriptiveDataSetName)
        {
            IList<string> possibleValues = dataSetSpecification.getPossibleAttributeValues(descriptiveDataSetName);
            // If more than two possible values
            // then is multivalued
            if (possibleValues.Count > 2)
            {
                return true;
            }
            // If one of the possible values for the attribute
            // matches the true goal value then consider
            // it not being multivalued.
            foreach (string pv in possibleValues)
            {
                if (trueGoalValue.Equals(pv))
                {
                    return false;
                }
            }

            return true;
        }

        public string getExampleConstant(int egNo)
        {
            string egConstant = examplePrefix + egNo;
            addConstant(egConstant);
            return egConstant;
        }

        public string getFOLName(string dsName)
        {
            string folName = dsToFOLNameMap[dsName];
            if (null == folName)
            {
                folName = dsName;
                if (!char.IsLetter(dsName[0]))
                {
                    folName = "_" + dsName;
                }
                folName = allowableCharactersRegEx.Replace(folName, "_");
                dsToFOLNameMap[dsName] = folName;
            }

            return folName;
        }

        //
        // PRIVATE METHODS
        //
        private void constructFOLDomain()
        {
            // Ensure the target predicate is included
            addPredicate(getFOLName(dataSetSpecification.getTarget()));
            // Create the descriptive predicates
            foreach (string saName in dataSetSpecification.getNamesOfStringAttributes())
            {
                if (dataSetSpecification.getTarget().Equals(saName))
                {
                    // Don't add the target to the descriptive predicates
                    continue;
                }
                string folSAName = getFOLName(saName);
                // Add a predicate for the attribute
                addPredicate(folSAName);

                descriptionPredicateNames.Add(folSAName);
                descriptionDataSetNames.Add(saName);

                IList<string> attributeValues = dataSetSpecification.getPossibleAttributeValues(saName);
                // If a multivalued attribute need to setup
                // Constants for the different possible values
                if (isMultivalued(saName))
                {
                    foreach (string av in attributeValues)
                    {
                        addConstant(getFOLName(av));
                    }
                }
            }
        }
    }

}