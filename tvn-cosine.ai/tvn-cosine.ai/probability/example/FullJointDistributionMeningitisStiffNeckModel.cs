﻿using tvn.cosine.ai.probability.full;

namespace tvn.cosine.ai.probability.example
{
    /**
     * 
     * @author Ciaran O'Reilly
     *
     */
    public class FullJointDistributionMeningitisStiffNeckModel : FullJointDistributionModel<string>
    { 
        public FullJointDistributionMeningitisStiffNeckModel()
                : base(new double[] {
				// Meningitis * StiffNeck = 4 possible worlds
				// Meningitis = true, StiffNeck = true
				0.000014, // i.e 1/50000 * 0.7
				// Meningitis = true, StiffNeck = false
				0.000006, // i.e. (1/50000) * 0.3
				// Meningitis = false, StiffNeck = true
				0.009986, // i.e. 0.01 - 0.000014
				// Meningitis = false, StiffNeck = false
				0.989994 // i.e. 0.99 - 0.000006
                }, ExampleRV.MENINGITIS_RV, ExampleRV.STIFF_NECK_RV)
        { }
    }
}
