﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return default(double[]);

                    var newArray = new double[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return default(int[]);

                    var newArray = new int[_places.Length];
                    Array.Copy(_places, newArray, _places.Length);
                    return newArray;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) return 0;
                    return _places.Sum(); //or nah?
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[0];
                _places = new int[7];
            }

            public void Evaluate(double result)
            {
                if (_marks.Length == 7 || _marks == null) return; 
                Array.Resize(ref _marks, _marks.Length+1);
                _marks[_marks.Length-1] = result;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                for (int judgeIndex = 0; judgeIndex < 7; judgeIndex++)
                {
                    SortByJudge(participants, judgeIndex);
                    for (int i = 0; i < participants.Length; i++)
                    {
                        participants[i]._places[judgeIndex] = i+1;
                    }
                }
            }
            private static void SortByJudge(Participant[] array, int judgeIndex)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].Marks[judgeIndex] < key.Marks[judgeIndex]) 
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && CompareParticipants(array[j], key))
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }
            private static bool CompareParticipants(Participant p1, Participant p2) 
            {
                //True: p1>p2
                //False: p1<p2
                if (p1.Score != p2.Score) return p1.Score > p2.Score; 
                if (p1.Places.Min() != p1.Places.Min()) return p1.Places.Min() < p1.Places.Min(); //Parity by sum of places
                return p1.Marks.Sum() > p2.Marks.Sum(); //Parity by max judge place
            }
        }
    }
}
