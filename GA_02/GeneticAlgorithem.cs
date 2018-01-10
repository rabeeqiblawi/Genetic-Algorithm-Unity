using System;
using System.Collections.Generic;
using UnityEngine;
public  class GeneticAlgorithem : MonoBehaviour, IComparer<Agent>
{
    public GameObject agent;
    public bool endAlgorithm = false;
    internal List<Agent>population;
    public int Generation;
    public int popSize;
    public double mutationRate;
    internal System.Random random;
    internal List<Agent> newpopulation;
    internal double fitnessSum;
    public int dnaSize;
    internal Agent bestFitted;

    List<Agent> grave;
    public void Awake()
    {
        grave= new List<Agent>();
        population = new List<Agent>();
        Generation = 1;
        newpopulation = new List<Agent>();
        random = new System.Random();

       
    }

    private void Start()
    {
        //populate
        for (int i = 0; i < popSize; i++)
        {
            Agent inst = Instantiate(agent).GetComponent<Agent>();
            inst.generate_random_chromosome(random);
            population.Add(inst);
        }
        //bestFitted = new Agent();
        StartRace();
        Time.timeScale = 3.5f;
    }


    public  Agent weightedRandomSelection()// choose parent 
    {
        /*returns a DNA ,DNAes with higher fitness are more 
		 * Luckily to be chosen */
        fitnessSum = 0;
        for (int j = 0; j < popSize; j++)
        {
            fitnessSum += population[j].fitness; //get the total fitness
        }
        for (int i = 0; i < popSize; i++)
        {
            //spin the fortune wheel 
            double spin = random.NextDouble() * fitnessSum;
            if (spin < population[i].fitness) //the larger the fitness of the indivedual the more luckely it's choosen 
            {
                return population[i];
            }
            spin = spin - population[i].fitness; //remove the selected DNA from the next calculation
        }
        return population[random.Next(popSize)];
    }
    public virtual void generateNewGenration(int elitism)//elitism : number of agents not effected 
    {
        newpopulation.Clear();
        population.Sort(this);//sort by fitness 
        Generation++;
        for (int i = 0; i < popSize; i++)
        {
            if (i <elitism)
            {
                print("&^&^&^&^&^"+population.Count);
                Agent topAgent = population[i];
                var clone = Instantiate(agent).GetComponent<Agent>();
                clone.SetChromosome(topAgent.chromosome);
                newpopulation.Add(clone);
                
            }
            else
            {
                Agent parent1 = weightedRandomSelection();
                Agent parent2 = weightedRandomSelection();
                Agent child =Instantiate(agent).GetComponent<Agent>();
                child.SetChromosome(parent1.crossOver(parent2.chromosome,random));
                child.Mutate(mutationRate,random);
                newpopulation.Add(child);
                
            }
        }

        for (int i = 0; i < popSize; i++)//delete gameobjects from scene
        {
            Destroy(population[i].gameObject);
        }
        population.Clear();

        foreach (var item in newpopulation)
        {
            population.Add(item);
        }
        bestFitted = population[0];
        print("best of his generation :   ");
        print("best chromosome"+bestFitted.chromosome.ToArray());
        print(" fitness :" + bestFitted.fitness);
        print(" generation :" + Generation);

    }

void StartRace()
    {
        for (int i = 0; i <popSize; i++)
        {
            population[i]._start = true;
        }

    }
    public virtual int Compare(Agent o1,Agent o2)
    {
        if (o1.fitness < o2.fitness)
        {
            return 1;
        }
        else if (o1.fitness > o2.fitness)
        {
            return -1;
        }
        return 0;
    }
    public void agent_died(Agent _agent) {
        grave.Add(_agent);
        print("OH Iam DEAD , MY FITNESS IS :"+_agent.fitness+"  dead counter:"+ grave.Count);
       
        if (grave.Count==popSize)
        {
            grave.Clear();
            generateNewGenration(2); 
            StartRace();
        }
    }
}
