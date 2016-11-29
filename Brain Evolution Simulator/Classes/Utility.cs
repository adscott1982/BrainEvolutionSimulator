using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Brain_Evolution_Simulator
{
    public static class Utility
    {
        public struct Connection // Struct to hold neural connection information
        {
            public int x, y; // x = x position of neural target, y = y position of neural target
            public float strength; // strength = strength of neural connection

            public Connection(int x, int y, float strength) // Sole public constructor, no overloads.
            {
                this.x = x;
                this.y = y;
                this.strength = strength;
            }
        }

        public struct Destination // Struct to hold distance and direction
        {
            public float distance;
            public float direction;

            public Destination(float distance, float direction)
            {
                this.distance = distance;
                this.direction = direction;
            }
        }

        public static float Distance(float x1, float y1, float x2, float y2) // Finds the distance between two 2D points
        {
            float xDifference = x1 - x2;
            float yDifference = y1 - y2;

            float distance = (float)Math.Sqrt((xDifference * xDifference) + (yDifference * yDifference)); // Perform pythagoras

            return distance;
        }

        public static float Direction(float x1, float y1, float x2, float y2) // Finds the direction between two 2D points
        {
            double direction = Math.Atan2(y2 - y1, x2 - x1) * (180.0 / Math.PI);
            return (float)direction;
        }

        public static float RadToDeg(float rotation) // Return a radian angle in degrees
        {
            rotation = rotation * (float)(180.0 / Math.PI);
            return rotation;
        }

        public static float DegToRad(float rotation) // Return an degree angle in radians
        {
            rotation = rotation / (float)(180.0 / Math.PI);
            return rotation;
        }

        public static float RotationFormat180(float rotation) // Convert rotation to -180 to +180 format
        {
            rotation = rotation % 360f;
            if (rotation > 180f) rotation = rotation - 360f;
            if (rotation < -180f) rotation = rotation + 360f;
            return rotation;
        }

        public static float RotationFormat360(float rotation) // Convert rotation to 0 to 360 format
        {
            rotation = rotation % 360f; // Perform modulus.
            if (rotation < 0) rotation = 360 - rotation;
            return rotation;
        }

        public static PointF CalculateOffsetPosition(PointF position, PointF offset, float rotation) // Return the position of an offset point, having received the origin position, rotation and offset
        {
            rotation = DegToRad(rotation);

            PointF newPosition = new PointF(
                ((float)Math.Cos(rotation) * offset.X) - ((float)Math.Sin(rotation) * offset.Y) + position.X,
                ((float)Math.Sin(rotation) * offset.X) + ((float)Math.Sin(rotation) * offset.Y) + position.Y);

            return newPosition;
        }

        public static void SaveEntity(Entity entity, string path) // Save entity to file
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH.mm ss.fff");

            string filename = string.Format("Entity {0}.ent", time);

            filename = Path.Combine(path, filename); // Set file path

            bool exists = System.IO.Directory.Exists(path);

            if (!exists)
                System.IO.Directory.CreateDirectory(path);

            string endSection = "*";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                file.WriteLine(string.Format("Brain Width {0}", entity.brain.GetLength(0)));
                file.WriteLine(string.Format("Brain Height {0}", entity.brain.GetLength(1)));
                file.WriteLine(endSection);

                foreach (Neuron neuron in entity.leftEyeNeurons) // List left eye neuron positions
                {
                    file.WriteLine(string.Format("LeftEye {0} {1}", neuron.brainPositionX, neuron.brainPositionY));
                }

                file.WriteLine(endSection);

                foreach (Neuron neuron in entity.rightEyeNeurons) // List right eye neuron positions
                {
                    file.WriteLine(string.Format("RightEye {0} {1}", neuron.brainPositionX, neuron.brainPositionY));
                }

                file.WriteLine(endSection);

                foreach (Neuron neuron in entity.leftMotorNeurons) // List left motor neuron positions
                {
                    file.WriteLine(string.Format("LeftMotor {0} {1}", neuron.brainPositionX, neuron.brainPositionY));
                }

                file.WriteLine(endSection);

                foreach (Neuron neuron in entity.rightMotorNeurons) // List right motor neuron positions
                {
                    file.WriteLine(string.Format("RightMotor {0} {1}", neuron.brainPositionX, neuron.brainPositionY));
                }

                file.WriteLine(endSection);

                foreach (Neuron neuron in entity.brain) // Write a section for every neuron in brain and list each connection
                {
                    file.WriteLine(string.Format("Neuron({0}, {1})", neuron.brainPositionX, neuron.brainPositionY));
                    file.WriteLine(string.Format("Threshold {0}", neuron.fireConnectionThreshold));
                    if(neuron.isEyeNeuron)
                        file.WriteLine(string.Format("Section Start {0}, Section End {1}", neuron.eyeSectionStart, neuron.eyeSectionEnd));

                    foreach (Utility.Connection connection in neuron.connectionList)
                    {
                        file.WriteLine(string.Format("{0} {1} {2}", connection.x, connection.y, connection.strength));
                    }

                    file.WriteLine(endSection);
                }
            }
        }

        public static Entity LoadEntity(string directory)
        {

            return null;
        }

        public static void DrawEntity(Entity entity, Graphics graphics, float ratioX, float ratioY) // Draw entity
        {
            // Set values for position and rotation of rectangle representing entity

            float width = entity.brain.GetLength(0); // Find width of brain, use for rectangle width
            float height = entity.brain.GetLength(1); // Find height of brain, use for rectangle height

            float positionX = entity.position.X;
            float positionY = entity.position.Y;

            float x = -width / 2;
            float y = -height / 2;
  
            float rotation = entity.rotation;

            // Set values to draw lines for eyes and motors

            PointF leftEyeStart = new PointF(x, y);
            PointF leftEyeEnd = new PointF(x + entity.leftEyeNeurons.Count - 1, y);

            PointF rightEyeStart = new PointF(x + width - entity.rightEyeNeurons.Count, y);
            PointF rightEyeEnd = new PointF(x + width, y);

            PointF leftMotorStart = new PointF(x, y + height);
            PointF leftMotorEnd = new PointF(x, y + height - entity.leftMotorNeurons.Count);

            PointF rightMotorStart = new PointF(x + width, y + height);
            PointF rightMotorEnd = new PointF(x + width, y + height - entity.rightMotorNeurons.Count);

            // Initialize transform to correct position and rotation

            graphics.TranslateTransform(positionX * ratioX, positionY * ratioY);

            string display = string.Format("Impulse L {0}, R {1}", entity.leftMoveImpulse, entity.rightMoveImpulse);
            display = string.Format(display + "\nAv Dist Gained {0}", entity.distanceGainedPerStepAverage);
            graphics.DrawString(display, new Font("Courier New", 9), Brushes.Purple, new PointF(10, 10));

            graphics.RotateTransform(rotation+90);

            // Draw filled rectangle

            graphics.DrawRectangle(
                Pens.Red,
                x * ratioX,
                y * ratioY,
                width * ratioX,
                width * ratioY);

            graphics.FillRectangle(
                Brushes.Red, 
                x * ratioX, 
                y * ratioY, 
                width * ratioX, 
                width * ratioY);

            // Draw eye lines

            graphics.DrawLine(
                Pens.DeepSkyBlue,
                leftEyeStart.X * ratioX,
                leftEyeStart.Y * ratioY,
                leftEyeEnd.X * ratioX,
                leftEyeEnd.Y * ratioY);

            graphics.DrawLine(
                Pens.DeepSkyBlue,
                rightEyeStart.X * ratioX,
                rightEyeStart.Y * ratioY,
                rightEyeEnd.X * ratioX,
                rightEyeEnd.Y * ratioY);

            // Draw motor lines

            graphics.DrawLine(
                Pens.Yellow,
                leftMotorStart.X * ratioX,
                leftMotorStart.Y * ratioY,
                leftMotorEnd.X * ratioX,
                leftMotorEnd.Y * ratioY);

            graphics.DrawLine(
                Pens.Yellow,
                rightMotorStart.X * ratioX,
                rightMotorStart.Y * ratioY,
                rightMotorEnd.X * ratioX,
                rightMotorEnd.Y * ratioY);

            
            // Reset transform for next drawing action

            graphics.ResetTransform();

        }

        public static void DrawFood(Food food, Graphics graphics, float ratioX, float ratioY) // Draw food
        {
            // Set values for position of rectangle representing food

            float width = food.size.X;
            float height = food.size.Y;

            float positionX = food.position.X;
            float positionY = food.position.Y;

            float x = -width / 2;
            float y = -height / 2;

            // Initialize transform to correct position

            graphics.TranslateTransform(positionX * ratioX, positionY * ratioY);

            // Draw filled rectangle

            graphics.DrawRectangle(
                Pens.Lime,
                x * ratioX,
                y * ratioY,
                width * ratioX,
                width * ratioY);

            graphics.FillRectangle(
                Brushes.Lime,
                x * ratioX,
                y * ratioY,
                width * ratioX,
                width * ratioY);

            // Reset transform for next drawing action

            graphics.ResetTransform();
        }
    }
}

//TODO Add metrics to judge success of entity
//TODO Implement entity mutation properly
//TODO Implement load entity


