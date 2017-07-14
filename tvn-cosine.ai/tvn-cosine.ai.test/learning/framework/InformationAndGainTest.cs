﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using tvn.cosine.ai.learning.framework;
using tvn.cosine.ai.util;

namespace tvn_cosine.ai.test.learning.framework
{
    [TestClass]
    public class InformationAndGainTest
    {
        [TestMethod]
        public void testInformationCalculation()
        {
            double[] fairCoinProbabilities = new double[] { 0.5, 0.5 };
            double[] loadedCoinProbabilities = new double[] { 0.01, 0.99 };

            Assert.AreEqual(1.0, Util.information(fairCoinProbabilities), 0.001);
            Assert.AreEqual(0.08079313589591118, Util.information(loadedCoinProbabilities), 0.000000000000000001);
        }

        [TestMethod]
        public void testBasicDataSetInformationCalculation()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            // this should be the generic distribution
            double infoForTargetAttribute = ds.getInformationFor();
            Assert.AreEqual(1.0, infoForTargetAttribute, 0.001);
        }

        [TestMethod]
        public void testDataSetSplit()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            // this should be the generic distribution
            IDictionary<string, DataSet> hash = ds.splitByAttribute("patrons");
            Assert.AreEqual(3, hash.Keys.Count);
            Assert.AreEqual(6, hash["Full"].size());
            Assert.AreEqual(2, hash["None"].size());
            Assert.AreEqual(4, hash["Some"].size());
        }

        [TestMethod]
        public void testGainCalculation()
        {
            DataSet ds = DataSetFactory.getRestaurantDataSet();
            double gain = ds.calculateGainFor("patrons");
            Assert.AreEqual(0.541, gain, 0.001);
            gain = ds.calculateGainFor("type");
            Assert.AreEqual(0.0, gain, 0.001);
        }
    }
}
