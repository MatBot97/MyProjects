using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DodgingGame
{
    class MainMenuScreen
    {
        Texture2D texture;                                                                                          //sprite that is going to display the BG
        main main;                                                                                                  //a reference to the class main


        public MainMenuScreen(main mainPointer)                                                                     //passing a parameter that contains the information 
                                                                                                                    //from main class
        {
            main = mainPointer;
        }
        public void LoadSprites(Texture2D sprites)                                                                  //parameter sprite that is type texture2D                                                                
        {
            texture = sprites;

        }

        public void Update(KeyboardState keyBoard)
        {
            if (keyBoard.IsKeyDown(Keys.Enter))																	    //if user press key enter then
            {
                main.gameState = main.GameState.GameScreen;													    	//open the gameScreen
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);                                              //draw the BG
        }
    }
}
