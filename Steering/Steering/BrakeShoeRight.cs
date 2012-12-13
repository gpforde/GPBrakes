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
    class BrakeShoeRight : Entity
    {
        

        public BrakeShoeRight()
        {

        }

        public BepuEntity createShoeRight(Vector3 position, float width, float height, float length)
        {
            BepuEntity theBox2 = new BepuEntity();
            theBox2.modelName = "BrakeShoeRight23";
            theBox2.localTransform = Matrix.CreateScale(new Vector3(width, height, length));
            theBox2.body = new Box(position, 2, 6, .5f, 1);
            XNAGame.Instance().space.Add(theBox2.body);
            XNAGame.Instance().children.Add(theBox2);
            theBox2.HasColor = true;
           // modelDrawer.Add(theBox2.body);
            return theBox2;
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
