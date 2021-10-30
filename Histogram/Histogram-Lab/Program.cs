using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> wordResults = new List<string>();
            char[] seperators = new char[] { '.', ',', '!', ':', ' ', ';', '\n', '\t', '\r' };

            List<string> sentenceResults = new List<string>();
            char[] seperators2 = new char[] { '.', '!'};

            wordResults = GetSpeech().Split(seperators, StringSplitOptions.RemoveEmptyEntries).ToList();
            sentenceResults = GetSpeech().Split(seperators2, StringSplitOptions.RemoveEmptyEntries).ToList();

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach(string word in wordResults)
            {
                if (wordCount.ContainsKey(word.ToLower()))
                {
                    wordCount[word.ToLower()] += 1;
                }
                else
                {
                    wordCount.Add(word.ToLower(), 1);
                }
            }

            bool exit = false;
             
            while (!exit)
            {
                int menuSelection = default;

                string[] options = new string[6]
                {
                    "1: The Speech",
                    "2: List of Words",
                    "3: Show Histogram",
                    "4: Search for Word",
                    "5: Remove Word",
                    "6: Exit"
                };
                 
                ReadChoice("What option would you like to choose? ", options, out menuSelection);

                switch (menuSelection)
                {
                    case 1:
                        string speech = GetSpeech();

                        Console.Clear();
                        Console.Write($"\n{speech}");
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    case 2:
                        Console.Clear();

                        foreach(string word in wordResults)
                        {
                            Console.WriteLine(word);
                        }

                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine();

                        int optionSelection = default;

                        string[] options2 = new string[2]
                        {
                            "1: Sort Alpahbetically",
                            "2: Sort by Count"
                        };

                        ReadChoice("What option would you like to choose? ", options2, out optionSelection);
                        switch (optionSelection)
                        {
                            case 1:
                                int yPos = 12;

                                List<string> keys = wordCount.Keys.ToList();
                                keys.Sort();

                                foreach (string word in keys)
                                {
                                    int amount = wordCount[word];
                                    int xPos = 20 - word.Length;

                                    Console.SetCursorPosition(xPos, yPos);
                                    Console.Write(word);
                                    Console.SetCursorPosition(24, yPos);
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                                    for (int i = 0; i < amount; i++)
                                    {
                                        Console.Write(" ");
                                    }

                                    Console.ResetColor();
                                    Console.Write(amount);
                                    yPos++;
                                }
                                break;
                            case 2:
                                int yPos2 = 12;

                                foreach (KeyValuePair<string, int> wordSet in wordCount.OrderBy(key => key.Value))
                                {
                                    string word = wordSet.Key;
                                    int amount = wordSet.Value;
                                    int xPos = 20 - word.Length;

                                    Console.SetCursorPosition(xPos, yPos2);
                                    Console.Write(word);
                                    Console.SetCursorPosition(24, yPos2);
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                                    for (int i = 0; i < amount; i++)
                                    {
                                        Console.Write(" ");
                                    }

                                    Console.ResetColor();
                                    Console.Write(amount);
                                    yPos2++;
                                }
                                break;
                        }
     
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        string chosenWord = default;
                        int yPosition = 9;

                        ReadString("What word do you want to find? ", ref chosenWord);

                        if (wordCount.ContainsKey(chosenWord.ToLower()))
                        {
                            Console.WriteLine();

                            int amount = wordCount[chosenWord.ToLower()];
                            int xPos = 10 - chosenWord.Length;

                            Console.SetCursorPosition(xPos, yPosition);
                            Console.Write(chosenWord);
                            Console.SetCursorPosition(14, yPosition);
                            Console.BackgroundColor = ConsoleColor.DarkBlue;

                            for (int i = 0; i < amount; i++)
                            {
                                Console.Write(" ");
                            }

                            Console.ResetColor();
                            Console.Write(amount);
                            Console.WriteLine("\n");

                            int loops = 1;

                            foreach (string sentence in sentenceResults)
                            {
                                string[] words = sentence.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                                foreach (string word in words)
                                {
                                    if (word == chosenWord)
                                    {
                                        Console.WriteLine($"{loops}) {sentence}\n");
                                        loops++;
                                        break;
                                    }
                                }
                                
                            }
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{chosenWord} is not found");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                        }
                            break;
                    case 5:
                        string removeWord = default;

                        ReadString("What word would you like to remove? ", ref removeWord);

                        try
                        {
                            if (wordCount.Remove(removeWord))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine($"{removeWord} has been removed");
                                Console.ResetColor();
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            { 
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{removeWord} is not found");
                                Console.ResetColor();
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"{removeWord} is not found");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case 6:
                        exit = true;
                        break;
                }
            }

            int ReadInteger(string prompt, int min, int max)
            {
                int choice = default;
                bool makingChoice = true;

                do
                {
                    Console.Write($"{prompt}");

                    string anwser = Console.ReadLine();

                    if(int.TryParse(anwser, out int foundAnwser))
                    {
                        if(foundAnwser >= min && foundAnwser <= max)
                        {
                            choice = foundAnwser;
                            makingChoice = false;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"Please choose a number between {min} and {max}.");
                            Console.ReadKey();
                            Console.ResetColor();
                            ClearLastLine();
                            Console.CursorTop--;
                            ClearLastLine();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Please choose a correct number.");
                        Console.ReadKey();
                        Console.ResetColor();
                        ClearLastLine();
                        Console.CursorTop--;
                        ClearLastLine();
                    }

                } 
                while (makingChoice);

                return choice;

            }

            void ReadString(string prompt, ref string value)
            {
                bool makingChoice = true;

                do
                {
                    Console.Write($"{prompt}");

                    string anwser = Console.ReadLine();

                    if (!String.IsNullOrEmpty(anwser))
                    {
                        value = anwser;
                        makingChoice = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Please choose a word.");
                        Console.ReadKey();
                        Console.ResetColor();
                        ClearLastLine();
                    }
                }
                while (makingChoice);

            }

            void ReadChoice(string prompt, string[] options, out int selection)
            {
                foreach(string option in options)
                {
                    Console.WriteLine(option);
                }

                 selection = ReadInteger(prompt ,1 ,options.Length);
            }

            string GetSpeech()
            {
                string text = "I say to you today, my friends, so even though we face the difficulties of today and tomorrow, I still have a dream. It is a dream deeply rooted in the American dream. " +
           "I have a dream that one day this nation will rise up and live out the true meaning of its creed: We hold these truths to be self-evident: that all men are created equal. " +
           "I have a dream that one day on the red hills of Georgia the sons of former slaves and the sons of former slave owners will be able to sit down together at the table of brotherhood. " +
           "I have a dream that one day even the state of Mississippi, a state sweltering with the heat of injustice, sweltering with the heat of oppression, will be transformed into an oasis of freedom and justice. " +
           "I have a dream that my four little children will one day live in a nation where they will not be judged by the color of their skin but by the content of their character. " +
           "I have a dream today. I have a dream that one day, down in Alabama, with its vicious racists, with its governor having his lips dripping with the words of interposition and nullification; one day right there in Alabama, little black boys and black girls will be able to join hands with little white boys and white girls as sisters and brothers. " +
           "I have a dream today. I have a dream that one day every valley shall be exalted, every hill and mountain shall be made low, the rough places will be made plain, and the crooked places will be made straight, and the glory of the Lord shall be revealed, and all flesh shall see it together. " +
           "This is our hope. This is the faith that I go back to the South with. With this faith we will be able to hew out of the mountain of despair a stone of hope. With this faith we will be able to transform the jangling discords of our nation into a beautiful symphony of brotherhood. " +
           "With this faith we will be able to work together, to pray together, to struggle together, to go to jail together, to stand up for freedom together, knowing that we will be free one day. " +
           "This will be the day when all of God's children will be able to sing with a new meaning, My country, 'tis of thee, sweet land of liberty, of thee I sing. Land where my fathers died, land of the pilgrim's pride, from every mountainside, let freedom ring. " +
           "And if America is to be a great nation this must become true. So let freedom ring from the prodigious hilltops of New Hampshire. Let freedom ring from the mighty mountains of New York. Let freedom ring from the heightening Alleghenies of Pennsylvania! " +
           "Let freedom ring from the snowcapped Rockies of Colorado! Let freedom ring from the curvaceous slopes of California! But not only that; let freedom ring from Stone Mountain of Georgia! " +
           "Let freedom ring from Lookout Mountain of Tennessee! Let freedom ring from every hill and molehill of Mississippi. From every mountainside, let freedom ring. " +
           "And when this happens, when we allow freedom to ring, when we let it ring from every village and every hamlet, from every state and every city, we will be able to speed up that day when all of God's children, black men and white men, Jews and Gentiles, Protestants and Catholics, will be able to join hands and sing in the words of the old Negro spiritual, Free at last! free at last! thank God Almighty, we are free at last!";

                return text;
            }

            void ClearLastLine()
            {
                int currentLinePosition = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, currentLinePosition);
            }
        }
    }
}
