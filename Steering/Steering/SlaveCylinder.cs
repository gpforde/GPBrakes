
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
    public class SlaveCylinder:Entity
    {

        
              public SlaveCylinder()
              {
            
              }
      
             public  BepuEntity createCylinder(Vector3 position, string mesh, float scale)
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
                  entity.body.Position = position;
                  entity.body.Orientation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
                  XNAGame.Instance().space.Add(entity.body);
                  XNAGame.Instance().children.Add(entity);
                 // modelDrawer.Add(entity.body);
                  return entity;
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
