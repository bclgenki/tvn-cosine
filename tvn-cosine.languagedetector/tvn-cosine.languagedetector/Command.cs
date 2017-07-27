﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvn_cosine.languagedetector
{
    /**
     * 
     * LangDetect Command Line Interface
     * <p>
     * This is a command line interface of Language Detection Library "LandDetect".
     * 
     * 
     * @author Nakatani Shuyo
     *
     */
    public class Command
    {
        /** smoothing default parameter (ELE) */
        private static final double DEFAULT_ALPHA = 0.5;

        /** for Command line easy parser */
        private IDictionary<string, String> opt_with_value = new Dictionary<string, String>();
        private IDictionary<string, String> values = new Dictionary<string, String>();
        private HashSet<string> opt_without_value = new HashSet<string>();
        private IList<string> arglist = new List<string>();

        /**
         * Command line easy parser
         * @param args command line arguments
         */
        private void parse(String[] args)
        {
            for (int i = 0; i < args.length; ++i)
            {
                if (opt_with_value.containsKey(args[i]))
                {
                    string key = opt_with_value.get(args[i]);
                    values.put(key, args[i + 1]);
                    ++i;
                }
                else if (args[i].startsWith("-"))
                {
                    opt_without_value.add(args[i]);
                }
                else
                {
                    arglist.add(args[i]);
                }
            }
        }

        private void addOpt(string opt, string key, string value)
        {
            opt_with_value.put(opt, key);
            values.put(key, value);
        }
        private string get(string key)
        {
            return values.get(key);
        }
        private Long getLong(string key)
        {
            string value = values.get(key);
            if (value == null) return null;
            try
            {
                return Long.valueOf(value);
            }
            catch (NumberFormatException e)
            {
                return null;
            }
        }
        private double getDouble(string key, double defaultValue)
        {
            try
            {
                return Double.valueOf(values.get(key));
            }
            catch (NumberFormatException e)
            {
                return defaultValue;
            }
        }

        private bool hasOpt(string opt)
        {
            return opt_without_value.contains(opt);
        }


        /**
         * File search (easy glob)
         * @param directory directory path
         * @param pattern   searching file pattern with regular representation
         * @return matched file
         */
        private File searchFile(File directory, string pattern)
        {
            for (File file : directory.listFiles())
            {
                if (file.getName().matches(pattern)) return file;
            }
            return null;
        }


        /**
         * load profiles
         * @return false if load success
         */
        private bool loadProfile()
        {
            string profileDirectory = get("directory") + "/";
            try
            {
                DetectorFactory.loadProfile(profileDirectory);
                Long seed = getLong("seed");
                if (seed != null) DetectorFactory.setSeed(seed);
                return false;
            }
            catch (LangDetectException e)
            {
                System.err.println("ERROR: " + e.getMessage());
                return true;
            }
        }

        /**
         * Generate Language Profile from Wikipedia Abstract Database File
         * 
         * <pre>
         * usage: --genprofile -d [abstracts directory] [language names]
         * </pre>
         * 
         */
        public void generateProfile()
        {
            File directory = new File(get("directory"));
            for (string lang: arglist)
            {
                File file = searchFile(directory, lang + "wiki-.*-abstract\\.xml.*");
                if (file == null)
                {
                    System.err.println("Not Found abstract xml : lang = " + lang);
                    continue;
                }

                FileOutputStream os = null;
                try
                {
                    LangProfile profile = GenProfile.loadFromWikipediaAbstract(lang, file);
                    profile.omitLessFreq();

                    File profile_path = new File(get("directory") + "/profiles/" + lang);
                    os = new FileOutputStream(profile_path);
                    JSON.encode(profile, os);
                }
                catch (JSONException e)
                {
                    e.printStackTrace();
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
                catch (LangDetectException e)
                {
                    e.printStackTrace();
                }
                finally
                {
                    try
                    {
                        if (os != null) os.close();
                    }
                    catch (IOException e) { }
                }
            }
        }

        /**
         * Generate Language Profile from Text File
         * 
         * <pre>
         * usage: --genprofile-text -l [language code] [text file path]
         * </pre>
         * 
         */
        private void generateProfileFromText()
        {
            if (arglist.size() != 1)
            {
                System.err.println("Need to specify text file path");
                return;
            }
            File file = new File(arglist.get(0));
            if (!file.exists())
            {
                System.err.println("Need to specify existing text file path");
                return;
            }

            string lang = get("lang");
            if (lang == null)
            {
                System.err.println("Need to specify langage code(-l)");
                return;
            }

            FileOutputStream os = null;
            try
            {
                LangProfile profile = GenProfile.loadFromText(lang, file);
                profile.omitLessFreq();

                File profile_path = new File(lang);
                os = new FileOutputStream(profile_path);
                JSON.encode(profile, os);
            }
            catch (JSONException e)
            {
                e.printStackTrace();
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            catch (LangDetectException e)
            {
                e.printStackTrace();
            }
            finally
            {
                try
                {
                    if (os != null) os.close();
                }
                catch (IOException e) { }
            }
        }

        /**
         * Language detection test for each file (--detectlang option)
         * 
         * <pre>
         * usage: --detectlang -d [profile directory] -a [alpha] -s [seed] [test file(s)]
         * </pre>
         * 
         */
        public void detectLang()
        {
            if (loadProfile()) return;
            for (string filename: arglist)
            {
                BufferedReader is = null;
                try
                {
                is = new BufferedReader(new InputStreamReader(new FileInputStream(filename), "utf-8"));

                    Detector detector = DetectorFactory.create(getDouble("alpha", DEFAULT_ALPHA));
                    if (hasOpt("--debug")) detector.setVerbose();
                    detector.append(is);
                    System.out.println(filename + ":" + detector.getProbabilities());
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
                catch (LangDetectException e)
                {
                    e.printStackTrace();
                }
                finally
                {
                    try
                    {
                        if (is!= null) is.close();
                    }
                    catch (IOException e) { }
                }

            }
        }

        /**
         * Batch Test of Language Detection (--batchtest option)
         * 
         * <pre>
         * usage: --batchtest -d [profile directory] -a [alpha] -s [seed] [test data(s)]
         * </pre>
         * 
         * The format of test data(s):
         * <pre>
         *   [correct language name]\t[text body for test]\n
         * </pre>
         *  
         */
        public void batchTest()
        {
            if (loadProfile()) return;
            IDictionary<string, IList<string>> result = new Dictionary<string, IList<string>>();
            for (string filename: arglist)
            {
                BufferedReader is = null;
                try
                {
                is = new BufferedReader(new InputStreamReader(new FileInputStream(filename), "utf-8"));
                    while (is.ready()) {
                string line = is.readLine();
                int idx = line.indexOf('\t');
                if (idx <= 0) continue;
                string correctLang = line.substring(0, idx);
                string text = line.substring(idx + 1);

                Detector detector = DetectorFactory.create(getDouble("alpha", DEFAULT_ALPHA));
                detector.append(text);
                string lang = "";
                try
                {
                    lang = detector.detect();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
                if (!result.containsKey(correctLang)) result.put(correctLang, new List<string>());
                result.get(correctLang).add(lang);
                if (hasOpt("--debug")) System.out.println(correctLang + "," + lang + "," + (text.length() > 100 ? text.substring(0, 100) : text));
            }

        } catch (IOException e) {
                e.printStackTrace();
            } catch (LangDetectException e) {
                e.printStackTrace();
            } finally {
                try {
                    if (is!=null) is.close();
                } catch (IOException e) {}
            }

            IList<string> langlist = new List<string>(result.keySet());
Collections.sort(langlist);

            int totalCount = 0, totalCorrect = 0;
            for ( string lang :langlist) {
                IDictionary<string, Integer> resultCount = new Dictionary<string, Integer>();
int count = 0;
IList<string> list = result.get(lang);
                for (string detectedLang: list) {
                    ++count;
                    if (resultCount.containsKey(detectedLang)) {
                        resultCount.put(detectedLang, resultCount.get(detectedLang) + 1);
                    } else {
                        resultCount.put(detectedLang, 1);
                    }
                }
                int correct = resultCount.containsKey(lang) ? resultCount.get(lang) : 0;
double rate = correct / (double)count;
System.out.println(String.format("%s (%d/%d=%.2f): %s", lang, correct, count, rate, resultCount));
                totalCorrect += correct;
                totalCount += count;
            }
            System.out.println(String.format("total: %d/%d = %.3f", totalCorrect, totalCount, totalCorrect / (double)totalCount));
            
        }
        
    }

    /**
     * Command Line Interface
     * @param args command line arguments
     */
    public static void main(String[] args)
{
    Command command = new Command();
    command.addOpt("-d", "directory", "./");
    command.addOpt("-a", "alpha", "" + DEFAULT_ALPHA);
    command.addOpt("-s", "seed", null);
    command.addOpt("-l", "lang", null);
    command.parse(args);

    if (command.hasOpt("--genprofile"))
    {
        command.generateProfile();
    }
    else if (command.hasOpt("--genprofile-text"))
    {
        command.generateProfileFromText();
    }
    else if (command.hasOpt("--detectlang"))
    {
        command.detectLang();
    }
    else if (command.hasOpt("--batchtest"))
    {
        command.batchTest();
    }
}

}

}
