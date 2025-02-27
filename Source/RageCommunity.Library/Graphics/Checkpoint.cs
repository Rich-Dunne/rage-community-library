﻿using System;
using System.Drawing;
using Rage;
using RageCommunity.Library.Extensions;
using RageCommunity.Library.Wrappers;

namespace RageCommunity.Library.Graphics
{
    /// <summary>
    /// This class represents a checkpoint within GTA V.
    /// </summary>
    /// <remarks>
    /// Originally created by PNWParksFan. 
    /// </remarks>
    public class Checkpoint : IDeletable, ISpatial
    {
        private readonly bool setToGround;
        private Color color;
        private float height;
        private Vector3 nextPosition;
        private Vector3 position;
        private float radius;
        private int reserved;
        private int checkpointType;

        public Checkpoint(Vector3 position,
                          Color color,
                          float radius = 1f,
                          float height = 1f,
                          int checkpointType = 47,
                          int reserved = 0,
                          bool setZtoGround = true) : this(position, position, color, radius, height, checkpointType, reserved, setZtoGround)
        {
        }

        public Checkpoint(Vector3 position,
                          Vector3 nextPosition,
                          Color color,
                          float radius,
                          float height,
                          int checkpointType = 0,
                          int reserved = 0,
                          bool setZtoGround = true)
        {
            this.checkpointType = checkpointType;
            this.position = position;
            this.nextPosition = nextPosition;
            this.radius = radius;
            this.color = color;
            this.reserved = reserved;
            this.setToGround = setZtoGround;
            this.height = height;

            CreateCheckpoint();
        }

        /// <summary>
        /// The checkpoint types represents how the checkpoints looks in the game. 
        /// </summary>
        /// <remarks>
        /// Checkpoint type 47 for example is a simple cylinder with no arrow or number.
        /// Visit the <a href="https://nativedb.dotindustries.dev/natives?search=checkpoint">Grand Theft Auto V Native DB</a> for a list of all checkpoint types.
        /// A set operation on this property will recreate the checkpoint.
        /// </remarks>
        public int CheckpointType
        {
            get => this.checkpointType;
            set
            {
                this.checkpointType = value;
                ReCreateCheckpoint();
            }
        }

        public Color Color
        {
            get => this.color;
            set
            {
                this.color = value;
                UpdateColor(value);
            }
        }

        /// <summary>
        /// The ID of this checkpoint.
        /// </summary>
        public int Handle { get; private set; }

        public float Height
        {
            get => this.height;
            set
            {
                this.height = value;
                SetHeight(this.height, this.height, Radius);
            }
        }

        public bool IsValid { get; private set; }

        /// <summary>
        /// The position of the checkpoint after this one. 
        /// </summary>
        /// <remarks>
        /// A set operation on this property will recreate the checkpoint.
        /// </remarks>
        public Vector3 NextPosition
        {
            get => this.nextPosition;
            set
            {
                this.nextPosition = value;
                ReCreateCheckpoint();
            }
        }

        /// <summary>
        /// The position within the game. 
        /// </summary>
        /// <remarks>
        /// A set operation on this property will recreate the checkpoint.
        /// </remarks>
        public Vector3 Position
        {
            get => this.position;
            set
            {
                this.position = value;
                ReCreateCheckpoint();
            }
        }

        /// <summary>
        /// The radius of this checkpoint instance.
        /// </summary>
        /// <remarks>
        /// A set operation on this property will recreate the checkpoint.
        /// </remarks>
        public float Radius
        {
            get => this.radius;
            set
            {
                this.radius = value;
                ReCreateCheckpoint();
            }
        }

        /// <summary>
        /// Some <see cref="CheckpointType"/>s can display an icon or number. The number or icon can be set via this Reserved property. 
        /// </summary>
        /// <remarks>
        /// A set operation on this property will recreate the checkpoint.
        /// </remarks>
        public int Reserved
        {
            get => this.reserved;
            set
            {
                this.reserved = value;
                ReCreateCheckpoint();
            }
        }

        public static implicit operator bool(Checkpoint checkpoint)
        {
            return checkpoint != null && checkpoint.IsValid;
        }
        /// <inheritdoc/>
        public void Delete()
        {
            if (IsValid)
            {
                NativeWrappers.DeleteCheckpoint(Handle);
            }

            IsValid = false;
        }
        /// <inheritdoc/>
        public float DistanceTo(ISpatial target)
        {
            return Position.DistanceTo(target);
        }
        /// <inheritdoc/>
        public float DistanceTo(Vector3 target)
        {
            return Position.DistanceTo(target);
        }
        /// <inheritdoc/>
        public float DistanceTo2D(Vector3 target)
        {
            return Position.DistanceTo2D(target);
        }
        /// <inheritdoc/>
        public float DistanceTo2D(ISpatial target)
        {
            return Position.DistanceTo2D(target);
        }
        /// <inheritdoc/>
        public float TravelDistanceTo(Vector3 target)
        {
            return Position.TravelDistanceTo(target);
        }
        /// <inheritdoc/>
        public float TravelDistanceTo(ISpatial target)
        {
            return Position.TravelDistanceTo(target);
        }

        private void CreateCheckpoint()
        {
            var checkpointPosition = Position;
            if (this.setToGround)
            {
                checkpointPosition = Position.HeightToGround();
            }

            try
            {
                Handle = NativeWrappers.CreateCheckpoint(CheckpointType, checkpointPosition, NextPosition, Radius, Color, Reserved);
                SetHeight(Height, Height, Radius);
                IsValid = true;
                Game.LogTrivialDebug("Created checkpoint, handle = " + Handle);
            }
            catch (Exception e)
            {
                Game.LogTrivialDebug("Exception trying to create checkpoint: " + e.Message);
                Game.LogTrivialDebug(e.StackTrace);
                IsValid = false;
            }
        }

        private void ReCreateCheckpoint()
        {
            Delete();
            CreateCheckpoint();
        }

        private void SetHeight(float near, float far, float radius)
        {
            if (IsValid)
            {
                NativeWrappers.SetCheckpointCylinderHeight(Handle, near, far, radius);
            }
        }

        private void UpdateColor(Color newColor)
        {
            if (IsValid)
            {
                NativeWrappers.SetCheckpointColor(Handle, newColor);
            }
        }
    }
}
