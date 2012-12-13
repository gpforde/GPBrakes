
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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
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
    public class Vehicle:Entity
    {

        
              public Vehicle()
              {
            
              }
      
             
        public void createVehicle(Vector3 position)
        {
            Chassis chassisentity = new Chassis();
            float width = 4;
            float height = 4;
            float length = 4;
            float pistonheight=1;
            float pistonradius=1;
            float hubheight = .1f;
            float hubradius = 3.25f; 
            WeldJoint weldjoint;
            BepuEntity chassis = chassisentity.createChassis(position, width, height, length);
            chassis.LoadContent();
            chassis.body.Mass = 5;
            float wheelWidth = 3;
            float wheelRadius = .006f;
            BepuEntity wheel;
            BepuEntity cylinder1;
            BepuEntity piston;
            BepuEntity shoeright;
            BepuEntity shoeleft;



            
            BepuEntity hub;

            pos = position;
            chassis.body.Position = position;
            position = pos;

            SlaveCylinder slave = new SlaveCylinder();
            Hub hubentity = new Hub();
            BrakeShoeLeft brakeshoeleft = new BrakeShoeLeft();
            BrakeShoeRight brakeshoeright = new BrakeShoeRight();
            Wheel wheelentity = new Wheel();
            SlavePiston slavepiston = new SlavePiston();
            

            /*
            shoeright = createShoeRight(new Vector3(position.X - (width + 12), position.Y, position.Z - (length / 2)), width, height, length);
            LineJoint = new LineSliderJoint(chassis.body, shoeright.body, new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0));
         //   LineJoint.Motor.Settings.VelocityMotor.GoalVelocity = .0000000000000000001f;
            LineJoint.Motor.IsActive = false;
            space.Add(LineJoint);*/
            
            shoeright = brakeshoeright.createShoeRight(new Vector3(position.X - (width + 12), position.Y, position.Z - (length / 2)), width, height, length);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().space.Add(XNAGame.Instance().joint);
            shoeright = brakeshoeright.createShoeRight(new Vector3(position.X + (width + 12), position.Y, position.Z - (length / 2)), width, height, length);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().space.Add(XNAGame.Instance().joint);
            shoeright = brakeshoeright.createShoeRight(new Vector3(position.X - (width + 12), position.Y, position.Z + (length + 23)), width, height, length);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().space.Add(XNAGame.Instance().joint);
            shoeright = brakeshoeright.createShoeRight(new Vector3(position.X + (width + 12), position.Y, position.Z + (length + 23)), width, height, length);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, shoeright.body, shoeright.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().space.Add(XNAGame.Instance().joint);




            /*
                       shoeleft = brakeshoeleft.createShoeLeft(new Vector3(position.X - (width + 12), position.Y-2.5f, position.Z - (length / 2)), width, height, length);
                       joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(0, 0, 1));
                       space.Add(joint);
                       shoeleft = brakeshoeleft.createShoeLeft(new Vector3(position.X + (width + 12), position.Y-2.5f, position.Z - (length / 2)), width, height, length);
                       joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(1, 0, 0));
                       space.Add(joint);
                       shoeleft = brakeshoeleft.createShoeLeft(new Vector3(position.X - (width + 12), position.Y-2.5f, position.Z + (length + 23)), width, height, length);
                       joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(1, 0, 0));
                       space.Add(joint);
                       shoeleft = brakeshoeleft.createShoeLeft(new Vector3(position.X + (width + 12), position.Y-2.5f, position.Z + (length + 23)), width, height, length);
                       joint = new RevoluteJoint(chassis.body, shoeleft.body, shoeleft.body.Position, new Vector3(0, 1, 0));
                       space.Add(joint);
                  */



            hub = hubentity.createHub(new Vector3(position.X - (width + 10), position.Y, position.Z - (length - 2)), hubheight, hubradius);         
            weldjoint = new WeldJoint(chassis.body, hub.body);
            XNAGame.Instance().space.Add(weldjoint);
            hub = hubentity.createHub(new Vector3(position.X + (width + 10), position.Y, position.Z - (length - 2)), hubheight, hubradius);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            XNAGame.Instance().space.Add(weldjoint);
            hub = hubentity.createHub(new Vector3(position.X - (width + 10), position.Y, position.Z + (length + 23)), hubheight, hubradius);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            XNAGame.Instance().space.Add(weldjoint);

            hub = hubentity.createHub(new Vector3(position.X + (width + 10), position.Y, position.Z + (length + 23)), hubheight, hubradius);
            weldjoint = new WeldJoint(chassis.body, hub.body);
            XNAGame.Instance().space.Add(weldjoint);

            

  
            




            cylinder1 = slave.createCylinder(new Vector3(position.X - (width + 11), position.Y + 1.25f, position.Z -1), "SlaveCylinder", 1);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));

            XNAGame.Instance().space.Add(XNAGame.Instance().joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            XNAGame.Instance().space.Add(weldjoint);
            cylinder1 = slave.createCylinder(new Vector3(position.X + (width + 11), position.Y + 1.25f, position.Z - 1), "SlaveCylinder", 1);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            XNAGame.Instance().space.Add(XNAGame.Instance().joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            XNAGame.Instance().space.Add(weldjoint);
             cylinder1 = slave.createCylinder(new Vector3(position.X - (width + 11), position.Y + 1.25f, position.Z + (length + 24)), "SlaveCylinder", 1);
             XNAGame.Instance().joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
             XNAGame.Instance().space.Add(XNAGame.Instance().joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            XNAGame.Instance().space.Add(weldjoint);
            cylinder1 = slave.createCylinder(new Vector3(position.X + (width + 11), position.Y + 1.25f, position.Z + (length + 24)), "SlaveCylinder", 1);
            XNAGame.Instance().joint = new RevoluteJoint(chassis.body, cylinder1.body, cylinder1.body.Position, new Vector3(0, 0, 1));
            XNAGame.Instance().space.Add(XNAGame.Instance().joint);

            weldjoint = new WeldJoint(chassis.body, cylinder1.body);
            XNAGame.Instance().space.Add(weldjoint);

            piston = slavepiston.createPiston1(new Vector3(position.X - (width + 11), position.Y + 1.25f, position.Z - 1), pistonheight, pistonradius);
            /*
            prismjoint = new PrismaticJoint(cylinder1.body, piston.body, cylinder1.body.Position,new Vector3(0,1,0) , piston.body.Position);
            prismjoint.Motor.IsActive = false;
            prismjoint.Limit.IsActive = true;
            prismjoint.Limit.Minimum = 0;
            prismjoint.Limit.Maximum = 4;
            space.Add(prismjoint);
            */

            XNAGame.Instance().LineJoint = new LineSliderJoint(cylinder1.body, piston.body, new Vector3(60, 5, -21), new Vector3(0, 0, -10), new Vector3(60, 5, -10));
            XNAGame.Instance().LineJoint.Motor.IsActive = false;
            XNAGame.Instance().space.Add(XNAGame.Instance().LineJoint);

            piston = slavepiston.createPiston1(new Vector3(position.X + (width + 11), position.Y + 1.25f, position.Z - 1), pistonheight, pistonradius);
            XNAGame.Instance().LineJoint = new LineSliderJoint(cylinder1.body, piston.body, new Vector3(60, 5, -21), new Vector3(0, 0, 10), new Vector3(60, 5, -10));
            XNAGame.Instance().LineJoint.Motor.IsActive = false;
        //    space.Add(LineJoint);

            piston = slavepiston.createPiston1(new Vector3(position.X - (width + 11), position.Y + 1.25f, position.Z + (length + 24)), pistonheight, pistonradius);
            XNAGame.Instance().LineJoint = new LineSliderJoint(cylinder1.body, piston.body, new Vector3(60, 5, -21), new Vector3(0, 0, 10), new Vector3(60, 5, -10));
            XNAGame.Instance().LineJoint.Motor.IsActive = false;
        //    space.Add(LineJoint);

            piston = slavepiston.createPiston1(new Vector3(position.X + (width + 11), position.Y + 1.25f, position.Z + (length + 24)), pistonheight, pistonradius);
            XNAGame.Instance().LineJoint = new LineSliderJoint(cylinder1.body, piston.body, new Vector3(60, 5, -21), new Vector3(0, 0, 10), new Vector3(60, 5, -10));
            XNAGame.Instance().LineJoint.Motor.IsActive = false;
        //    space.Add(LineJoint);



     
            //WHEELS AS MESH ENTITYS 
            wheel = wheelentity.createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z - (length - 5) - wheelWidth), "Wheels6", 1);
            XNAGame.Instance().wheeljoint1 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().wheeljoint1.Motor.IsActive = false;
            XNAGame.Instance().space.Add(XNAGame.Instance().wheeljoint1);


            wheel = wheelentity.createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z - (length - 5) - wheelWidth), "Wheels6", 1);
            XNAGame.Instance().wheeljoint2 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().wheeljoint2.Motor.IsActive = false;
            XNAGame.Instance().space.Add(XNAGame.Instance().wheeljoint2);

            wheel = wheelentity.createWheel(new Vector3(position.X - (width + 12) + wheelRadius, position.Y, position.Z + (length + 21) + wheelWidth), "Wheels6", 1);
            XNAGame.Instance().wheeljoint3 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().wheeljoint3.Motor.IsActive = false;
            XNAGame.Instance().space.Add(XNAGame.Instance().wheeljoint3);

            wheel = wheelentity.createWheel(new Vector3(position.X + (width + 12) - wheelRadius, position.Y, position.Z + (length + 21) + wheelWidth), "Wheels6", 1);
            XNAGame.Instance().wheeljoint4 = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            XNAGame.Instance().wheeljoint4.Motor.IsActive = false;
            XNAGame.Instance().space.Add(XNAGame.Instance().wheeljoint4);
            
            //modelDrawer.Add(chassis.body);
          
        }
               
        public override void LoadContent()
        {

        }




        public override void Update(GameTime gameTime)
        {
            

         
            base.Update(gameTime);
        }

        public override void UnloadContent()
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            
        }
    }
}
