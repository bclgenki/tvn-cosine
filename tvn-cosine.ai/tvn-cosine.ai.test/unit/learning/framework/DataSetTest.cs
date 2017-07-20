﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.datastructures;
using tvn.cosine.ai.common.exceptions;
using tvn.cosine.ai.learning.framework;
using tvn.cosine.ai.learning.neural;

namespace tvn_cosine.ai.test.unit.learning.framework
{
    [TestClass]
    public class DataSetTest
    {
        private static readonly string YES = "Yes";

       // DataSetSpecification spec;

        [TestMethod]
        public void testNormalizationOfFileBasedDataProducesCorrectMeanStdDevAndNormalizedValues()
        {
            RabbitEyeDataSet reds = new RabbitEyeDataSet();
            reds.createNormalizedDataFromFile("rabbiteyes");

            IQueue<double> means = reds.getMeans();
            Assert.AreEqual(2, means.Size());
            Assert.AreEqual(244.771, means.Get(0), 0.001);
            Assert.AreEqual(145.505, means.Get(1), 0.001);

            IQueue<double> stdev = reds.getStdevs();
            Assert.AreEqual(2, stdev.Size());
            Assert.AreEqual(213.554, stdev.Get(0), 0.001);
            Assert.AreEqual(65.776, stdev.Get(1), 0.001);

            IQueue<IQueue<double>> normalized = reds.getNormalizedData();
            Assert.AreEqual(70, normalized.Size());

            // check first value
            Assert.AreEqual(-1.0759, normalized.Get(0).Get(0), 0.001);
            Assert.AreEqual(-1.882, normalized.Get(0).Get(1), 0.001);

            // check last Value
            Assert.AreEqual(2.880, normalized.Get(69).Get(0), 0.001);
            Assert.AreEqual(1.538, normalized.Get(69).Get(1), 0.001);
        }

        [TestMethod]
        public void testExampleFormation()
        {
            RabbitEyeDataSet reds = new RabbitEyeDataSet();
            reds.createExamplesFromFile("rabbiteyes");
            Assert.AreEqual(70, reds.howManyExamplesLeft());
            reds.getExampleAtRandom();
            Assert.AreEqual(69, reds.howManyExamplesLeft());
            reds.getExampleAtRandom();
            Assert.AreEqual(68, reds.howManyExamplesLeft());
        }

        [TestMethod]
        public void testLoadsDatasetFile()
        {

            DataSet ds = DataSetFactory.getRestaurantDataSet();
            Assert.AreEqual(12, ds.size());

            Example first = ds.getExample(0);
            Assert.AreEqual(YES, first.getAttributeValueAsString("alternate"));
            Assert.AreEqual("$$$", first.getAttributeValueAsString("price"));
            Assert.AreEqual("0-10",
                        first.getAttributeValueAsString("wait_estimate"));
            Assert.AreEqual(YES, first.getAttributeValueAsString("will_wait"));
            Assert.AreEqual(YES, first.targetValue());
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testThrowsExceptionForNonExistentFile()
        {
            new DataSetFactory().fromFile("nonexistent", null, null);
        }

        [TestMethod]
        public void testLoadsIrisDataSetWithNumericAndStringAttributes()
        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(0);
            Assert.AreEqual("5.1", first.getAttributeValueAsString("sepal_length"));
        }

        [TestMethod]
        public void testNonDestructiveRemoveExample()
        {
            DataSet ds1 = DataSetFactory.getRestaurantDataSet();
            DataSet ds2 = ds1.removeExample(ds1.getExample(0));
            Assert.AreEqual(12, ds1.size());
            Assert.AreEqual(11, ds2.size());
        }

        [TestMethod]
        public void testNumerizesAndDeNumerizesIrisDataSetExample1()


        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(0);
            Numerizer n = new IrisDataSetNumerizer();
            Pair<IQueue<double>, IQueue<double>> io = n.numerize(first);

            Assert.AreEqual(Factory.CreateQueue<double>(new[] { 5.1, 3.5, 1.4, 0.2 }), io.getFirst());
            Assert.AreEqual(Factory.CreateQueue<double>(new[] { 0.0, 0.0, 1.0 }), io.getSecond());

            string plant_category = n.denumerize(Factory.CreateQueue<double>(new[] { 0.0, 0.0, 1.0 }));
            Assert.AreEqual("setosa", plant_category);
        }

        [TestMethod]
        public void testNumerizesAndDeNumerizesIrisDataSetExample2()


        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(51);
            Numerizer n = new IrisDataSetNumerizer();
            Pair<IQueue<double>, IQueue<double>> io = n.numerize(first);

            Assert.AreEqual(Factory.CreateQueue<double>(new[] { 6.4, 3.2, 4.5, 1.5 }), io.getFirst());
            Assert.AreEqual(Factory.CreateQueue<double>(new[] { 0.0, 1.0, 0.0 }), io.getSecond());

            string plant_category = n.denumerize(Factory.CreateQueue<double>(new[] { 0.0, 1.0, 0.0 }));
            Assert.AreEqual("versicolor", plant_category);
        }

        [TestMethod]
        public void testNumerizesAndDeNumerizesIrisDataSetExample3()


        {
            DataSet ds = DataSetFactory.getIrisDataSet();
            Example first = ds.getExample(100);
            Numerizer n = new IrisDataSetNumerizer();
            Pair<IQueue<double>, IQueue<double>> io = n.numerize(first);

            Assert.AreEqual(Factory.CreateQueue<double>(new[] { 6.3, 3.3, 6.0, 2.5 }), io.getFirst());
            Assert.AreEqual(Factory.CreateQueue<double>(new[] { 1.0, 0.0, 0.0 }), io.getSecond());

            string plant_category = n.denumerize(Factory.CreateQueue<double>(new[] { 1.0, 0.0, 0.0 }));
            Assert.AreEqual("virginica", plant_category);
        }
    } 
}