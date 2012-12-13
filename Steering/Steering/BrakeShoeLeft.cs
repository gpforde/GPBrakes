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
    class BrakeShoeLeft:Entity
    {
      

        public BrakeShoeLeft()
        {
 
        }

        public BepuEntity createShoeLeft(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox3 = new BepuEntity();
            theBox3.modelName = "brakeshoeleft2";
            theBox3.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox3.body = new Box((new Vector3(position.X, position.Y, position.Z)), 2, 2, .5f, 4);
            XNAGame.Instance().space.Add(theBox3.body);
            XNAGame.Instance().children.Add(theBox3);
            theBox3.HasColor = true;
           // modelDrawer.Add(theBox3.body);
            return theBox3;
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
