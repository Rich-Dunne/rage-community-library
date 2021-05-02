﻿using System;
using Rage;
using RageCommunity.Library.Wrappers;

namespace RageCommunity.Library.Extensions
{
    public static class VehicleExtensions
    {
        /// <summary>
        /// Makes this <see cref="Vehicle"/> honk it's horn for the given <paramref name="duration"/> (in ms).
        /// </summary>
        public static void HonkHorn(this Vehicle vehicle, int duration, bool heldDown = false, bool forever = false) => NativeWrappers.StartVehicleHorn(vehicle, duration, heldDown, forever);
        /// <summary>
        /// get the <see cref="Vehicle"/> display name or the <see cref="Game"/> name
        /// </summary>
        public static string GetDisplayName(this Model vehicleModel)
        {
            string labelName = NativeWrappers.GetDisplayNameFromVehicleModel(vehicleModel.Hash);
            return NativeWrappers.GetLabelText(labelName);
        }
        /// <summary>
        /// get the <see cref="Vehicle"/> manufacturer name
        /// </summary>
        public static string GetMakeName(this Model vehicleModel)
        {
            string labelName = NativeWrappers.GetMakeNameFromVehicleModel(vehicleModel.Hash);
            return NativeWrappers.GetLabelText(labelName);
        }
        /// <summary>
        /// get the <see cref="Vehicle"/> display name or the <see cref="Game"/> name
        /// </summary>
        public static string GetDisplayName(this Vehicle vehicle) => GetDisplayName(vehicle.Model);
        /// <summary>
        /// get the <see cref="Vehicle"/> manufacturer name
        /// </summary>
        public static string GetMakeName(this Vehicle vehicle) => GetMakeName(vehicle.Model);
        /// <summary>
        /// randomise the given <paramref name="vehicle"/> license plate
        /// </summary>
        /// <remarks>
        /// Source: <a href="https://github.com/Albo1125/Albo1125-Common/blob/master/Albo1125.Common/CommonLibrary/ExtensionMethods.cs#L454"></a>
        /// </remarks>
        public static void RandomiseLicensePlate(this Vehicle vehicle)
        {
            if (vehicle)
            {
                vehicle.LicensePlate = MathHelper.GetRandomInteger(9).ToString() +
                                       MathHelper.GetRandomInteger(9).ToString() +
                                       Convert.ToChar(MathHelper.GetRandomInteger(0, 25) + 65) +
                                       Convert.ToChar(MathHelper.GetRandomInteger(0, 25) + 65) +
                                       Convert.ToChar(MathHelper.GetRandomInteger(0, 25) + 65) +
                                       MathHelper.GetRandomInteger(9).ToString() +
                                       MathHelper.GetRandomInteger(9).ToString() +
                                       MathHelper.GetRandomInteger(9).ToString();
            }
        }
    }
}
