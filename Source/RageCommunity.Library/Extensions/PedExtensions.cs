﻿using System;
using Rage;
using RageCommunity.Library.Wrappers;
using RageCommunity.Library.Peds;
using System.Linq;
using System.Collections.Generic;
using RageCommunity.Library.Task;

namespace RageCommunity.Library.Extensions
{
    public static class PedExtensions
    {
        /// <summary>
        /// Causes this ped to face the specified <paramref name="entity"/> for some <paramref name="duration"/> (in ms).
        /// </summary>
        /// <param name="duration">In miliseconds (-1 is infinite)</param>
        public static void FaceEntity(this Ped ped, Entity entity, int duration) => NativeWrappers.TaskTurnPedToFaceEntity(ped, entity, duration);

        /// <summary>
        /// Causes this ped to shoot from a vehicle at the specified <paramref name="target"/> ped. (e.g. drive-bys)
        /// </summary>
        /// <param name="unknown">Unknown parameter, default 0</param>
        public static void ShootFromVehicle(this Ped ped, Ped target, float unknown = 0) => NativeWrappers.TaskVehicleShootAtPed(ped, target, unknown);

        /// <summary>
        /// Causes this ped to start playing the specified scenario.
        /// </summary>
        public static void StartScenarioInPlace(this Ped ped, Scenario scenario, int unkDelay = 0, bool playEnterAnimation = true) => NativeWrappers.TaskStartScenarioInPlace(ped, scenario, unkDelay, playEnterAnimation);

        /// <summary>
        /// Returns true if the specified task is active for this ped.
        /// </summary>
        public static bool IsTaskActive(this Ped ped, PedTask task) => NativeWrappers.GetIsTaskActive(ped, (int)task);

        /// <summary>
        /// Gets a list of all active tasks for this ped.
        /// </summary>
        public static List<PedTask> GetAllActiveTasks(this Ped ped)
        {
            var tasks = (PedTask[])Enum.GetValues(typeof(PedTask));
            return tasks.Where(t => ped.IsTaskActive(t)).ToList();
        }
    }
}
