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
using BEPUphysics.MathExtensions;
using BEPUphysics.Constraints.TwoEntity.Joints;
using BEPUphysics.Constraints.SolverGroups;
using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.DataStructures;

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
            slavecylinder.pos.X = (float) 3.25; slavecylinder.pos.Y = (float)10.5; slavecylinder.pos.Z = (float)28.8;
            children.Add(slavecylinder);


            SlavePiston slavepiston = new SlavePiston();
            slavepiston.pos.X = (float)3.25; slavepiston.pos.Y = (float)10.5; slavepiston.pos.Z = (float)28.8;
            children.Add(slavepiston);

            SlavePiston slavepiston2 = new SlavePiston();
            slavepiston2.pos.X = (float)3.25; slavepiston2.pos.Y = (float)10.5; slavepiston2.pos.Z = (float)29;
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
            brakeshoeright.pos.X = (float)3.05; brakeshoeright.pos.Y = (float)9.9; brakeshoeright.pos.Z = (float)29.25;
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
            
            for (float y = 1; y > 0; y -= 1)
            {
                createShoeRight(new Vector3(0, y, 0), "BrakeShoeRight23" ,1);
            }

            for (float y = 1; y > 0; y -= 1)
            {
                createShoeLeft(new Vector3(0, y, 0), "brakeshoeleft2", 1);
            }
            /*
            for (float y = 70; y > 20; y -= 5)
            {
                createShoeLeft(new Vector3(0, y, 0), 0, 0, 0);
            }
             */ 
            for (float y = 2; y > 1; y -= 1)
            {
                createPiston1(new Vector3(0, y, 0), 0, 0, 0);
            }
          //  for (float y = 70; y > 20; y -= 5)
          //  {
            //    createShoeLeft(new Vector3(0, y, 0), 0, 0, 0);
            //}
            /*
            for (float y = 70; y > 20; y -= 5)
            {
                createShoeRight(new Vector3(0, y, 0), 0, 0, 0);
            }
            */
            
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
            theBox.HasColor = true;
        }

        void createPiston1(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "slavepiston";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
        }
        /*
        void createShoeLeft(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "brakeshoeleft2";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
        }*/

        BepuEntity createShoeLeft(Vector3 position, string mesh, float scale)
        {
            BepuEntity entity = new BepuEntity();
            entity.modelName = mesh;
            entity.LoadContent();
            Vector3[] vertices;
            int[] indices;
            TriangleMesh.GetVerticesAndIndicesFromModel(entity.model, out vertices, out indices);
            AffineTransform localTransform = new AffineTransform(new Vector3(scale, scale, scale), Quaternion.Identity, new Vector3(0, 0, 0));
            MobileMesh mobileMesh = new MobileMesh(vertices, indices, localTransform, BEPUphysics.CollisionShapes.MobileMeshSolidity.Counterclockwise, 1);
            entity.localTransform = Matrix.CreateScale(scale, scale, scale);
            entity.body = mobileMesh;
            entity.HasColor = true;
            entity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            entity.body.Position = position;
            space.Add(entity.body);
            children.Add(entity);
            return entity;
        }

        BepuEntity createShoeRight(Vector3 position, string mesh, float scale)
        {
            BepuEntity entity = new BepuEntity();
            entity.modelName = mesh;
            entity.LoadContent();
            Vector3[] vertices;
            int[] indices;
            TriangleMesh.GetVerticesAndIndicesFromModel(entity.model, out vertices, out indices);
            AffineTransform localTransform = new AffineTransform(new Vector3(scale, scale, scale), Quaternion.Identity, new Vector3(0, 0, 0));
            MobileMesh mobileMesh = new MobileMesh(vertices, indices, localTransform, BEPUphysics.CollisionShapes.MobileMeshSolidity.Counterclockwise, 1);
            entity.localTransform = Matrix.CreateScale(scale, scale, scale);
            entity.body = mobileMesh;
            entity.HasColor = true;
            entity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            entity.body.Position = position;
            space.Add(entity.body);
            children.Add(entity);
            return entity;
        }
        /*
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
         */ 
       protected override void LoadContent()
        {
       
            spriteBatch = new SpriteBatch(GraphicsDevice);
            space = null;
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

          //  space.Update(timeDelta);

       
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
