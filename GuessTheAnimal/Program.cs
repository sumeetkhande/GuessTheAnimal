using System;
using System.Collections.Generic;
using System.Linq;


namespace GuessTheAnimal
{    
    //Animal with their characteristics
    class Animal
    {
        public string Name { get; set; }

        public List<string> Characteristics { get; set; }

        public Animal(string name, List<string> list)
        {
            Name = name;
            Characteristics = list;
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            //get the list of animal and their characteristics
            var animalList = new List<Animal>();
            //Animals can have unique characteristics or same as few others
            animalList.Add(new Animal("Elephant",new List<string>(new string[] {"has trunk"})));
            animalList.Add(new Animal("Giraffe", new List<string>(new string[] { "has long neck" })));
            animalList.Add(new Animal("Lion", new List<string>(new string[] { "roars" })));
            animalList.Add(new Animal("Tiger", new List<string>(new string[] { "roars","has scales" })));

            //list to store matched characteristics to avoid asking the same question
            var matchedCharacteristics = new List<string>();

            try
            {
                //start the game
                Console.WriteLine("Lets play the game.. 'guess the animal!'");

                //get the animal names 
                var animalNames = string.Join(", ", animalList.Select(animal => animal.Name));

                //show the animal list
                Console.WriteLine("Choose an animal from the list: " + animalNames);

                //start guessing
                Console.WriteLine("Now let me guess it");

                //keep guessing till we have only one animal left in our list
                while (animalList.Count > 1)
                {
                    //check for all animals
                    for(int i= animalList.Count-1; i>=0; i--)                   
                    {
                            //check for each characteristic of animal
                            for (int j = animalList[i].Characteristics.Count - 1; j>= 0; j--)
                            {
                                //match the characteristics in already matched set so that the question doesn't gets repeated
                                if (!matchedCharacteristics.Contains(animalList[i].Characteristics[j]))
                                {
                                    //ask the quesiton
                                    Console.WriteLine("Does the animal " + animalList[i].Characteristics[j] + "? (y/n)");

                                    //get the asnwer
                                    var input = Console.ReadLine().ToLower();

                                    //if yes then filter all the animals with that characteristic and start all over again from the filtered set
                                    if (input == "y")
                                    {
                                        matchedCharacteristics.Add(animalList[i].Characteristics[j]);

                                        var newList = from al in animalList
                                                      where al.Characteristics.Contains(animalList[i].Characteristics[j])
                                                      select al;
                                        animalList = new List<Animal>(newList);
                                        
                                        break;
                                    }
                                    //if no the remove the animal with unmatched characteristic
                                    else
                                    {
                                        animalList.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                        break;
                        }
                    }
                
                //show the result
                Console.WriteLine("I got it ... your animal is " + animalList[0].Name);                
            }
            catch (Exception)
            {
                //show the error
                Console.WriteLine("Sorry some error occurred.. It looks we have to start it again!!");
            }
        }
    }

}
