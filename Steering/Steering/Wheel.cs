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
    class Wheel:Entity
    {
 

        public Wheel()
        {

        }


        public BepuEntity createWheel(Vector3 position, string mesh, float scale)
        {
            BepuEntity entity = new BepuEntity();
            entity.modelName = "Wheels7";
            entity.LoadContent();
            Vector3[] vertices;
            int[] indices;
            TriangleMesh.GetVerticesAndIndicesFromModel(entity.model, out vertices, out indices);
            AffineTransform localTransform = new AffineTransform(new Vector3(scale, scale, scale), Quaternion.Identity, new Vector3(0, 0, 0));
            MobileMesh mobileMesh = new MobileMesh(vertices, indices, localTransform, BEPUphysics.CollisionShapes.MobileMeshSolidity.Counterclockwise, 1);
            //Correct Scale for 'Wheels6:'
            //      entity.localTransform = Matrix.CreateScale(4.5f, 4.5f, 4.5f);
            entity.localTransform = Matrix.CreateScale(2.75f, 2.75f, 2.75f);
            entity.body = mobileMesh;
            entity.HasColor = true;
           entity.body.Position = position;
            XNAGame.Instance().space.Add(entity.body);
            XNAGame.Instance().children.Add(entity);
            //modelDrawer.Add(entity.body);
            return entity;
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
