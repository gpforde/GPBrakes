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
using BEPUphysics.MathExtensions;
using BEPUphysics.Constraints.TwoEntity.Joints;
using BEPUphysics.Constraints.TwoEntity.Motors;
using BEPUphysics.Constraints.TwoEntity.JointLimits;
using BEPUphysics.Constraints.SolverGroups;
using BEPUphysics.Collidables;
using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.DataStructures;

namespace BrakingSystem
{
 
    public class XNAGame : Microsoft.Xna.Framework.Game
    {
        
        
        public BoundingBoxDrawer chassisboundary;
        public BasicEffect bepudrawer;
        public float rotation;
        public Vector3 force = Vector3.Zero;

        static XNAGame instance = null;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Vector3 look;
             Vector3 pos,cPos, cLook, cUp, cRight;
            
    
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
        
        protected override void Initialize()
        {
          
            camera = new Camera();
            int midX = GraphicsDeviceManager.DefaultBackBufferHeight / 2;
            int midY = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            Mouse.SetPosition(midX, midY);

            chassisboundary = new BoundingBoxDrawer(this);
            bepudrawer = new BasicEffect(GraphicsDevice);

            cPos = new Vector3(0, 5, 0);
            cLook = new Vector3(0, 0, -1);
            cRight = new Vector3(1, 0, 0);
            cUp = new Vector3(0, 1, 0);

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


            Wheel wheel = new Wheel();
            wheel.pos.X = (float)3.05; wheel.pos.Y = (float)9.9; wheel.pos.Z = (float)29.25;
            children.Add(wheel);

            base.Initialize();
        }


        void createTower()
        {
         /*
            for (float y = 70; y > 20; y -= 5)
            {
                createCylinder(new Vector3(4, 10, 28), 1, 1, 1);
            }

            for (float y = 70; y > 20; y -= 5)
            {
                createPiston1(new Vector3(4, 10, 28), 1, 1, 1);
            }

      

            for (float y = 10; y > 1; y -= 1)
            {
                createChassis(new Vector3(4, 10, 28), 1, 1, 1);
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



       // BepuEntity createWheel(Vector3 position, Entity body, out RevoluteMotor drivingMotor, float wheelWidth, float wheelRadius)
          BepuEntity createWheel(Vector3 position, float wheelWidth, float wheelRadius)
      
        {
            BepuEntity wheelEntity = new BepuEntity();
            wheelEntity.modelName = "Wheels4";
    //        wheelEntity.Material.KineticFriction = 2.5f;
      //      wheelEntity.Material.StaticFriction = 2.5f;
        //    wheelEntity.Orientation = Quaternion.CreateFromAxisAngle(Vector3.Forward, MathHelper.PiOver2);
            wheelEntity.LoadContent();
            wheelEntity.body = new Cylinder(position, wheelWidth, wheelRadius, wheelRadius);
            wheelEntity.localTransform = Matrix.CreateScale(wheelRadius, wheelWidth, wheelRadius);
            wheelEntity.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            wheelEntity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
/*
            drivingMotor = new RevoluteMotor();
            drivingMotor.Settings.VelocityMotor.Softness = .3f;
            drivingMotor.Settings.MaximumForce = 100;
            drivingMotor.IsActive = false;
        */    
            space.Add(wheelEntity.body);
            children.Add(wheelEntity);
         //   space.Add(drivingMotor);
            return wheelEntity;
        }

     
        
        public void createVehicle(Vector3 position)
        {
            float width = 4;
            float height = 4;
            float length = 4;
            
            RevoluteJoint joint;
            BepuEntity chassis = createChassis(position, width, height, length);
            chassis.LoadContent();
            chassis.body.Mass = 0;
            float wheelWidth = 2;
            float wheelRadius = 2;
            BepuEntity wheel;
            pos = position;
            chassis.body.Position = position;
            position = pos;
          //  drive = new RevoluteMotor();
            
            chassisboundary = new BoundingBoxDrawer(this);
            bepudrawer = new BasicEffect(GraphicsDevice);
            // createWheel(Vector3 position,Entity body,out RevoluteMotor drivingMotor, float wheelWidth, float wheelRadius)

            /*
            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth),body,out, wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth),out, wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth),out, wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth),out, wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);
             */ 
            
            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);
            

          
        }
        ////////////////////////////////////////////////////////////////////

        /*
        Entity AddDriveWheel(Vector3 wheelOffset, Entity body, out RevoluteMotor drivingMotor)
        {
            var wheel = new Cylinder(body.Position + wheelOffset, .2f, .5f, 5f);
            wheel.Material.KineticFriction = 2.5f;
            wheel.Material.StaticFriction = 2.5f;
            wheel.Orientation = Quaternion.CreateFromAxisAngle(Vector3.Forward, MathHelper.PiOver2);

            //Make the swivel hinge extremely rigid.  There are going to be extreme conditions when the wheels get up to speed;
            //we don't want the forces involved to torque the wheel off the frame!

            //Motorize the wheel.
            drivingMotor = new RevoluteMotor(body, wheel, Vector3.Left);
            drivingMotor.Settings.VelocityMotor.Softness = .3f;
            drivingMotor.Settings.MaximumForce = 100;
            //Let it roll when the user isn't giving specific commands.
            drivingMotor.IsActive = false;

            space.Add(wheel);
            space.Add(drivingMotor);
     
            return wheel;
        }
        */
       


        
        /// ///////////////////////////////////////////////////////////////
      
  
        BepuEntity createChassis(Vector3 position, float width, float height, float length)
        {
            BepuEntity theChasis = new BepuEntity();
            theChasis.modelName = "chassis7";
            theChasis.LoadContent();
            theChasis.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theChasis.body = new Box(position, width, height, length, 1);
            theChasis.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            theChasis.configureEvents();
            space.Add(theChasis.body);
            children.Add(theChasis);
            return theChasis;
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


            
            createTower();
            createVehicle(Vector3.Zero);
            

        }
       protected override void LoadContent()
        {
       
            spriteBatch = new SpriteBatch(GraphicsDevice);
   
            resetScene();
         
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

            float speed = 0.5f;

            float dirX = (float)Math.Sin(rotation);
            float dirZ = (float)Math.Cos(rotation);

            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }


            

            if (keyState.IsKeyDown(Keys.Up))
            {
                //chassis.body.Position -= new Vector3((speed * dirX), 0, (speed * dirZ));
                //speed++;
                //createVehicle(Vector3.Zero) -= new Vector3((speed * dirX), 0, (speed * dirZ));
                //createVehicle = speed;
                //pos -= new Vector3((speed * dirX), 0, (speed * dirZ));
                //wheel.Position -= new Vector3((speed * dirX), 0, (speed * dirZ));
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                   // createVehicle.chassis.body.Position -= new Vector3((speed * dirX), 0, (speed * dirZ));

                    rotation += (5.0f * timeDelta);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    rotation -= (5.0f * timeDelta);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.V))
                {
                    speed++;

                }

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


            bepudrawer.LightingEnabled = false;
            bepudrawer.VertexColorEnabled = true;
            bepudrawer.World = Matrix.Identity;
            bepudrawer.View = Camera.view;
            bepudrawer.Projection = Camera.projection;
            chassisboundary.Draw(bepudrawer, space);


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
