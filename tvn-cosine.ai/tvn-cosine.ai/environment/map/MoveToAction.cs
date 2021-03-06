﻿using tvn.cosine.ai.agent;

namespace tvn.cosine.ai.environment.map
{
    public class MoveToAction : DynamicAction
    { 
        public const string ATTRIBUTE_MOVE_TO_LOCATION = "location";

        public MoveToAction(string location)
            : base("moveTo") 
        {
            SetAttribute(ATTRIBUTE_MOVE_TO_LOCATION, location);
        }

        public string getToLocation()
        {
            return (string)GetAttribute(ATTRIBUTE_MOVE_TO_LOCATION);
        }
    }
}
