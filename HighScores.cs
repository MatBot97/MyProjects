using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using Newtonsoft.Json;


namespace DodgingGame
{
    class HighScores
    {
        Texture2D texture;                                                                                      //texture variable 
        main main;                                                                                              //variable that is storing the info from 
                                                                                                                //the main class
        SpriteFont Font;																						//variable for the Font
        private int points;																						//int that is going to store the points
        private bool keyPressed;                                                                                //boll that is going to track if the key is pressed

        class SavingScores                                                                                      //class that is going to have the binary file
        {               
           public ArrayList HighScoresPoints;																	//array list that is going to store the 5 best scores

            public SavingScores()																				//constructor that is declaring the array
            {
                HighScoresPoints = new ArrayList();
            }
        }
        SavingScores savingScores;                                                                              //saving scores contains the information 
                                                                                                                //of the class SavingScores

        public HighScores(main MainValue)
        {
            main = MainValue;                                                                                   //passing the information that the constructor
                                                                                                                //is receiving regarding to main to the variable MainValue 
            savingScores = new SavingScores();                                                                  //saying that savingScores is going to call a 
                                                                                                                //new method savingScores()
            keyPressed = false;                                                                                 //assigning false to the bool
        }

        public void LoadSprites(Texture2D sprites, SpriteFont Font)                                             //method that loads the textures
        {
            texture = sprites;
            this.Font = Font;

        }

        public void Update(KeyboardState keyBoard)                                                              //update method that receive keys from the keyboard
        {
            points = main.PointsSaver;                                                                          //giving the variable the points from the points saver

            if (main.LoadPointsHighScore)                                                                       //if the variable is true
            {
                main.LoadPointsHighScore = false;                                                               //setting the variable to false

                try                                                                                             //exception handling
                {
                    savingScores = JsonConvert.DeserializeObject<SavingScores>(File.ReadAllText("HighScores.json"));//saving the variable in the file that is converted to json file
                }
                catch (System.Exception)                                                                        //catch block
                {
                    Console.WriteLine("ERROR WITH THE FILE");                                                   //if there is a problem with the file display
                }
            }

            if (keyBoard.IsKeyDown(Keys.Escape))																//if the key esc is pressed exit the app
            {
                main.gameState = main.GameState.Exit;
            }

            if (keyBoard.IsKeyDown(Keys.S)&& !keyPressed)														//if the key S is pressed and the bool is false then
            {
                keyPressed = true;                                                                              //set the bool to true
                savingScores.HighScoresPoints.Add((double)points);                                              //adding the points to the array
                savingScores.HighScoresPoints.Sort();                                                           //sorting the array
                if (savingScores.HighScoresPoints.Count > 5)                                                    //if the size of the array is greater than 5
                {
                    savingScores.HighScoresPoints.RemoveAt(0);                                                  //remove the lowest score
                }
                string JsonSaver = JsonConvert.SerializeObject(savingScores);                                   //saving the array in the file

                File.WriteAllText("HighScores.json", JsonSaver);                                                //name of the file
            }

        }

        public void Draw(SpriteBatch spriteBatch)                                                               //draw method
        {

            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);                                          //drawing the BG in the pos 0,0

            for (byte counter = (byte)(savingScores.HighScoresPoints.Count -1); counter >= 0; counter--)        //for that is going to draw the high scores
            {
                spriteBatch.DrawString(Font, "" + savingScores.HighScoresPoints[counter],                       //using the font to draw the value of the array
                    new Vector2(880, (700 - 70 * counter)),                                                     //in the index of the value counter
                    Color.White, 0, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
            }


        }


    }
}
