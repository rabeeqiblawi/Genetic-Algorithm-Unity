import java.util.Random;

public class Main {

	public static void main(String[] args) {
		
		char[] target = "this is a nice a day hello howhh".toCharArray();	
		GeneticAlgorithem g=new GeneticAlgorithem(500,target.length , new Random(), 0.003f,target);
		
		while(!g.endAlgorithm){
			g.generateNewGenration(8);
			
		}
					
	}	
}
