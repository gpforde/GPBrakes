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
        RevoluteJoint wheeljoint1, wheeljoint2, wheeljoint3, wheeljoint4;
        RevoluteJoint joint;
        LineSliderJoint LineJoint;
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
        
        BepuEntity createPiston1(Vector3 position,float pistonheight, float pistonradius)
        {
            BepuEntity theBox1 = new BepuEntity();
            theBox1.modelName = "slavepiston";
            theBox1.body = new Cylinder(position,  .4f, .22f,1);
            theBox1.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            theBox1.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            space.Add(theBox1.body);
            children.Add(theBox1);
            theBox1.HasColor = true;
            modelDrawer.Add(theBox1.body);
            return theBox1;
        }
        //SHOE AS BOX
        BepuEntity createShoeRight(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "BrakeShoeRight23";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Box(position, 2, 3, .5f, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            modelDrawer.Add(theBox.body);
            return theBox;
        }
        //SHOE AS CYLINDER/*
        /*
        BepuEntity createShoeRight(Vector3 position, float rightshoeheight, float rightshoeradius)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "BrakeShoeRight23";
           // theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox.body = new Cylinder(position, rightshoeheight, rightshoeradius, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            theBox.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), MathHelper.PiOver2);
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            modelDrawer.Add(theBox.body);
            return theBox;
        }
        */
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////
        /// Trying to export this to its own class
        
        BepuEntity createShoeLeft(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "brakeshoeleft2";
            theBox.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
           // theBox.body = new Box(position, 2, 2, .5f, 1);
            theBox.body = new Box((new Vector3(position.X , position.Y, position.Z )), 2, 2, .5f, 4);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            modelDrawer.Add(theBox.body);
            return theBox;
        }
 
 
        //MESH CYLINDER IN USE
        
        BepuEntity createCylinder(Vector3 position, string mesh, float scale)
        {
            BepuEntity entity = new BepuEntity();
            entity.modelName = "SlaveCylinder";
            entity.LoadContent();
            entity.Mass = 0;
            Vector3[] vertices;
            int[] indices;

            TriangleMesh.GetVerticesAndIndicesFromModel(entity.model, out vertices, out indices);
            AffineTransform localTransform = new AffineTransform(new Vector3(.4f, .4f, .4f), Quaternion.Identity, new Vector3(0, 0, 0));
            MobileMesh mobileMesh = new MobileMesh(vertices, indices, localTransform, BEPUphysics.CollisionShapes.MobileMeshSolidity.Counterclockwise, 1);
            entity.localTransform = Matrix.CreateScale(1f, 1f,1f);
            entity.body = mobileMesh;
            entity.HasColor = true;
            entity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            entity.body.Position = position;
            entity.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            space.Add(entity.body);
            children.Add(entity);
            modelDrawer.Add(entity.body);
            return entity;
        }



        BepuEntity createHub(Vector3 position,  float hubheight, float hubradius)
        {
            BepuEntity theBox = new BepuEntity();
            theBox.modelName = "hub";
          //  theBox.localTransform = Matrix.CreateScale(new Vector3(hubheight, hubradius));
            theBox.body = new Cylinder(position, hubheight, hubradius, 1);
            theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            theBox.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), MathHelper.PiOver2);
            space.Add(theBox.body);
            children.Add(theBox);
            theBox.HasColor = true;
            modelDrawer.Add(theBox.body);
            return theBox;
        }


        /*
          BepuEntity createWheel(Vector3 position,float wheelWidth, float wheelRadius)      
        {
            BepuEntity wheelEntity = new BepuEntity();
            wheelEntity.modelName = "Wheels4";
            wheelEntity.LoadContent();
            wheelEntity.body = new Cylinder(position, wheelWidth, wheelRadius, wheelRadius);
            wheelEntity.localTransform = Matrix.CreateScale(wheelRadius, wheelWidth, wheelRadius);
            wheelEntity.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), MathHelper.PiOver2);
            wheelEntity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            space.Add(wheelEntity.body);
            children.Add(wheelEntity);
            modelDrawer.Add(wheelEntity.body);
            return wheelEntity;
        }
         */ 
        // MESH VERSION OF WHEEL
          BepuEntity createWheel(Vector3 position, string mesh, float scale)
          {
              BepuEntity entity = new BepuEntity();
              entity.modelName = "Wheels6";
              entity.LoadContent();
              Vector3[] vertices;
              int[] indices;
              TriangleMesh.GetVerticesAndIndicesFromModel(entity.model, out vertices, out indices);
              AffineTransform localTransform = new AffineTransform(new Vector3(scale, scale, scale), Quaternion.Identity, new Vector3(0, 0, 0));
              MobileMesh mobileMesh = new MobileMesh(vertices, indices, localTransform, BEPUphysics.CollisionShapes.MobileMeshSolidity.Counterclockwise, 1);
              entity.localTransform = Matrix.CreateScale(4.5f, 4.5f, 4.5f);
              entity.body = mobileMesh;
              entity.HasColor = true;
              entity.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
              entity.body.Position = position;
              space.Add(entity.body);
              children.Add(entity);
              modelDrawer.Add(entity.body);
              return entity;
          }

        /*
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
         */

          BepuEntity createChassis(Vector3 position, float width, float height, float length)
          {
              BepuEntity theChasis = new BepuEntity();
              theChasis.modelName = "chassis7";
              theChasis.LoadContent();
              theChasis.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
              theChasis.body = new Box(position, 20, 15, 60, 1);
              theChasis.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
              theChasis.configureEvents();
              space.Add(theChasis.body);
              children.Add(theChasis);
              return theChasis;
          }


        public void createVehicle(Vector3 position)
        {
            float width = 4;
            float height = 4;
            float length = 4;
            float radius = 1;
            float pistonheight=1;
            float pistonradius=1;
            float cylinderheight = 2;
            float cylinderradius = 2;
            float rightshoeheight = 1;
            float rightshoeradius = 1;
            float hubheight = .1f;
            float hubradius = 3.25f;
          //  RevoluteJoint joint;
            LinearAxisMotor Linear;
          //  LineSliderJoint LineJoint;
            WeldJoint weldjoint;
            WeldJoint weldjoint3;
            BepuEntity chassis = createChassis(position, width, height, length);
            chassis.LoadContent();
            chassis.body.Mass = 5;
            float wheelWidth = 3;
            float wheelRadius = .006f;
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
            

            /*
            shoeright = createShoeRight(new Vector3(position.X - (width + 12), position.Y, position.Z - (length / 2)), width, height, length);
            LineJoint = new LineSliderJoint(chassis.body, shoeright.body, new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0));
         //   LineJoint.Motor.Settings.VelocityMotor.GoalVelocity = .0000000000000000001f;
            LineJoint.Motor.IsActive = false;
            space.Add(LineJoint);*/
            
            shoeright = createShoeRight(new Vector3(position.X - (width + 12), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
         //   joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
        //    joint.Motor.IsActive = true;
            space.Add(joint);
            shoeright = createShoeRight(new Vector3(position.X + (width + 12), position.Y, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeright = createShoeRight(new Vector3(position.X - (width + 12), position.Y, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeright = createShoeRight(new Vector3(position.X + (width + 12), position.Y, position.Z + (length + 23)),  width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);



/*
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            joint.Motor.IsActive = false;
            space.Add(joint);
            //Shoe as cylinder
 */
         //   shoeleft = createShoeLeft(new Vector3(position.X - (width + 12), position.Y-2.5f, position.Z - (length / 2)), width, height, length);
           // joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(0, 0, 1));
            //space.Add(joint);
            shoeleft = createShoeLeft(new Vector3(position.X + (width + 12), position.Y-2.5f, position.Z - (length / 2)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeleft = createShoeLeft(new Vector3(position.X - (width + 12), position.Y-2.5f, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            shoeleft = createShoeLeft(new Vector3(position.X + (width + 12), position.Y-2.5f, position.Z + (length + 23)), width, height, length);
            joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);
            
            hub = createHub(new Vector3(position.X - (width + 10), position.Y, position.Z - (length - 2)), hubheight, hubradius);         
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            hub = createHub(new Vector3(position.X + (width + 10), position.Y, position.Z - (length - 2)), hubheight, hubradius);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            hub = createHub(new Vector3(position.X - (width + 10), position.Y, position.Z + (length + 23)), hubheight, hubradius);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);
            
            hub = createHub(new Vector3(position.X + (width + 10), position.Y, position.Z + (length + 23)), hubheight, hubradius);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            space.Add(weldjoint);








            //SlaveCylinder slave = new SlaveCylinder();
            //space.Add(slave.createCylinder.entity.body);


            cylinder1 = createCylinder(new Vector3(position.X - (width + 11), position.Y + 1.25f, position.Z -1), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));

            space.Add(joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            space.Add(weldjoint);
            cylinder1 = createCylinder(new Vector3(position.X + (width + 11), position.Y + 2, position.Z - (length / 2)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
             space.Add(weldjoint);
            cylinder1 = createCylinder(new Vector3(position.X - (width + 11), position.Y + 2, position.Z + (length + 23)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            space.Add(weldjoint);
            cylinder1 = createCylinder(new Vector3(position.X + (width + 11), position.Y + 2, position.Z + (length + 23)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            space.Add(weldjoint);
            /*
            //SlaveCylinder slave = new SlaveCylinder();
           //space.Add(slave.createCylinder.entity.body);
          
            
            cylinder1 = slave.createCylinder(new Vector3(position.X - (width + 11), position.Y+2, position.Z - (length / 2)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));

            space.Add(joint);
            
            //weldjoint = new WeldJoint(chassis.body, cylinder1.body);
           // space.Add(weldjoint);
            cylinder1 = slave.createCylinder(new Vector3(position.X + (width + 11), position.Y + 2, position.Z - (length / 2)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);
            
            // weldjoint = new WeldJoint(chassis.body, cylinder1.body);
           // space.Add(weldjoint);
            cylinder1 = slave.createCylinder(new Vector3(position.X - (width + 11), position.Y + 2, position.Z + (length + 23)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);
            
            //  weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            //space.Add(weldjoint);
            cylinder1 = slave.createCylinder(new Vector3(position.X + (width + 11), position.Y + 2, position.Z + (length + 23)), "SlaveCylinder", 1);
            joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);
            
            //weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            //space.Add(weldjoint);
           
              /*/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           /*
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
            */
            piston = createPiston1(new Vector3(position.X - (width + 11), position.Y+1.25f, position.Z-1 ) , pistonheight,pistonradius);
         //   joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(0, 0, 1));
           // space.Add(joint);

            LineJoint = new LineSliderJoint(cylinder1.body, shoeright.body, new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0));
          //  LineJoint.Motor.Settings.VelocityMotor.GoalVelocity = .6f;
            LineJoint.Motor.IsActive = false;
            space.Add(LineJoint);

            piston = createPiston1(new Vector3(position.X + (width + 11), position.Y+2, position.Z - (length / 2)), pistonheight, pistonradius);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            piston = createPiston1(new Vector3(position.X - (width + 11), position.Y+2, position.Z + (length + 23)), pistonheight, pistonradius);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);
            piston = createPiston1(new Vector3(position.X + (width + 11), position.Y+2, position.Z + (length + 23)), pistonheight, pistonradius);
            joint = new RevoluteJoint(chassis.body, piston.body, piston.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);

            /*
            //WHEELS AS CYLINDER BEPU ENTITIES
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
             */ 
     
            //WHEELS AS MESH ENTITYS 
            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z - (length -5) - wheelWidth), "Wheels6", 1);
            wheeljoint1 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
         //   joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            wheeljoint1.Motor.IsActive = false;
            space.Add(wheeljoint1);
          

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z - (length -5) - wheelWidth), "Wheels6", 1);
            wheeljoint2 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
          //  joint.Motor.Settings.VelocityMotor.GoalVelocity = 15;
            wheeljoint2.Motor.IsActive = false;
            space.Add(wheeljoint2);

            wheel = createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z + (length + 21) + wheelWidth), "Wheels6", 1);
            wheeljoint3 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
       //     joint.Motor.Settings.VelocityMotor.GoalVelocity = 1;
            wheeljoint3.Motor.IsActive = false;
            space.Add(wheeljoint3);

            wheel = createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z + (length + 21) + wheelWidth), "Wheels6", 1);
            wheeljoint4 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
        //    joint.Motor.Settings.VelocityMotor.GoalVelocity = -1;
            wheeljoint4.Motor.IsActive = false;
            space.Add(wheeljoint4);
            
            modelDrawer.Add(chassis.body);
          
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
           // createVehicle(Vector3.Zero);
            createVehicle(new Vector3(0,10,0));

        }
       protected override void LoadContent()
        {
       
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Song jazz;
            jazz = Content.Load<Song>("swingguitars");
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
            keyState = Keyboard.GetState();

            modelDrawer.Update();

            float speed = 0.5f;

            float dirX = (float)Math.Sin(rotation);
            float dirZ = (float)Math.Cos(rotation);

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
                 LineJoint.Motor.Settings.VelocityMotor.GoalVelocity = 1f;
                 LineJoint.Motor.IsActive = true;
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
        /*
       public void Motor(KeyboardState motoron)
       {
           if (keyState.IsKeyDown(Keys.R))
           {
               createVehicle.joint.Motor.IsActive = false;
           }
       }*/

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



        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return graphics;
            }
        }
    }
}
