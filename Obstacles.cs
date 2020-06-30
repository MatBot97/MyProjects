using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgingGame
{
    class Obstacles
    {
        private float[] coordinateObstacle;                                                                 //array that is containing the coordinate X in the position 0
                                                                                                            //and the Y coordinate in the position 1                                                                            
        private byte directionObstacle;                                                                     //direction of the obstacle being going to the left or right of x 
                                                                                                            //or falling down from y

        public Obstacles(float[] coordinate, byte direction)												//passing the parameters by value
        {

            coordinateObstacle = coordinate;
            directionObstacle = direction;

        }
        public float[] getX_Coordinate()																	//getter method for the coordinates of the obstacle
        {

            return coordinateObstacle;
        }

        public byte getDirection()													    					//getter for the direction
        {

            return directionObstacle;
        }

        public void updateObstacle(float movement)															//update that is going to move the obstacle
        {

            switch (directionObstacle)																		//switch depending on the direction of the obstacle
            {

                case 1:                                                                                     //x movement left to right
                    coordinateObstacle[0] += movement;
                    break;

                case 2:                                                                                     //x movement right to left
                    coordinateObstacle[0] -= movement;
                    break;

                case 3:                                                                                     //y movement up to down
                    coordinateObstacle[1] += movement;
                    break;

            }

        }
    }

}
