using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System;

namespace DodgingGame
{
    class GameScreen
    {

        private float x_coordinate;                                                                                 //x coordinate of the character
        private float y_coordinate;                                                                                 //y coordinate of the character
        private short speed = 200;                                                                                  //speed of the character
        private byte current_sprite = 0;                                                                            //byte that is going to switch between sprites
        private bool right = false;                                                                                 //boolean that is checking if the right key is pressed
        private bool left = false;                                                                                  //boolean that is checking if the left key is pressed
        private int points = 0;                                                                                     //short that is saving the points
        private byte lives = 5;                                                                                     //byte that is saving the lives of the player
        private float gravity = -2;                                                                                 //float that is going to keep track of the gravity	
        private float speedSpawingObstacles = 0.9f;                                                                 //float that is going to be used to set the speed of the spawn of the obstacles
        private float spawningTime = 0;                                                                             //variable that is going to have the spawning time
                                                                                                                    //of the obstacles

        private Random random;                                                                                      //random variable


        Texture2D Background, obstacle;																				//texture for the BG and the obstacle
        Texture2D[] characterLeft, characterRight, characterIdle;                                                   //texture array that contains the animation for
                                                                                                                    //walking right, walking left and staying
        SpriteFont Font;																					        //variable for the Font

        ArrayList obstacles;																	                    //array of obstacles
        main main;

        public GameScreen(main mainValue)
        {
            main = mainValue;																						//assigning main information to the mainValue
            random = new Random();                                                                                  //new random
            obstacles = new ArrayList();
            x_coordinate = 850;                                                                                     //setting the intials coordinate for x
            y_coordinate = 1050;                                                                                    //setting the intials coordinate for y
        }
        public void LoadSprites(Texture2D Background, Texture2D obstacles, Texture2D[] characterLeft,
            Texture2D[] characterRight, Texture2D[] characterIdle, SpriteFont Font)
        {
            this.Background = Background;                                                                           //assigning the value to the variable
            this.obstacle = obstacles;                                                                              //assigning the value to the variable
            this.characterLeft = characterLeft;                                                                     //assigning the value to the variable
            this.characterRight = characterRight;                                                                   //assigning the value to the variable
            this.characterIdle = characterIdle;                                                                     //assigning the value to the variable
            this.Font = Font;                                                                                       //assigning the value to the variable
        }

