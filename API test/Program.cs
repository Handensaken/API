using System;
using System.Net.Http;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace API_test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> choices = new List<string>();
            choices.Add("Pokemon");
            choices.Add("Digimon");
            RestClient client = new RestClient("https://pokeapi.co/api/v2/");
            switch (Selection(choices.ToArray()))
            {
                case 0:
                    {
                        System.Console.WriteLine("Stellar! Everyone likes Pokemon");
                        Press();
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine("You're cringe, not one person likes Digimon");
                        Press();
                        Environment.Exit(0);
                        break;
                    }
            }
            choices.Clear();
            RestRequest req = new RestRequest();
            bool loopydoopy = false;
            Pokemon pokemon = new Pokemon();
            int stupido = 0;

            while (!loopydoopy)
            {
                if (stupido == 10)
                {
                    System.Console.WriteLine("You've failed ten times in a row. You are assumed to have a very limited intelligence. Three pokemon will be suggested.");
                    System.Console.WriteLine("press enter if you understand");
                    Console.ReadLine();
                    loopydoopy = true;
                    //add 3 random pokemon names from api number wih Random type and get class witth name to add tto list and display in Selection  
                }
                else
                {
                    stupido++;
                    System.Console.WriteLine("Enter the name of a Pokemon");
                    string pokemonRequest = Console.ReadLine().ToLower();
                    req = new RestRequest($"pokemon/{pokemonRequest}");
                    Press();

                }

                IRestResponse response = client.Get(req);
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    loopydoopy = true;
                    pokemon = JsonConvert.DeserializeObject<Pokemon>(response.Content);
                    stupido = 0;
                }

            }

            //  Pokemon spinda = JsonConvert.DeserializeObject<Pokemon>(response.Content);

            System.Console.WriteLine(pokemon.name);
            Console.ReadLine();

        }
        static void Press()
        {
            System.Console.WriteLine("Press any button to continue");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void PrintChoices(string[] choices, int current)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (current == i)
                {
                    System.Console.WriteLine($"> {choices[i]}");
                }
                else
                {
                    System.Console.WriteLine($"  {choices[i]} ");
                }
            }
        }
        static int Selection(string[] choices)
        {
            int current = 0;
            while (true)
            {
                Console.Clear();
                PrintChoices(choices, current);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            current++;
                            current = current % choices.Length;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            current--;
                            if (current < 0) { current = choices.Length - 1; }
                            else
                            {
                                current = Math.Abs(current % choices.Length); //This now works properly
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            return current;
                        }
                    default:
                        {
                            // handle everything else
                        }
                        break;
                }
            }
        }
    }
}
