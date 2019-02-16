﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2019, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2019. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.480)
//  Version 5.480.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;

namespace ComponentFactory.Krypton.Workspace
{
    /// <summary>
    /// A size where the width and height are in star notation.
    /// </summary>
    public class StarSize
    {
        #region Internal Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the StarSize class.
        /// </summary>
        public StarSize()
            : this("50*,50*")
        {
        }

        /// <summary>
        /// Initialize a new instance of the StarSize class.
        /// </summary>
        /// <param name="starSize">Initial star sizing value.</param>
        public StarSize(string starSize)
        {
            StarWidth = new StarNumber();
            StarHeight = new StarNumber();
            Value = starSize;
        }

        /// <summary>
        /// Gets a string representing the value of the instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the star notation and breaks it apart.
        /// </summary>
        public string Value
        {
            get => StarWidth.ToString() + "," + StarHeight.ToString();

            set
            {
                // Validate the incoming value
                if (value == null)
                {
                    throw new ArgumentNullException("Cannot be assigned a null value.");
                }

                // Split the string into comma separated parts
                string[] parts = value.Split(',');

                // Must consist of two values
                if (parts.Length != 2)
                {
                    throw new ArgumentNullException("Value must have two values separated by a comma.");
                }

                // Parse both halfs, exceptions are thrown if a problem occurs
                StarNumber width = new StarNumber(parts[0]);
                StarNumber height = new StarNumber(parts[1]);
            
                // No errors, so use the values
                StarWidth.Value = width.Value;
                StarHeight.Value = height.Value;
            }
        }

        /// <summary>
        /// Gets the star number for the width.
        /// </summary>
        public StarNumber StarWidth { get; }

        /// <summary>
        /// Gets the star number for the height.
        /// </summary>
        public StarNumber StarHeight { get; }

        #endregion

        #region Internal
        internal string PersistString
        {
            get => StarWidth.PersistString + ":" + StarHeight.PersistString;

            set
            {
                string[] parts = value.Split(':');
                StarWidth.PersistString = parts[0];
                StarHeight.PersistString = parts[1];
            }
        }
        #endregion
    }
}
