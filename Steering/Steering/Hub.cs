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
    public class Hub:Entity
    {
        

        public Hub()
        {

            
        }
        
        public BepuEntity createHub(Vector3 position, float hubheight, float hubradius)
        {
            BepuEntity theBox6 = new BepuEntity();
            theBox6.modelName = "hub";
            //  theBox.localTransform = Matrix.CreateScale(new Vector3(hubheight, hubradius));
            theBox6.body = new Cylinder(position, hubheight, hubradius, 1);
            theBox6.HasColor = true;
           // theBox.diffuse = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            theBox6.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), MathHelper.PiOver2);
            XNAGame.Instance().space.Add(theBox6.body);
            XNAGame.Instance().children.Add(theBox6);

            // modelDrawer.Add(theBox.body);
            return theBox6;
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
