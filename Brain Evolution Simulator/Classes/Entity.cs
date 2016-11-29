using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
namespace Brain_Evolution_Simulator
{
    public class Entity
    {
        Random random;

        public World world;
        public Neuron[,] brain; // 2 dimensional array to hold neurons

        public PointF position;
        public PointF leftEyeOffset;
        public PointF rightEyeOffset;
        public PointF leftMotorOffset;
        public PointF rightMotorOffset;
        public float rotation;


        // Variables to judge performance
        public double totalSteps = 0f;
        public double distanceGainedPerStepAverage = 0f;
        public double foodEaten = 0d;
        public float previousDistance;
        public bool isFoodEatenLastStep = false;

        // Lists to hold eye and motor neuron groups
        public List<Neuron> leftEyeNeurons = new List<Neuron>();
        public List<Neuron> rightEyeNeurons = new List<Neuron>();
        public List<Neuron> leftMotorNeurons = new List<Neuron>();
        public List<Neuron> rightMotorNeurons = new List<Neuron>();

        public float leftMoveImpulse = 0f; // Resultant movement from left motor neurons
        public float rightMoveImpulse = 0f; // Resultant movement from right motor neurons
        
        public Entity(
            int xSize, 
            int ySize, 
            int eyeLength, 
            Random random, 
            int connectionStrengthResolution, 
            float connectionChance, 
            int motorLength, 
            int maxConnections, 
            PointF position, 
            float rotation, 
            int eyeFieldOfView, 
            World world) // Normal entity constructor for brand new entities
        {
            brain = new Neuron[xSize, ySize]; // create array at default size
            this.random = random; // Assign random reference
            this.position = position;
            this.rotation = rotation;

            this.world = world;

            //previousDistance = Utility.Distance(0f, 0f, world.worldXSize, world.worldYSize);

            CreateBrain(brain, connectionStrengthResolution, eyeLength, connectionChance, motorLength, maxConnections, eyeFieldOfView); // Create brain
            DefineOffsets();
        }

        public Entity(Entity parent, Random random) // Entity constructor for new child entity
        {
            brain = parent.brain; // Hold reference to parents brain
            this.random = random; // Assign random reference
        }

        public Entity(string entityPath) // Entity constructor for entity loaded from file
        {

        }

        public Neuron[,] MutateBrain(Neuron[,] brain) // Method to take a brain and mutate it, returning the reference to the mutated brain. Original object is unaffected.
        {
            return brain;            
        }

        private void CreateBrain(
            Neuron[,] brain, 
            int connectionStrengthResolution, 
            int eyeLength, 
            float connectionChance, 
            int motorLength, 
            int maxConnections, 
            int eyeFieldOfView) // Initialize the neurons in the brain and assign optic and motor neurons
        {
            // Step through all the neurons in the brain and perform constructor
            for (int i = 0; i < brain.GetLength(0); i++)
            {
                for (int j = 0; j < brain.GetLength(1); j++)
                {
                    brain[i, j] = new Neuron(this, i, j, random, connectionStrengthResolution, connectionChance, maxConnections); // For the specified neuron, pass the brain reference and its position, plus the random reference
                }
            }
            Debug.WriteLine("Brain created");
            
            
            // Assign neurons to be eyes

            // Define eye start and end positions

            int eyeDepth = 0; // Eyes are placed at top row of brain

            int leftEyeFirstX = 0;
            int leftEyeLastX = eyeLength - 1;

            int rightEyeFirstX = brain.GetLength(0) - eyeLength;
            int rightEyeLastX = brain.GetLength(0) - 1;

            float sectionSize = (float)eyeFieldOfView / (float)eyeLength; // Define size of section in degrees that eye can see
            float leftMostEyeStart = -(float)eyeFieldOfView / 2;
            int x = 0;
            for (int i = leftEyeFirstX; i <= leftEyeLastX; i++)
            {
                float eyeNeuronSectionEnd = leftMostEyeStart + ((x + 1) * sectionSize);
                if (eyeNeuronSectionEnd >= 360f) eyeNeuronSectionEnd = eyeNeuronSectionEnd - 360;
                float eyeNeuronSectionStart = leftMostEyeStart + (x * sectionSize);

                brain[i, eyeDepth].SetAsEyeNeuron(eyeNeuronSectionStart, eyeNeuronSectionEnd);
                leftEyeNeurons.Add(brain[i, eyeDepth]);
                x++;
            }

            x = 0;
            for (int i = rightEyeFirstX; i <= rightEyeLastX; i++)
            {
                float eyeNeuronSectionEnd = leftMostEyeStart + ((x + 1) * sectionSize);
                if (eyeNeuronSectionEnd >= 360f) eyeNeuronSectionEnd = eyeNeuronSectionEnd - 360;
                float eyeNeuronSectionStart = leftMostEyeStart + (x * sectionSize);

                brain[i, eyeDepth].SetAsEyeNeuron(eyeNeuronSectionStart, eyeNeuronSectionEnd);
                rightEyeNeurons.Add(brain[i, eyeDepth]);
                x++;
            }

            // Assign neurons to be motor neurons

            int motorDepth = brain.GetLength(1) - 1; // Motors are placed at bottom row of brain

            int leftMotorFirstX = 0;
            int leftMotorLastX = motorLength - 1;

            int rightMotorFirstX = brain.GetLength(0) - motorLength;
            int rightMotorLastX = brain.GetLength(0) - 1;

            for (int i = leftMotorFirstX; i <= leftMotorLastX; i++)
            {
                brain[i, motorDepth].SetAsMotorNeuron();
                leftMotorNeurons.Add(brain[i, motorDepth]);
            }

            for (int i = rightMotorFirstX; i <= rightMotorLastX; i++)
            {
                brain[i, motorDepth].SetAsMotorNeuron();
                rightMotorNeurons.Add(brain[i, motorDepth]);
            }

        }

