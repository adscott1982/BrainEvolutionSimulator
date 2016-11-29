using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace Brain_Evolution_Simulator
{
    public class World
    {
        // Entity and brain settings
        public int brainXSize = 60;
        public int brainYSize = 60;
        public int entityEyeLength = 20;
        public int eyeFieldOfView = 180;
        public int entityMotorLength = 20;
        public int entityConnectionStrengthResolution = 1000;
        public float entityConnectionChance = 0.95f;
        public int maxConnections = 100;
        
        public List<Entity> entityList = new List<Entity>(); // Create list to hold references to Entity objects
        public List<Food> foodList = new List<Food>(); // Create list to hold references to Food objects

        // World settings
        public int worldXSize = 4000;
        public int worldYSize = 4000;
        public int distanceToFoodMin = 800;
        public PointF foodSize = new PointF(25f, 25f);

        Random random = new Random(); // Create reference to Random to be used for all entities

        public World() // Basic constructor
        {

        }

        public World(int xSize, int ySize) // Override world size
        {
            worldXSize = xSize;
            worldYSize = ySize;
        }

        public World(int xSize, int ySize, int brainXSize, int brainYSize) // Override world size and brain size
        {
            worldXSize = xSize;
            worldYSize = ySize;
            this.brainXSize = brainXSize;
            this.brainYSize = brainYSize;
        }

        public void CreateEntity() // Method to create entity using default values
        {
            // Determine position and rotation of entity in world;
            float rotation = random.Next(360);
            PointF position = PositionEntity();

            entityList.Add(new Entity(brainXSize, brainYSize, entityEyeLength, random, entityConnectionStrengthResolution, entityConnectionChance, entityMotorLength, maxConnections, position, rotation, eyeFieldOfView, this)); // Create new entity with default values and add to list
            Debug.WriteLine(string.Format("Entity created at X: {0} Y: {1}", position.X, position.Y));
        }

        public void DestroyEntity(Entity entity) // Destroy an entity
        {

        }

        public void CreateFood() // Method to create food
        {
            PointF position = PositionFood();
            foodList.Add(new Food(position, foodSize));
            Debug.WriteLine(string.Format("Food created at X: {0} Y: {1}", position.X, position.Y));
        }

        public void DestroyFood(Food food) // Destroy a food item
        {
            // Destroy food here
            // Create new food item in random position
        }

        public void Update() // World iterate one step
        {
            foreach (Entity entity in entityList)
            {
                entity.Update();
            }
        }

        public PointF PositionEntity() // Position entity in the world a sufficient distance from any food items
        {
            bool positionBad = true;
            PointF position = new PointF();

            while (positionBad) // Create random positions until the position is determined to be good
            {
                position.X = random.Next(worldXSize);
                position.Y = random.Next(worldYSize);

                // Check if creation position is too close to food
                float foodDistance = distanceToFoodMin + 1;
                foreach (Food food in foodList)
                {
                    float distance = Utility.Distance(position.X, position.Y, food.position.X, food.position.Y);

                    if (distance < foodDistance) foodDistance = distance;
                }

                if (foodDistance > distanceToFoodMin) positionBad = false;
            }

            return position;
        }

        public PointF PositionFood() // Position food in the world a sufficient distance from any Entities
        {
            bool positionBad = true;
            PointF position = new PointF();

            while (positionBad) // Create random positions until the position is determined to be good
            {
                position.X = random.Next(worldXSize);
                position.Y = random.Next(worldYSize);

                // Check if creation position is too close to food
                float entityDistance = distanceToFoodMin + 1;
                foreach (Entity entity in entityList)
                {
                    float distance = Utility.Distance(position.X, position.Y, entity.position.X, entity.position.Y);

                    if (distance < entityDistance) entityDistance = distance;
                }

                if (entityDistance > distanceToFoodMin) positionBad = false;
            }

            return position;
        }
    }
}
