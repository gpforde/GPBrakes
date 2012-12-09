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

            bepuboundingboxdrawer = new BoundingBoxDrawer(this);
            bepuDrawer = new BasicEffect(GraphicsDevice);

            //chassisboundary = new BoundingBoxDrawer(this);
            //bepudrawer = new BasicEffect(GraphicsDevice);

            cPos = new Vector3(0, 5, 0);
            cLook = new Vector3(0, 0, -1);
            cRight = new Vector3(1, 0, 0);
            cUp = new Vector3(0, 1, 0);

            children.Add(camera);
            ground = new Ground();                        
            children.Add(ground);
            
         /*
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
            */
            base.Initialize();
        }


        void createTower()
        {
         /*
            for (float y = 5; y > 20; y -= 5)
            {
                createVehicle(new Vector3(4, 10, 28));
            }
            /*
            for (float y = 70; y > 20; y -= 5)
            {
                createPiston1(new Vector3(4, 10, 28), 1, 1, 1);
            }
            */
            for (float y = 1; y > 1; y -= 1)
            {
                createVehicle(new Vector3(4, 10, 28));
            }
            /*
            for (float y = 10; y > 1; y -= 1)
            {
                createChassis(new Vector3(4, 10, 28), 1, 1, 1);
            }
     */
        }

        BepuEntity createPiston1(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "slavepiston";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            return theBox;
        }

        BepuEntity createShoeLeft(Vector3 position, string mesh, float scale)
        {
            BepuEntity entity = new BepuEntity();
            entity.modelName = "brakeshoeleft2";
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
        BepuEntity createShoeRight(Vector3 position, string mesh, float scale)
        {
            BepuEntity entity = new BepuEntity();
            entity.modelName = "BrakeShoeRight23";
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
        */
        BepuEntity createShoeRight(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "BrakeShoeRight23";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            return theBox;
        }


        BepuEntity createShoeLeft(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "brakeshoeleft2";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            return theBox;
        }
        BepuEntity createCylinder(Vector3 position, float width, float height, float length)
        {        
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "SlaveCylinder";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            return theBox;
        }

        BepuEntity createHub(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "hub";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, width, height, length, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            return theBox;
        }



        /*
        // BepuEntity createWheel(Vector3 position, Entity body, out RevoluteMotor drivingMotor, float wheelWidth, float wheelRadius)
        BepuEntity createWheel(Vector3 position, float wheelWidth, float wheelRadius)
        {
            BepuEntity wheelEntity = new BepuEntity();
            wheelEntity.modelName = "Wheels4";
            //        wheelEntity.Material.KineticFriction = 2.5f;
            //        wheelEntity.Material.StaticFriction = 2.5f;
            //      wheelEntity.Orientation = Quaternion.CreateFromAxisAngle(Vector3.Forward, MathHelper.PiOver2);
            wheelEntity.LoadContent();
            wheelEntity.body = new Cylinder(position, wheelWidth, wheelRadius, wheelRadius);
            wheelEntity.localTransform = Matrix.CreateScale(wheelRadius, wheelWidth, wheelRadius);
            wheelEntity.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            wheelEntity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

            //       drivingMotor = new RevoluteMotor();
            //       drivingMotor.Settings.VelocityMotor.Softness = .3f;
            //   drivingMotor.Settings.MaximumForce = 100;
            //drivingMotor.IsActive = false;

            space.Add(wheelEntity.body);
            children.Add(wheelEntity);
            //space.Add(drivingMotor);
            return wheelEntity;
        }
         */ 
        
          BepuEntity createWheel(Vector3 position,float wheelWidth, float wheelRadius)      
        {
            BepuEntity wheelEntity = new BepuEntity();
            wheelEntity.modelName = "Wheels4";
            wheelEntity.LoadContent();
            wheelEntity.body = new Cylinder(position, wheelWidth, wheelRadius, wheelRadius);
            wheelEntity.localTransform = Matrix.CreateScale(wheelRadius, wheelWidth, wheelRadius);
            wheelEntity.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            wheelEntity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(wheelEntity.body);
            children.Add(wheelEntity);
            return wheelEntity;
        }
        /* MESH VERSION OF WHEEL
          BepuEntity createWheel(Vector3 position, string mesh, float scale)
          {
              BepuEntity entity = new BepuEntity();
              entity.modelName = "Wheels4";
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
          }*/

    
         
        public void createVehicle(Vector3 position)
        {
            float width = 4;
            float height = 4;
            float length = 4;
            
            RevoluteJoint joint;
            WeldJoint weldjoint;
            BepuEntity chassis = createChassis(position, width, height, length);
            chassis.LoadContent();
            chassis.body.Mass = 0;
            float wheelWidth = 2;
            float wheelRadius = 2;
            //chassis.Material = new chassis.Material(.3f, .2f, .2f);
            BepuEntity wheel;
            BepuEntity cylinder1;
            BepuEntity piston;
            BepuEntity shoeright;
            BepuEntity shoeleft;
            BepuEntity hub;

            pos = position;
            chassis.body.Position = position;
            position = pos;
            chassisboundary = new BoundingBoxDrawer(this);
            bepudrawer = new BasicEffect(GraphicsDevice);
        


            
            shoeright = createShoeRight(new Vector3(position.X - (width + 27), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);
            shoeright = createShoeRight(new Vector3(position.X + (width + 27), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeright = createShoeRight(new Vector3(position.X - (width + 27), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeright = createShoeRight(new Vector3(position.X + (width + 27), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);

            shoeleft = createShoeLeft(new Vector3(position.X - (width + 31), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);
            shoeleft = createShoeLeft(new Vector3(position.X + (width + 31), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeleft = createShoeLeft(new Vector3(position.X - (width + 31), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeleft = createShoeLeft(new Vector3(position.X + (width + 31), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);
            
            hub = createHub(new Vector3(position.X - (width + 15), position.Y, position.Z - (length / 2)), width, height, length);         
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            hub = createHub(new Vector3(position.X + (width + 15), position.Y, position.Z - (length / 2)), width, height, length);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            hub = createHub(new Vector3(position.X - (width + 15), position.Y, position.Z + (length + 23)), width, height, length);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            
            hub = createHub(new Vector3(position.X + (width + 15), position.Y, position.Z + (length + 23)), width, height, length);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            
            /*
            hub = createHub(new Vector3(position.X + (width + 12), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, hub.body, hub.body.Position, new Vector3(1, 0, 0));
            joint.Motor.Settings.VelocityMotor.GoalVelocity = 0;
            joint.Motor.IsActive = false;
            space.Add(joint);
            */
            

            cylinder1 = createCylinder(new Vector3(position.X - (width + 19), position.Y, position.Z - (length / 2)), width, height, length);
            weldjoint = new WeldJoint(hub.body, cylinder1.body);
            space.Add(weldjoint);
            cylinder1 = createCylinder(new Vector3(position.X + (width + 19), position.Y, position.Z - (length / 2)), width, height, length);
            weldjoint = new WeldJoint(hub.body, cylinder1.body);
            space.Add(weldjoint);
            cylinder1 = createCylinder(new Vector3(position.X - (width + 19), position.Y, position.Z + (length + 23)), width, height, length);
            weldjoint = new WeldJoint(hub.body, cylinder1.body);
            space.Add(weldjoint);
            cylinder1 = createCylinder(new Vector3(position.X + (width + 19), position.Y, position.Z + (length + 23)), width, height, length);
            weldjoint = new WeldJoint(hub.body, cylinder1.body);
            space.Add(weldjoint);
            
            piston = createPiston1(new Vector3(position.X - (width + 23), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);
            piston = createPiston1(new Vector3(position.X + (width + 23), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            piston = createPiston1(new Vector3(position.X - (width + 23), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            piston = createPiston1(new Vector3(position.X + (width + 23), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);

            
            
            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            joint.Motor.IsActive = true;
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            joint.Motor.IsActive = true;
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            joint.Motor.IsActive = true;
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            joint.Motor.IsActive = true;
            space.Add(joint);
            /*
            RevoluteJoint hinge;
            hinge = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 0, 1));
            hinge.Motor.Settings.VelocityMotor.GoalVelocity = 50;
            */
            /*WHEELS AS MESH ENTITYS 
            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), "Wheels4", 1);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), "Wheels4", 1);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth), "Wheels4", 1);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z + (length + 23) + wheelWidth), "Wheels4", 1);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);
            */

          
        }

      
  
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
            //chassisboundary.Draw(bepudrawer, space);
            //bepuboundingboxdrawer.Draw(bepudrawer,space);
            

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
