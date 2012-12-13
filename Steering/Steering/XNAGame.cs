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
using BEPUphysics.Entities;
using BEPUphysics.CollisionRuleManagement;
using BEPUphysics.MathExtensions;
using BEPUphysics.Constraints.TwoEntity.Joints;
using BEPUphysics.Constraints.TwoEntity.Motors;
using BEPUphysics.Constraints.TwoEntity.JointLimits;
using BEPUphysics.Constraints.SolverGroups;
using BEPUphysics.Collidables;
using BEPUphysics.Vehicle;
using BEPUphysicsDrawer;
using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.DataStructures;
using BEPUphysicsDrawer.Models;

namespace BrakingSystem
{
 
    public class XNAGame : Microsoft.Xna.Framework.Game
    {
        
        
        public BoundingBoxDrawer chassisboundary;
        public BasicEffect bepudrawer;
        public float rotation;
        public Vector3 force = Vector3.Zero;
        BoundingBoxDrawer bepuboundingboxdrawer;
        BasicEffect bepuDrawer;
        static XNAGame instance = null;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyState;
        public RevoluteJoint wheeljoint1, wheeljoint2, wheeljoint3, wheeljoint4;
        public RevoluteJoint joint;
        public LineSliderJoint LineJoint;
        SoundEffect enginenoise;
       // PointOnLineJoint;
        public PrismaticJoint prismjoint;

        Vehicle vehicle = new Vehicle();
    
        public Space space;
        Box groundBox;
        public Random random = new Random();

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }
        private Camera camera;
        public List<Entity> children = new List<Entity>();

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

        private InstancedModelDrawer modelDrawer;

        public InstancedModelDrawer ModelDrawer
        {
            get { return modelDrawer; }
            set { modelDrawer = value; }
        }

        protected override void Initialize()
        {
            
            camera = new Camera();
            int midX = GraphicsDeviceManager.DefaultBackBufferHeight / 2;
            int midY = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            Mouse.SetPosition(midX, midY);

            modelDrawer = new InstancedModelDrawer(this);
            modelDrawer.IsWireframe = true;

            bepuboundingboxdrawer = new BoundingBoxDrawer(this);
            bepuDrawer = new BasicEffect(GraphicsDevice);

            children.Add(camera);
            ground = new Ground();                        
            children.Add(ground);
            
 
            base.Initialize();
        }

       
        public void resetScene()
        {
            for (int i = 0; i < children.Count(); i++)
            {
                if (children[i] is BepuEntity)
                {
                    children.Remove(children[i]);
                    i--;
                }
            }

            space = null;
            space = new Space();
            space.ForceUpdater.Gravity = new Vector3(0, -9.81f, 0);

            groundBox = new Box(Vector3.Zero, ground.width, 0.1f, ground.height);
            space.Add(groundBox);
       
            vehicle.createVehicle(new Vector3(0,10,0));

        }
       protected override void LoadContent()
        {
       
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Song jazz;
            jazz = Content.Load<Song>("swingguitars");
            resetScene();

 
            enginenoise = Content.Load<SoundEffect>("engine");


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
            keyState = Keyboard.GetState();

            modelDrawer.Update();

 

            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
           
            Song jazz;
            jazz = Content.Load<Song>("swingguitars");
            if (keyState.IsKeyDown(Keys.J))
            {
            MediaPlayer.Play(jazz);
            }

 
            if (keyState.IsKeyDown(Keys.F))
            {
                enginenoise.Play();
            }
            
            if (keyState.IsKeyDown(Keys.R))
            {

                wheeljoint1.Motor.Settings.VelocityMotor.GoalVelocity = -.1f;
                wheeljoint1.Motor.IsActive = true;
                wheeljoint2.Motor.Settings.VelocityMotor.GoalVelocity = -.1f;
                wheeljoint2.Motor.IsActive = true;
                wheeljoint3.Motor.Settings.VelocityMotor.GoalVelocity = -.1f;
                wheeljoint3.Motor.IsActive = true;
                wheeljoint4.Motor.Settings.VelocityMotor.GoalVelocity = -.1f;
                wheeljoint4.Motor.IsActive = true;

            }

            else
            {
                
                wheeljoint1.Motor.Settings.VelocityMotor.GoalVelocity = -.9f;
                wheeljoint1.Motor.IsActive = false;
                wheeljoint2.Motor.Settings.VelocityMotor.GoalVelocity = -.9f;
                wheeljoint2.Motor.IsActive = false;
                wheeljoint3.Motor.Settings.VelocityMotor.GoalVelocity = -.9f;
                wheeljoint3.Motor.IsActive = false;
                wheeljoint4.Motor.Settings.VelocityMotor.GoalVelocity = -.9f;
                wheeljoint4.Motor.IsActive = false;
                
            }

            if (keyState.IsKeyDown(Keys.B))
            {
                
                LineJoint.Motor.Settings.VelocityMotor.GoalVelocity = 150f;
                LineJoint.Motor.IsActive = true;
                 
             //   prismjoint.Motor.Settings.VelocityMotor.GoalVelocity = 2;
               // prismjoint.Motor.IsActive = true;
            }
            else
            {
                LineJoint.Motor.IsActive = false;
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

           /*
            bepudrawer.LightingEnabled = false;
            bepudrawer.VertexColorEnabled = true;
            bepudrawer.World = Matrix.Identity;
            bepudrawer.View = Camera.view;
            bepudrawer.Projection = Camera.projection;
 */
           
            
            GraphicsDevice.Clear(Color.Black);
            RasterizerState st = new RasterizerState();
            st.FillMode = FillMode.Solid;

            GraphicsDevice.RasterizerState = st;
            spriteBatch.Begin();
            foreach (Entity child in children)
            {
                DepthStencilState state = new DepthStencilState();
                state.DepthBufferEnable = true;           
                GraphicsDevice.DepthStencilState = state;
                child.Draw(gameTime);
            }

            spriteBatch.End();
            modelDrawer.Draw(Camera.getView(), Camera.getProjection());
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

        public SlaveCylinder slave
        {
            get
            {
                return slave;
            }
            set
            {
                slave = value;
            }
        }

        public Hub hubentity
        {
            get
            {
                return hubentity;
            }
            set
            {
                hubentity = value;
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
