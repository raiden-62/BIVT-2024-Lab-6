using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null) return 0;
                int answered = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    switch (questionNumber) 
                    {
                        case 1:
                            if (responses[i].Animal != null) answered++;
                            break;
                        case 2:
                            if (responses[i].CharacterTrait != null) answered++;
                            break;
                        case 3:
                            if (responses[i].Concept != null) answered++;
                            break;
                        default:
                            return 0;
                    }
                }
                return answered;
            }

            public void Print()
            {
                Console.WriteLine(_animal + " " + _characterTrait + " " + _concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    if (_responses == null) return default(Response[]);

                    var newArray = new Response[_responses.Length];
                    Array.Copy(_responses, newArray, _responses.Length);
                    return newArray;
                }
            }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null || _responses == null) return;
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null) return null;
                Dictionary<string, int> responseCount = new Dictionary<string, int>();
                foreach (var response in _responses)
                {
                    switch (question)
                    {
                        case 1:
                            if (response.Animal != null)
                            {
                                if(!responseCount.ContainsKey(response.Animal)) responseCount.Add(response.Animal, 1);
                                else responseCount[response.Animal]++;
                            }
                            break;
                        case 2:
                            if (response.CharacterTrait != null)
                            {
                                if (!responseCount.ContainsKey(response.CharacterTrait)) responseCount.Add(response.CharacterTrait, 1);
                                else responseCount[response.CharacterTrait]++;
                            }
                            break;
                        case 3:
                            if (response.Concept != null)
                            {
                                if (!responseCount.ContainsKey(response.Concept)) responseCount.Add(response.Concept, 1);
                                else responseCount[response.Concept]++;
                            }
                            break;                     
                    }
                }

                var sortedByResponseCount = responseCount.OrderByDescending(response => response.Value).ToList();
                string[] topResponses = new string[sortedByResponseCount.Count];
                int i = 0;
                foreach(var pair in sortedByResponseCount)
                {
                    topResponses[i++] = pair.Key;
                }
                if (topResponses.Length > 5)
                {
                    Array.Resize(ref topResponses, 5);
                }

                return topResponses;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (var response in _responses)
                {
                    response.Print();
                }
            }
        }
    }
}
