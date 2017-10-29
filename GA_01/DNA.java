import java.util.Random;

public class DNA {

	char[] genes;
	double fitness;
	Random random;
	char[] target;
	int size;
	
	
	public DNA(int size,char[] target,Random random,boolean shouldInitGenes){
		this.size = size;
		this.target = target;
		this.random = random;
		this.genes= new char[size];
		if(shouldInitGenes){//new child should inherit genes from his parents and not randomly
		{
		for (int i=0;i<size;i++){
			genes[i]=generateRandomChar();
		}
}
		calculateFitness();
		}
	}
	
	public  void   calculateFitness(){	
		double score=0;
		for(int i=0;i<size;i++)
		{
			if(genes[i]==target[i]){
				score=score+1;
			}
		}
		this.fitness = (double)((double)score/size);
		
	}	
	char generateRandomChar(){
		char newGene;
		char[] charSet="abcdefghijklmnopqrstuvwxyz ".toCharArray();
		newGene = charSet[random.nextInt(charSet.length)];
		return newGene;
	}
	
	public DNA crossOver(DNA other)
	{
		DNA child = new DNA(size,target, random,false);
		for(int i=0;i<size;i++){
			child.genes[i] = random.nextDouble() < 0.5 ? genes[i] : other.genes[i];
		}	
		return child;
	}
	public void  Mutate(double mutationRate){
		for(int i=0;i<size;i++){
			if(random.nextDouble()<mutationRate)	{
				genes[i]=generateRandomChar();		
			}		
		}			
	}
}