        private void DefineOffsets() // Define the x, y position of the eyes
        {
            // Find the x and y origin of the brain
            PointF brainOrigin = new PointF(
                (float)brain.GetLength(0) / 2f,
                (float)brain.GetLength(1) / 2f);

            // Find the average position of the left eye neurons

            float sumX = 0f;
            float sumY = 0f;
            foreach(Neuron neuron in leftEyeNeurons)
            {
                sumX = sumX + neuron.brainPositionX;
                sumY = sumY + neuron.brainPositionY;
            }

            PointF leftEyePosition = new PointF(
                sumX / leftEyeNeurons.Count,
                sumY / leftEyeNeurons.Count);

            // Find the average position of the right eye neurons

            sumX = 0f;
            sumY = 0f;
            foreach (Neuron neuron in rightEyeNeurons)
            {
                sumX = sumX + neuron.brainPositionX;
                sumY = sumY + neuron.brainPositionY;
            }

            PointF rightEyePosition = new PointF(
                sumX / rightEyeNeurons.Count,
                sumY / rightEyeNeurons.Count);

            // Find the offset

            leftEyeOffset = new PointF(
                -brainOrigin.X + leftEyePosition.X,
                -brainOrigin.Y + leftEyePosition.Y);

            rightEyeOffset = new PointF(
                -brainOrigin.X + rightEyePosition.X,
                -brainOrigin.Y + rightEyePosition.Y);

            // Find the average position of the left motor neurons

            sumX = 0f;
            sumY = 0f;
            foreach(Neuron neuron in leftMotorNeurons)
            {
                sumX = sumX + neuron.brainPositionX;
                sumY = sumY + neuron.brainPositionY;
            }

            PointF leftMotorPosition = new PointF(
                sumX / leftMotorNeurons.Count,
                sumY / leftMotorNeurons.Count);

            // Find the average position of the right motor neurons

            sumX = 0f;
            sumY = 0f;
            foreach (Neuron neuron in rightMotorNeurons)
            {
                sumX = sumX + neuron.brainPositionX;
                sumY = sumY + neuron.brainPositionY;
            }

            PointF rightMotorPosition = new PointF(
                sumX / rightMotorNeurons.Count,
                sumY / rightMotorNeurons.Count);

            // Find the offset

            leftMotorOffset = new PointF(
                -brainOrigin.X + leftMotorPosition.X,
                -brainOrigin.Y + leftMotorPosition.Y);

            rightMotorOffset = new PointF(
                -brainOrigin.X + rightMotorPosition.X,
                -brainOrigin.Y + rightMotorPosition.Y);
        }

        public void Update() // Tell brain to think
        {
            Look();

            FireConnections();

            SetFinalConnectionStrength();

            Move();

            CheckFoodCollision();

        }

        private void Look() // Tell eyes to look
        {   
            // Create list for each eye of food positions in relation to it.

            float maxViewDistance = Utility.Distance(0, 0, world.worldXSize, world.worldYSize);

            List<Utility.Destination> leftEyeDestinations = new List<Utility.Destination>(); // Determine position and direction of food just once to save on performance.
            List<Utility.Destination> rightEyeDestinations = new List<Utility.Destination>();

            float radRotation = Utility.DegToRad(rotation);

            // Work out absolute position of eye factoring in rotation of entity

            PointF leftEyePosition = Utility.CalculateOffsetPosition(position, leftEyeOffset, rotation);
            PointF rightEyePosition = Utility.CalculateOffsetPosition(position, rightEyeOffset, rotation);

            foreach(Food food in world.foodList) // Create a list of the location and distance of each food object from each eye
            {
                // Distance and direction from left eye
                float leftEyeDistance = Utility.Distance(leftEyePosition.X, leftEyePosition.Y, food.position.X, food.position.Y);
                float leftEyeDirection = Utility.Direction(leftEyePosition.X, leftEyePosition.Y, food.position.X, food.position.Y);

                // Distance and direction from right eye
                float rightEyeDistance = Utility.Distance(rightEyePosition.X, rightEyePosition.Y, food.position.X, food.position.Y);
                float rightEyeDirection = Utility.Direction(rightEyePosition.X, rightEyePosition.Y, food.position.X, food.position.Y);

                // Convert rotation to -180,+180 format and deduct absolute direction
                leftEyeDirection = Utility.RotationFormat180(leftEyeDirection - rotation); 
                rightEyeDirection = Utility.RotationFormat180(rightEyeDirection - rotation);

                // Add destination for this food item to list
                leftEyeDestinations.Add(new Utility.Destination(leftEyeDistance, leftEyeDirection));
                rightEyeDestinations.Add(new Utility.Destination(rightEyeDistance, rightEyeDirection));
            }

            // Pass the list to the neurons and activat the look method
            foreach (Neuron neuron in leftEyeNeurons)
            {
                neuron.Look(leftEyeDestinations, maxViewDistance);
            }

            foreach (Neuron neuron in rightEyeNeurons)
            {
                neuron.Look(rightEyeDestinations, maxViewDistance);
            }
        }

