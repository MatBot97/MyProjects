using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DodgingGame
{
    class GameOver
    {

        private int points = 0;																							//int that is going to store the points

        Texture2D texture;																								//texture that is going to display the screen
        main main;																								        //a reference to the class methods

        SpriteFont Font;																							    //declare the font

        public GameOver(main mainPointer)													        					//passing parameters points and game
        {
            main = mainPointer;                                                                                         //instantiating that this.game is the game from this class

        }

        public void LoadSprites(Texture2D sprites, SpriteFont Font)                                                     //loading the sprites and font
        {
            texture = sprites;
            this.Font = Font;

        }

        public void Update(KeyboardState keyBoard)
        {
            if (keyBoard.IsKeyDown(Keys.Enter))										    								//if user press the key enter then go to highscore screen
            {
                main.gameState = main.GameState.HighScore;							            						//passing points to the highscore screen
                main.LoadPointsHighScore = true;                                                                        //setting the bool to true
            }
            points = main.PointsSaver;                                                                                  //setting the variable points with the value from main.PointsSaver
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);													//draw the texture that contains the BG in the pos 0,0

            spriteBatch.DrawString(Font, "Final Score: " + points, new Vector2(760, 600), Color.White, 0, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);//drawing the Font

        }

     

    }
}
