﻿using tvn.cosine.collections;
using tvn.cosine.collections.api;
using tvn.cosine.ai.logic.fol;
using tvn.cosine.ai.logic.fol.inference;
using tvn.cosine.ai.logic.fol.inference.proof;
using tvn.cosine.ai.logic.fol.kb;
using tvn.cosine.ai.logic.fol.parsing.ast;

namespace tvn_cosine.ai.demo.logic.fol
{
   public abstract class FolDemoBase
    { 
        protected static void kingsDemo1(InferenceProcedure ip)
        {
            StandardizeApartIndexicalFactory.flush();

            FOLKnowledgeBase kb = FOLKnowledgeBaseFactory.createKingsKnowledgeBase(ip);

            string kbStr = kb.ToString();

            ICollection<Term> terms = CollectionFactory.CreateQueue<Term>();
            terms.Add(new Constant("John"));
            Predicate query = new Predicate("Evil", terms);

            InferenceResult answer = kb.ask(query);

            System.Console.WriteLine("Kings Knowledge Base:");
            System.Console.WriteLine(kbStr);
            System.Console.WriteLine("Query: " + query);
            foreach (Proof p in answer.getProofs())
            {
                System.Console.Write(ProofPrinter.printProof(p));
                System.Console.WriteLine("");
            }
        }

        protected static void kingsDemo2(InferenceProcedure ip)
        {
            StandardizeApartIndexicalFactory.flush();

            FOLKnowledgeBase kb = FOLKnowledgeBaseFactory
                    .createKingsKnowledgeBase(ip);

            string kbStr = kb.ToString();

            ICollection<Term> terms = CollectionFactory.CreateQueue<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("King", terms);

            InferenceResult answer = kb.ask(query);

            System.Console.WriteLine("Kings Knowledge Base:");
            System.Console.WriteLine(kbStr);
            System.Console.WriteLine("Query: " + query);
            foreach (Proof p in answer.getProofs())
            {
                System.Console.Write(ProofPrinter.printProof(p));
            }
        }

        protected static void weaponsDemo(InferenceProcedure ip)
        {
            StandardizeApartIndexicalFactory.flush();

            FOLKnowledgeBase kb = FOLKnowledgeBaseFactory.createWeaponsKnowledgeBase(ip);

            string kbStr = kb.ToString();

            ICollection<Term> terms = CollectionFactory.CreateQueue<Term>();
            terms.Add(new Variable("x"));
            Predicate query = new Predicate("Criminal", terms);

            InferenceResult answer = kb.ask(query);

            System.Console.WriteLine("Weapons Knowledge Base:");
            System.Console.WriteLine(kbStr);
            System.Console.WriteLine("Query: " + query);
            foreach (Proof p in answer.getProofs())
            {
                System.Console.Write(ProofPrinter.printProof(p));
                System.Console.WriteLine("");
            }
        }

        protected static void lovesAnimalDemo(InferenceProcedure ip)
        {
            StandardizeApartIndexicalFactory.flush();

            FOLKnowledgeBase kb = FOLKnowledgeBaseFactory.createLovesAnimalKnowledgeBase(ip);

            string kbStr = kb.ToString();

            ICollection<Term> terms = CollectionFactory.CreateQueue<Term>();
            terms.Add(new Constant("Curiosity"));
            terms.Add(new Constant("Tuna"));
            Predicate query = new Predicate("Kills", terms);

            InferenceResult answer = kb.ask(query);

            System.Console.WriteLine("Loves Animal Knowledge Base:");
            System.Console.WriteLine(kbStr);
            System.Console.WriteLine("Query: " + query);
            foreach (Proof p in answer.getProofs())
            {
                System.Console.Write(ProofPrinter.printProof(p));
                System.Console.WriteLine("");
            }
        }

        protected static void abcEqualityAxiomDemo(InferenceProcedure ip)
        {
            StandardizeApartIndexicalFactory.flush();

            FOLKnowledgeBase kb = FOLKnowledgeBaseFactory
                    .createABCEqualityKnowledgeBase(ip, true);

            string kbStr = kb.ToString();

            TermEquality query = new TermEquality(new Constant("A"), new Constant("C"));

            InferenceResult answer = kb.ask(query);

            System.Console.WriteLine("ABC Equality Axiom Knowledge Base:");
            System.Console.WriteLine(kbStr);
            System.Console.WriteLine("Query: " + query);
            foreach (Proof p in answer.getProofs())
            {
                System.Console.Write(ProofPrinter.printProof(p));
                System.Console.WriteLine("");
            }
        }

        protected static void abcEqualityNoAxiomDemo(InferenceProcedure ip)
        {
            StandardizeApartIndexicalFactory.flush();

            FOLKnowledgeBase kb = FOLKnowledgeBaseFactory.createABCEqualityKnowledgeBase(ip, false);

            string kbStr = kb.ToString();

            TermEquality query = new TermEquality(new Constant("A"), new Constant("C"));

            InferenceResult answer = kb.ask(query);

            System.Console.WriteLine("ABC Equality No Axiom Knowledge Base:");
            System.Console.WriteLine(kbStr);
            System.Console.WriteLine("Query: " + query);
            foreach (Proof p in answer.getProofs())
            {
                System.Console.Write(ProofPrinter.printProof(p));
                System.Console.WriteLine("");
            }
        }
    }
}
