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

namespace BrakingSystem
{
    class Hub:Entity
    {
        float mass2;

        public Hub()
        {
            force = Vector3.Zero;
            mass2 = 1.0f;
            colour = Color.Red;
        }

        public override void LoadContent()
        {
            model = XNAGame.Instance().Content.Load<Model>("hub");
        }

        public void push(Vector3 force)
        {
            this.force += force;
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
            // Draw the mesh
            if (model != null)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        
                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                        effect.World = worldTransform;
                        effect.Projection = XNAGame.Instance().Camera.getProjection();
                        effect.View = XNAGame.Instance().Camera.getView();
                        
                        //Draw(worldTransform, XNAGame.Instance().Camera.getView(), XNAGame.Instance().Camera.getProjection(), colour);
                    }
                    mesh.Draw();

                }
                
            }
        }
    }
}