        private void FireConnections() // Fire connections for all neurons, except motor neurons
        {
            Parallel.ForEach(brain.Cast<Neuron>(), neuron =>
                {
                    if (!neuron.isMotorNeuron) neuron.FireConnections();
                });
        }

        private void SetFinalConnectionStrength() // All connections now received, set final connection strength, except for eye neurons
        {
            foreach (Neuron neuron in brain)
            {
                if (!neuron.isEyeNeuron) neuron.SetFinalConnectionStrength();
            }
        }

        private void Move() // Translate motor neuron output to movement
        {
            float rightMotorTotal = 0f;

            foreach (Neuron neuron in rightMotorNeurons)
            {
                rightMotorTotal += neuron.finalConnectionStrength;
            }

            float leftMotorTotal = 0f;

            foreach (Neuron neuron in leftMotorNeurons)
            {
                leftMotorTotal += neuron.finalConnectionStrength;
            }

            // Set motor impulse left and right based on average neuron output

            leftMoveImpulse = leftMotorTotal / (float)leftMotorNeurons.Count();
            rightMoveImpulse = rightMotorTotal / (float)rightMotorNeurons.Count();

            rotation += leftMoveImpulse - rightMoveImpulse;

            float speed = leftMoveImpulse + rightMoveImpulse;

            float x = (float)Math.Cos(Utility.DegToRad(rotation));
            float y = (float)Math.Sin(Utility.DegToRad(rotation));

            position.X += x * speed;
            position.Y += y * speed;

            // Wrap position

            if (position.X > world.worldXSize) position.X -= world.worldXSize;
            if (position.Y > world.worldYSize) position.Y -= world.worldYSize;

            if (position.X < 0f) position.X += world.worldXSize;
            if (position.Y < 0f) position.Y += world.worldYSize;

            // Set rotation between 0 and 360

            rotation = Utility.RotationFormat360(rotation);
        }

        private void CheckFoodCollision() // Check for food collisions
        {
            // Food eaten count in this step
            int foodEatenThisStep = 0;

            // Work out collision distance
            float foodAverageRadius = (world.foodSize.X + world.foodSize.Y) / 2;
            float entityAverageRadius = (float)(brain.GetLength(0) + brain.GetLength(1)) / 2;
            float collisionDistance = foodAverageRadius + entityAverageRadius;

            // Set closest distance to maximum possible distance in world
            float closestDistance = Utility.Distance(0f, 0f, world.worldXSize, world.worldYSize);

            // Step through each food item and detect collisions
            foreach (Food food in world.foodList)
            {
                float distance = Utility.Distance(position.X, position.Y, food.position.X, food.position.Y);
                if (distance <= collisionDistance) 
                {
                    world.DestroyFood(food);
                    foodEaten++;
                }

                if (distance < closestDistance) closestDistance = distance;
            }

            UpdatePerforamce(closestDistance, foodEatenThisStep);
        }

        private void UpdatePerforamce(float closestDistance, int foodEatenThisStep) // Update performance variables
        {
            if (totalSteps == 0) // If very first step
            {
                previousDistance = closestDistance;
            }

            totalSteps++;

            foodEaten = foodEaten + foodEatenThisStep; // Increment master count of food eaten based on this step

            if (!isFoodEatenLastStep) // If no food was eaten in the last step update the average distance gained
            {
                double previousGainedTotal = distanceGainedPerStepAverage * (totalSteps - 1);
                double distanceGainedThisStep = previousDistance - closestDistance;

                distanceGainedPerStepAverage = (previousGainedTotal + distanceGainedThisStep) / totalSteps;

                previousDistance = closestDistance;
            }
            else // If food was eaten last step, do not compare distance with previous step, but restart from current closest distance
            {
                previousDistance = closestDistance;
            }

            if (foodEatenThisStep > 0)
            {
                isFoodEatenLastStep = true;
            }
            else
            {
                isFoodEatenLastStep = false;
            }

        }
    }
}
