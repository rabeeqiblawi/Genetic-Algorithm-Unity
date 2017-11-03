import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Random;

public class GeneticAlgorithem implements Comparator<DNA> {
	
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
		/*returns a DNA ,DNAes with higher fitness are more 
		 * Luckily to be chosen */
		fitnessSum=0;
		for (int j=0;j<popSize;j++){
			fitnessSum +=population.get(j).fitness;//get the total fitness 
		}
		for(int i=0;i<popSize;i++){
			double test=random.nextDouble()*fitnessSum;
			if(test<population.get(i).fitness)//if this DNA has a fitness larger than a random number*total fitness select it
				return population.get(i);
			test=test-population.get(i).fitness;//remove the selected DNA from the next calculation
		}		
		return population.get(random.nextInt(popSize)) ; 
}
	public void generateNewGenration(int elitism){	
			newpopulation.clear();
			//To Do ....ElEtism
			Collections.sort(population,this);
			Generation++;
		for(int i=0;i<popSize;i++){
			
			if(i<elitism){
				newpopulation.add(population.get(i));
			}
			else{
			DNA parent1= weightedRandomSelection();
			DNA parent2 = weightedRandomSelection();
			DNA child = parent1.crossOver(parent2);
			child.Mutate(mutationRate);
			child.calculateFitness();
			newpopulation.add(child);	
			}	
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
	@Override
	public int compare(DNA o1, DNA o2) {
		if(o1.fitness<o2.fitness){
			
			return 1;
		}else if(o1.fitness>o2.fitness){
			return -1;
		}

		return 0;
	}
	

}
	
	