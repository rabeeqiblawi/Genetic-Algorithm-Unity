using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    #region  vars
    public float distance;
    public List<char> chromosome;
    public float y_axis_rotation = 30;
    public float fitness;
    int chromosomeLength=400;
    Rigidbody agent_rigid_body;
    Transform agent_transform;
    public Order current_direction;
    public List<Order> orders;
    private int direction_index;
    float execution_rate=2.8f;
    float nextOrder = 0.0f;
    public float speed = 1.2f;
    public bool _start=false;
   
#endregion
  
  
    void Awake () {
        
        agent_rigid_body = GetComponent<Rigidbody>();
        agent_transform = GetComponent<Transform>();
        orders = new List<Order>();
        direction_index = 0;
        chromosome = new List<char>();
        
    }

   
    void Update() {
        if (_start)
        {
            transform.Translate(0, 0, Time.deltaTime *speed);
            
        }
    }

    private void FixedUpdate()
    {
        execute_orders();//TO-DO :  pass to controller 
    }

    void execute_orders()
    {
        if (_start)
        {
            if (Time.time > nextOrder && direction_index < orders.Count)
            {
                nextOrder = Time.time + execution_rate;
                current_direction = orders[direction_index];
                direction_index++;

                switch (current_direction)
                {
                    case Order.MOVE_FORWARD:
                        //nothing
                        break;
                    case Order.MOVE_LEFT:

                        transform.Rotate(0, -y_axis_rotation, 0);

                        break;
                    case Order.MOVE_RIGHT:
                        transform.Rotate(0, y_axis_rotation, 0);
                        break;
                }
            }
        }
    }

    void Calculate_fitness() {//called onCollision

        fitness = distance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("walls")))
        {
            print("X _ X");
            _start = false;
            Calculate_fitness();
            GameObject.FindGameObjectWithTag("GA").GetComponent<GeneticAlgorithem>().agent_died(this);
        }
    }
    
   void Decode() 
    {
       // orders.Clear();
        for (int i = 0; i < chromosome.Count; i+=2)
        {
            if (chromosome[i] == '0' && chromosome[i + 1] == '0')
            {
                orders.Add(Order.MOVE_FORWARD);
            }
            else if (chromosome[i] == '0' && chromosome[i + 1] == '1')
            {
                orders.Add(Order.MOVE_LEFT);
            }
            else if (chromosome[i] == '1' && chromosome[i + 1] == '0')
            {
                orders.Add(Order.MOVE_RIGHT);
            }
            else if (chromosome[i] == '1' && chromosome[i + 1] == '1') {
                orders.Add(Order.MOVE_FORWARD);
            }


        }

    }
    

    char generateRandomGene(System.Random random)
    {
        char newGene;
        char[] charSet = "01".ToCharArray();
        newGene = charSet[random.Next(charSet.Length)];
        return newGene;
    }

    public List<char> crossOver(List<char> other,System.Random random)
    {
        List<char> child = new List<char>();
        for (int i = 0; i < chromosomeLength; i++)
        {
            child.Add(random.NextDouble() < 0.5 ? chromosome[i] : other[i]); //toss a coin 
        }
        return child;
    }

    public void Mutate(double mutationRate,System.Random random)
    {
        for (int i = 0; i < chromosomeLength; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                chromosome[i] = generateRandomGene(random);
            }
        }
    }


    public void SetChromosome(List<char> chromosome) {
        this.chromosome = chromosome;
        Decode();
    }

    public List<Order> GetOrders() {
        return orders;
    }

    public void generate_random_chromosome(System.Random random) {
        chromosome.Clear();
            for (int i = 0; i < chromosomeLength; i++)
            {
            var c = generateRandomGene(random);
                chromosome.Add(c);
            }  
        Decode();  
    }


    public void UpDateDistance(float i)
    {//called by SendMessage , sender : cubes with indexes
       this.distance = i;
    }
}
public enum Order {MOVE_LEFT,MOVE_RIGHT,MOVE_FORWARD,MOVE_BACKWARD}
