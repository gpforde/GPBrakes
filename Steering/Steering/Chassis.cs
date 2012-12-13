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
    public class Chassis:Entity
    {

        
              public Chassis()
              {
            
              }

              public BepuEntity createChassis(Vector3 position, float width, float height, float length)
              {
                  BepuEntity theChasis = new BepuEntity();
                  theChasis.modelName = "chassis7";
                  theChasis.LoadContent();
                  theChasis.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
                  theChasis.body = new Box(position, 20, 15, 60, 1);
                  theChasis.configureEvents();
                  XNAGame.Instance().space.Add(theChasis.body);
                  XNAGame.Instance().children.Add(theChasis);
                  return theChasis;
              }
               
        public override void LoadContent()
        {

        }

        public void push(Vector3 force)
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
