/*
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
using BEPUphysics.MathExtensions;
using BEPUphysics.Constraints.TwoEntity.Joints;
using BEPUphysics.Constraints.SolverGroups;
using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.DataStructures;

namespace BrakingSystem
{
    class Chassis : XNAGame 
    {
        float mass3;
        public float rotation;
        Vector3 look;
        Vector3 pos, cPos, cLook, cUp, cRight;

        public Chassis()
        {
            force = Vector3.Zero;
            mass3 = 1.0f;
        }

        public void Initialize()
        {
            
            cPos = new Vector3(0, 5, 0);
            cLook = new Vector3(0, 0, -1);
            cRight = new Vector3(1, 0, 0);
            cUp = new Vector3(0, 1, 0);
              
        }


        public void createVehicle(Vector3 position)
        {
            float width = 4;
            float height = 4;
            float length = 4;
            RevoluteJoint joint;
            BepuEntity chassis = createChassis(position, width, height, length);
            chassis.LoadContent();
            chassis.body.Mass = 1;
            float wheelWidth = 2;
            float wheelRadius = 2;
            BepuEntity wheel;

           // chassisboundary = new BoundingBoxDrawer(this);
            //bepudrawer = new BasicEffect(GraphicsDevice);

            wheel = createWheel(new Vector3(position.X - (width + 11) + wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 0, 1));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 11) - wheelRadius, position.Y, position.Z - (length / 2) - wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X - (width + 11) + wheelRadius, position.Y, position.Z + (length + 24) + wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(1, 0, 0));
            space.Add(joint);

            wheel = createWheel(new Vector3(position.X + (width + 11) - wheelRadius, position.Y, position.Z + (length + 24) + wheelWidth), wheelWidth, wheelRadius);
            joint = new RevoluteJoint(chassis.body, wheel.body, wheel.body.Position, new Vector3(0, 1, 0));
            space.Add(joint);




           

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
        BepuEntity createWheel(Vector3 position, float wheelWidth, float wheelRadius)
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




        public void LoadContent()
        {
      
        }

        public void push(Vector3 force)
        {
            this.force += force;
        }


        public void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 5.0f;
            KeyboardState keyState = Keyboard.GetState();
            force = Vector3.Zero;

            float dirX = (float)Math.Sin(rotation);
            float dirZ = (float)Math.Cos(rotation);

            if (keyState.IsKeyDown(Keys.I))
            {
                push(look * 50f);
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                pos -= new Vector3((speed * dirX), 0, (speed * dirZ));
                speed++;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
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

            base.Update(gameTime);
        }

        public  void UnloadContent()
        {

        }

        public  void Draw(GameTime gameTime)
        {
            
            
        }
    }
}


*/

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
    class Chassis:Entity
    {
        float mass3;
        public float rotation;
        public Chassis()
        {
            force = Vector3.Zero;
            mass3 = 1.0f;
        }

        public override void LoadContent()
        {
            model = XNAGame.Instance().Content.Load<Model>("Chassis7");
        }

        public void push(Vector3 force)
        {
            this.force += force;
        }


        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 5.0f;
            KeyboardState keyState = Keyboard.GetState();
            

            float dirX = (float)Math.Sin(rotation);
            float dirZ = (float)Math.Cos(rotation);

            if (keyState.IsKeyDown(Keys.Up))
            {
                pos -= new Vector3((speed * dirX), 0, (speed * dirZ));
                speed++;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
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
                    }
                    mesh.Draw();
                }
            }
        }
    }
}

