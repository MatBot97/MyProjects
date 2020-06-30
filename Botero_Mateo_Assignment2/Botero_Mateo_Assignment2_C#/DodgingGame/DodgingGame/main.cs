using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DodgingGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class main : Game
    {
        GraphicsDeviceManager graphics;                                                                             //variable that is going to manage the graphics       
        SpriteBatch spriteBatch;                                                                                    //variable that is managing the sprites

        private Texture2D BackGroundSprite;                                                                         //sprite for the BG of the screens
        private Texture2D MainMenuScreenSprite;                                                                     //sprite for the BG of the screens
        private Texture2D GameOverScreenSprite;                                                                     //sprite for the BG of the screens
        private Texture2D HighScoreScreenSprite;                                                                    //sprite for the BG of the screens
        private Texture2D [] SpriteSheetRight;                                                                      //array that is going to store the animation sprites
        private Texture2D [] SpriteSheetLeft;                                                                       //array that is going to store the animation sprites
        private Texture2D [] SpriteSheetIdle;                                                                       //array that is going to store the animation sprites
        private Texture2D ObstacleSprite;                                                                           //sprite for the obstacle

        private SpriteFont Font;                                                                                    //Font for the game

        private MainMenuScreen mainMenuScreen;                                                                      //variable that contains the information of the screen
        private GameOver gameOver;                                                                                  //variable that contains the information of the screen
        private GameScreen gameScreen;                                                                              //variable that contains the information of the screen
        private HighScores highScores;                                                                              //variable that contains the information of the screen

        public int PointsSaver;                                                                                     //int that is having the points
        public bool LoadPointsHighScore;                                                                            //bool that is checking if the hich scores are loaded

        private KeyboardState keyBoard; 

        public enum GameState { MainMenu, GameScreen, GameOver, HighScore, Exit}                                    //enum for the game states including exit the app
        public GameState gameState;                                                                                 //variable for the gamestates
        public main()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1920;                                                          //size of the screen
            this.graphics.PreferredBackBufferHeight = 1080;                                                         //size of the screen
            this.Window.Title = "Bones the dodging Skeleton";                                                       //name of the window
            this.graphics.ApplyChanges();
            LoadPointsHighScore = false;                                                                            //assigning false to the bool
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameState = GameState.MainMenu;                                                                         //assigning a game state to tthe variable
            this.spriteBatch = new SpriteBatch(GraphicsDevice);                                                      
            this.mainMenuScreen = new MainMenuScreen(this);                                                         //initializing the screens
            this.gameScreen = new GameScreen(this);                                                                 //initializing the screens
            this.gameOver = new GameOver(this);                                                                     //initializing the screens
            this.highScores = new HighScores(this);                                                                 //initializing the screens

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);                                                          // Create a new SpriteBatch

            this.Font = this.Content.Load<SpriteFont>("FontGame");                                                  //loading the font
            this.HighScoreScreenSprite = this.Content.Load<Texture2D>("HighScores");                                //loading the BG for the screen
            this.GameOverScreenSprite = this.Content.Load<Texture2D>("FinalScreen");                                //loading the BG for the screen
            this.MainMenuScreenSprite = this.Content.Load<Texture2D>("MainScreen");                                 //loading the BG for the screen
            this.BackGroundSprite = this.Content.Load<Texture2D>("BackGroundGame");                                 //loading the BG for the screen
            this.ObstacleSprite = this.Content.Load<Texture2D>("obstacle");                                         //loading the sprite for the obstacle
            this.SpriteSheetRight = new Texture2D[18];                                                              //assigning the size of the array for the animations
            this.SpriteSheetLeft = new Texture2D[18];                                                               //assigning the size of the array for the animations
            this.SpriteSheetIdle = new Texture2D[18];                                                               //assigning the size of the array for the animations

            string number_sprite = "";                                                                              //local string that is currently empty
            for (byte counter = 0; counter < 18; counter++)                                                         //loop that is going to be executes 18 times
            {

                number_sprite = "Walking_right" + counter;                                                          //assigning the name of the image to the sprite
                SpriteSheetRight[counter] = this.Content.Load<Texture2D>(number_sprite);

                number_sprite = "Walking_left" + counter;                                                           //assigning the name of the image to the sprite
                SpriteSheetLeft[counter] = this.Content.Load<Texture2D>(number_sprite);

                number_sprite = "Idle_" + counter;                                                                  //assigning the name of the image to the sprite
                SpriteSheetIdle[counter] = this.Content.Load<Texture2D>(number_sprite);
            }

            mainMenuScreen.LoadSprites(MainMenuScreenSprite);                                                       //passing all the sprites to load in the main menu
            gameScreen.LoadSprites(BackGroundSprite, ObstacleSprite, SpriteSheetLeft, SpriteSheetRight, SpriteSheetIdle, Font);//passing all the sprites to load on the screen
            gameOver.LoadSprites(GameOverScreenSprite, Font);                                                       //passing all the sprites to load in the corresponding screen
            highScores.LoadSprites(HighScoreScreenSprite, Font);                                                    //passing all the sprites to load in the corresponding screen
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyBoard = Keyboard.GetState();                                                                         
            if ( keyBoard.IsKeyDown(Keys.Escape))                                                                   //if the key esc is pressed then exit the app
                Exit();

            switch (gameState)                                                                                      //switch between the game states
            {
                case GameState.MainMenu:                                                                            //if the game state is main menu
                    mainMenuScreen.Update(keyBoard);                                                                //calling the update method from the screen
             
                    break;

                case GameState.GameScreen:                                                                          //if the game state is game screen
                    gameScreen.Update(keyBoard, (float)gameTime.ElapsedGameTime.TotalSeconds);                      //calling the update method from the screen

                    break;

                case GameState.GameOver:                                                                            //of the game screen is game over
                    gameOver.Update(keyBoard);                                                                      //calling the update method from the screen

                    break;

                case GameState.HighScore:                                                                           //if the game screen is high score
                    highScores.Update(keyBoard);                                                                    //calling the update method from the screen

                    break;
            }



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.MainMenu:                                                                            //if the game state is main menu
                    mainMenuScreen.Draw(spriteBatch);                                                               //call the draw methodfrom the given class

                    break;

                case GameState.GameScreen:                                                                          //if the game state isgame screen
                    gameScreen.Draw(spriteBatch);                                                                   //call the draw methodfrom the given class

                    break;

                case GameState.GameOver:                                                                            //if the game state is game over
                    gameOver.Draw(spriteBatch);                                                                     //call the draw methodfrom the given class

                    break;

                case GameState.HighScore:                                                                           //if the game state is high score
                    highScores.Draw(spriteBatch);                                                                   //call the draw methodfrom the given class

                    break;
            }
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
