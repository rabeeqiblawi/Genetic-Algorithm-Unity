import java.util.ArrayList;
import java.util.Random;

public class GeneticAlgorithem {
	
	boolean endAlgorithm=false;
	ArrayList<DNA> population ;
	int Generation;
	int popSize;
	double mutationRate;
	Random random;
	ArrayList<DNA> newpopulation;
	char[] target;
	double fitnessSum;
	int dnaSize;
	DNA bestFitted;
	
	
	public GeneticAlgorithem(int popSize,int dnaSize,Random random,double mutationRate,char[] target){
		this.popSize= popSize;
		this.random=random;
		this.mutationRate=mutationRate;
		population = new ArrayList<DNA>();	
		Generation=1;
		newpopulation=new ArrayList<DNA>();
		this.dnaSize=dnaSize;
		//populate ...
		for(int i=0;i<popSize;i++){
			population.add(new DNA(dnaSize,target,random,true));
		}		
		bestFitted=new DNA(dnaSize,target,random,true);
	}
	public DNA weightedRandomSelection(){
		fitnessSum=0;
		for (int j=0;j<popSize;j++){
			fitnessSum +=population.get(j).fitness;
		}
		for(int i=0;i<popSize;i++){
			double test=random.nextDouble()*fitnessSum;
			if(test<population.get(i).fitness)
				return population.get(i);
			test=test-population.get(i).fitness;
		}
		
		return population.get(random.nextInt(popSize)) ; 
}
	public void generateNewGenration(){	
			newpopulation.clear();
			Generation++;
		for(int i=0;i<popSize;i++){
			DNA parent1= weightedRandomSelection();
			DNA parent2 = weightedRandomSelection();
			DNA child = parent1.crossOver(parent2);
			child.Mutate(mutationRate);
			child.calculateFitness();
			newpopulation.add(child);		
		}			
		population.clear();
		population.addAll(newpopulation);
		
		bestFitted.fitness=0;
		for(int i=0;i<popSize;i++){
			if(bestFitted.fitness < population.get(i).fitness)
				bestFitted=population.get(i);
			if(population.get(i).fitness==1)
				endAlgorithm=true;
			}
			
		System.out.print("best of his generation :   ");
		System.out.print( bestFitted.genes);
		System.out.print(" fitness :" + bestFitted.fitness);
		System.out.println(" generation :"+Generation);
	
	}
	
	}
	
	