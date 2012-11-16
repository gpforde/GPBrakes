using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BEPUphysics;
using BEPUphysics.Entities.Prefabs;

namespace BrakingSystem
{
 
    public class XNAGame : Microsoft.Xna.Framework.Game
    {
        
      //  public Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        static XNAGame instance = null;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       // private Ground ground;
        Space space;
        Random random = new Random();
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }
        private Camera camera;
        List<Entity> children = new List<Entity>();

        public List<Entity> Children
        {
            get { return children; }
            set { children = value; }
        }

        private Ground ground = null;

        public Ground Ground
        {
            get { return ground; }
            set { ground = value; }
        }

        public static XNAGame Instance()
        {
            return instance;
        }

        public XNAGame()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferMultiSampling = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
          
            camera = new Camera();
            int midX = GraphicsDeviceManager.DefaultBackBufferHeight / 2;
            int midY = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            Mouse.SetPosition(midX, midY);
                           
            children.Add(camera);
            ground = new Ground();                        
            children.Add(ground);
            
         
            SlaveCylinder slavecylinder = new SlaveCylinder();
            slavecylinder.pos.X = 3; slavecylinder.pos.Y = 10; slavecylinder.pos.Z = 30;
            children.Add(slavecylinder);


            SlavePiston slavepiston = new SlavePiston();
            slavepiston.pos.X = 3; slavepiston.pos.Y = 10; slavepiston.pos.Z = (float)30.5;
            children.Add(slavepiston);

            SlavePiston slavepiston2 = new SlavePiston();
            slavepiston2.pos.X = 3; slavepiston2.pos.Y = 10; slavepiston2.pos.Z = 30;
            children.Add(slavepiston2);

            Chassis chassis = new Chassis();
            chassis.pos.X = 0; chassis.pos.Y = 10; chassis.pos.Z = 30;
            children.Add(chassis);

            Hub hub = new Hub();
            hub.pos.X = (float)3.25; hub.pos.Y = 10; hub.pos.Z = (float)29.25;
            children.Add(hub);

            BrakeShoeLeft brakeshoeleft = new BrakeShoeLeft();
            brakeshoeleft.pos.X = (float)3.3; brakeshoeleft.pos.Y = (float)9.25; brakeshoeleft.pos.Z = (float) 29.25;
            children.Add(brakeshoeleft);

            BrakeShoeRight brakeshoeright = new BrakeShoeRight();
            brakeshoeright.pos.X = (float)3.3; brakeshoeright.pos.Y = (float)9.9; brakeshoeright.pos.Z = (float)29.25;
            children.Add(brakeshoeright);

            base.Initialize();
        }
        void createTower()
        {
            /*
            for (float y = 70; y > 20; y -= 5)
            {
                createCylinder(new Vector3(0, y, 0), 0, 0, 0);
            }*/
            for (float y = 70; y > 20; y -= 5)
            {
                createCylinder(new Vector3(0, y, 0), 0, 0, 0);
            }
            for (float y = 70; y > 20; y -= 5)
            {
                createShoeLeft(new Vector3(0, y, 0), 0, 0, 0);
            }
            for (float y = 70; y > 20; y -= 5)
            {
                createShoeRight(new Vector3(0, y, 0), 0, 0, 0);
            }
            
            
        }
        void createCylinder(Vector3 position, float width, float height, float length)
        {        
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "SlaveCylinder";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
        }
        void createShoeLeft(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "brakeshoeleft2";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
        }
        void createShoeRight(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "brakeshoeright3";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
        }
       protected override void LoadContent()
        {
       
            spriteBatch = new SpriteBatch(GraphicsDevice);
            space = new Space();
            space.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0);
            Box groundBox = new Box(Vector3.Zero, ground.width, 0.1f, ground.height);
            space.Add(groundBox);
          // space.Add(fireBall);

            createTower();
           
            foreach (Entity child in children)
            {
                child.LoadContent();
            }

        }
  
        protected override void UnloadContent()
        {
            foreach (Entity child in children)
            {
                child.UnloadContent();
            }

        }

        protected override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

    

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Update(gameTime);
            }

            space.Update(timeDelta);

       
            base.Update(gameTime);
        }

       protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            foreach (Entity child in children)
            {
                DepthStencilState state = new DepthStencilState();
                state.DepthBufferEnable = true;                
                GraphicsDevice.DepthStencilState = state;
                child.Draw(gameTime);
            }

            spriteBatch.End();            
        }

        public Camera Camera
        {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            }
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return graphics;
            }
        }
    }
}