        public void Update(KeyboardState keyBoard, float delta)
        {

            if (keyBoard.IsKeyDown(Keys.Left)) left = true;														    //if the key Left is pressed then the boolean left is true
            else left = false;																						//else the boolean is false

            if (keyBoard.IsKeyDown(Keys.Right)) right = true;													    //if the key right is pressed then the boolean right is true
            else right = false;																						//else the boolean is false

            if (keyBoard.IsKeyDown(Keys.Up)) gravity = -10;                                                         //if the key UP is pressed the gravity changes simulating 
                                                                                                                    //the character to fly

            if (left && !right && x_coordinate > -20) x_coordinate -= (speed * delta);                              //if the left is true and right is false and the x_coordinate
                                                                                                                    //is greater than -20 then x_coordinate is going to change 
                                                                                                                    //according to the speed

            if (right && !left && x_coordinate < 1750) x_coordinate += (speed * delta);                             //if the right is true and the left is false and the x_coordinate
                                                                                                                    //is less than 1750  then the character 
                                                                                                                    //is moving right at the speed of the variable speed
            y_coordinate += gravity;																				//y_coordinate is always being affected by the gravity

            if (y_coordinate < -20)																				    //if the y_coordinate is less than -20 then stay at -20
            {
                y_coordinate = -20;
            }
            else if (y_coordinate > 800)																			//boundary of the ground 
            {
                y_coordinate = 800;
            }

            gravity += 50 * delta;																					//value of the gravity


            current_sprite++;																						//adding 1 to the current sprite 
            if (current_sprite == 18) current_sprite = 0;                                                           //when the current sprite is 18 then it starts again to 
                                                                                                                    //simulate animation

            spawningTime += delta;																					//spawning time tracker

            if (spawningTime >= speedSpawingObstacles)                                                              //if the spawning time is greater or equals to the 
            {                                                                                                       //speedSpawingObstacles then spawning time = 0


                spawningTime = 0;

                switch (random.Next(3))																		        //switch that is randomly generating the obstacles
                {

                    case 0: obstacles.Add(new Obstacles(new float[] { -110, 										//first case is generating the obstacle on the left side
                        (short)random.Next(900) }, (byte)1)); break;

                    case 1: obstacles.Add(new Obstacles(new float[] { 1970, 										//second case is generating the obstacles on the right side
                        (short)random.Next(900) }, (byte)2)); break;

                    case 2: obstacles.Add(new Obstacles(new float[] { (short)random.Next(1920), 				    //third case is generating the obstacles on top of the screen
                        -110 }, (byte)3)); break;

                }

            }

            foreach (Obstacles counterObstacles in obstacles.ToArray())                                             //for that is updating the obstacles according to the array
                                                                                                                    //of obstacles 

            {
                counterObstacles.updateObstacle(delta * speed);                                                     //updating the obstacles on the position of the counter and 
                                                                                                                    //giving movement with the speed and delta time

                switch (counterObstacles.getDirection())				                                    		//giving the coordinates with the getter
                {

                    case 1:

                        if (counterObstacles.getX_Coordinate()[0] > 1970)										    //if the obstacles was moving to the right and is touching
                        {
                            obstacles.Remove(counterObstacles);													    //the invisible boundary of the screen then remove it

                            points += 10;																			//and add 10 to the variable points
                        }

                        break;

                    case 2:

                        if (counterObstacles.getX_Coordinate()[0] < -70)										    //if the obstacles was moving to the left and is touching
                        {
                            obstacles.Remove(counterObstacles);											    		//the invisible boundary of the screen then remove it

                            points += 10;																			//and add 10 to the variable points
                        }

                        break;

                    case 3:

                        if (counterObstacles.getX_Coordinate()[1] > 1100)										    //if the obstacles was moving to down and is touching
                        {
                            obstacles.Remove(counterObstacles);													    //the invisible boundary of the screen then remove i

                            points += 10;																			//and add 10 to the variable points
                        }

                        break;
                }

                if (Vector2.Distance(new Vector2(counterObstacles.getX_Coordinate()[0], counterObstacles.getX_Coordinate()[1]), 
                    new Vector2(x_coordinate, y_coordinate)) < 110)                                                 //collision between the character sprite and the obstacle
                                                                                                                    //is checking if the position of the obstacle and the 
                                                                                                                    //position of the player are touching in a range less than 110
                {

                    lives--;																						//if they are colliding then take 1 from the variable lives
                    obstacles.Remove(counterObstacles);										        				//remove the obstacle that collided with the character
                }

            }

            if (lives <= 0)																					    	//if the variable lives is 0 then
            {

                main.gameState = main.GameState.GameOver;												            //going to the next screen 
                main.PointsSaver = points;                                                                          //setting the pointsSaver to the value of points
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);

            if ((!right && !left) || (right && left))spriteBatch.Draw(characterIdle[current_sprite],                //if the player is not pressing any movement keys or if he
                                                                                                                    //is pressing both left and right then draw the idle animation
                new Rectangle((int)x_coordinate, (int)y_coordinate, 200, 200), Color.White);


            else if (right) spriteBatch.Draw(characterRight[current_sprite], new Rectangle((int)x_coordinate,       //if right is true then draw the animation for walking right
                (int)y_coordinate, 200, 200), Color.White);


            else if (left) spriteBatch.Draw(characterLeft[current_sprite], new Rectangle((int)x_coordinate,	        //if left is true then draw the animation for walking left
                (int)y_coordinate, 200, 200), Color.White);


            foreach (Obstacles counterObstacles in obstacles.ToArray())                                             //for that is drawing the obstacles according to the array
                                                                                                                    //of obstacles
            {
                spriteBatch.Draw(obstacle, new Rectangle((int)counterObstacles.getX_Coordinate()[0],			    //giving the coordinates with the getter and giving the 
                    (int)counterObstacles.getX_Coordinate()[1], 200, 200), Color.White);							//size of 200,200
            }


            spriteBatch.DrawString(Font, "Score: " + points, new Vector2(70, 100), Color.White, 0, 
                new Vector2 (0,0), 2, SpriteEffects.None,0);													    //drawing the current score with the variable points

            spriteBatch.DrawString(Font, "lives: " + lives, new Vector2(400, 100), Color.White ,0,
                new Vector2(0, 0), 2, SpriteEffects.None, 0);													    //drawing the lives 


        }
    }
}
