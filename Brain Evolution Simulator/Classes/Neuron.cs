using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Brain_Evolution_Simulator
{
    public class Neuron
    {
        public bool isEyeNeuron = false; 
        public bool isMotorNeuron = false;

        public float eyeSectionStart = 0f;
        public float eyeSectionEnd = 0f;

        public int brainPositionX;
        public int brainPositionY;
        public int connectionStrengthResolution;
        public int maxConnections;
        public float connectionChance;
        public float fireConnectionThreshold;

        public float incomingConnectionStrength = 0f;
        public bool isFirstConnection = true;
        public float finalConnectionStrength = 0f;

        Random random;
        Entity parentEntity;

        public List<Utility.Connection> connectionList = new List<Utility.Connection>();
        
        public Neuron(Entity entity, int x, int y, Random random, int connectionStrengthResolution, float connectionChance, int maxConnections) // Normal constructor, used to create new neuron
        {
            // Assign values
            brainPositionX = x; 
            brainPositionY = y; 
            this.connectionStrengthResolution = connectionStrengthResolution;
            this.connectionChance = connectionChance;
            this.maxConnections = maxConnections;
            this.random = random;
            parentEntity = entity;

            fireConnectionThreshold = 0f;

            while (fireConnectionThreshold == 0f)
            {
                fireConnectionThreshold = (float)this.random.Next(connectionStrengthResolution + 1) / connectionStrengthResolution; // Determine neuron connection firing threshold
            }
            
            // Create connections to other neurons
            CreateConnections();
        }

        public void CreateConnections() // Create connections to other neurons
        {
            int brainXSize = parentEntity.brain.GetLength(0); 
            int brainYSize = parentEntity.brain.GetLength(1); 

            int numOfConnections = 0;
            while (numOfConnections < maxConnections)
            {
                float random0to1 = (float)random.Next(1001) / 1000;
                if (random0to1 >= connectionChance) break;

                numOfConnections++;
            }

            if (numOfConnections == 0) numOfConnections++;

            for(int i = 0; i < numOfConnections; i++) // Iterate for each connection
            {
                // Derive random values for connection
                int randomX = random.Next(brainXSize);
                int randomY = random.Next(brainYSize);
                float randomStrength = (float)random.Next(connectionStrengthResolution + 1) / connectionStrengthResolution; // Strength a float between 0 and 1 

                connectionList.Add(new Utility.Connection(randomX, randomY, randomStrength)); // Add new connection
            }
        }

        public void ReceiveConnection(float strength) // Public method called by other neurons to fire connection
        {
            if (isFirstConnection) incomingConnectionStrength = strength;
            else incomingConnectionStrength = (incomingConnectionStrength + strength) / 2;
            isFirstConnection = false;
        }

        public void FireConnections() // Fire connections
        {
            Debug.WriteLine("Final connection strength = " + finalConnectionStrength + "\nFire connection threshold = " + fireConnectionThreshold);

            if (finalConnectionStrength > fireConnectionThreshold)
            {
                foreach (Utility.Connection connection in connectionList)
                {
                    Neuron neuron = parentEntity.brain[connection.x, connection.y];
                    neuron.ReceiveConnection(connection.strength);
                    Debug.WriteLine("Connection fired to " + connection.x + ", " + connection.y);
                }
            }
        }

        public void SetFinalConnectionStrength() // Called by Entity when connections have ceased to fire
        {
            finalConnectionStrength = incomingConnectionStrength;

            // Reset receiver connection values
            isFirstConnection = true;
            incomingConnectionStrength = 0f;
        } 

        public void SetAsEyeNeuron(float sectionStart, float sectionEnd) // Called by Entity to set as eye neuron
        {
            isMotorNeuron = false;
            isEyeNeuron = true;

            eyeSectionStart = sectionStart;
            eyeSectionEnd = sectionEnd;

            fireConnectionThreshold = 0f; // Set threshold for eye to 0, should this be the case? There is no variability.
        }

        public void SetAsMotorNeuron() // Called by Entity to set as motor neuron
        {
            isEyeNeuron = false;
            isMotorNeuron = true;
        }

        public void Look(List<Utility.Destination> destinations, float maxViewDistance) // Set finalConnectionStrength based on what is seen
        {
            // Check if eye can see any of the food objects and set connection strength to the strongest source

            float connectionStrength = 0f;

            foreach(Utility.Destination destination in destinations)
            {
                if ((destination.direction >= eyeSectionStart) &&
                    (destination.direction <= eyeSectionEnd))
                {
                    float strength = 1f - (destination.distance / maxViewDistance);
                    if (strength > connectionStrength) connectionStrength = strength;
                }
            }
            
            // Override final connection strength

            finalConnectionStrength = connectionStrength;
        }
    }
}
