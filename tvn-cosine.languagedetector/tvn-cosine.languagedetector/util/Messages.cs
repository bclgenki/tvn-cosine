﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvn_cosine.languagedetector.util
{
    /**
     * This is {@link Messages} class generated by Eclipse automatically.
     * Users don't use this class directly.
     * @author Nakatani Shuyo
     */
    public class Messages
    {
        private static final string BUNDLE_NAME = "com.cybozu.labs.langdetect.util.messages"; //$NON-NLS-1$

    private static final ResourceBundle RESOURCE_BUNDLE = ResourceBundle.getBundle(BUNDLE_NAME);

    private Messages()
        {
        }

        public static string getString(string key)
        {
            try
            {
                return RESOURCE_BUNDLE.getString(key);
            }
            catch (MissingResourceException e)
            {
                return '!' + key + '!';
            }
        }
    }

}
