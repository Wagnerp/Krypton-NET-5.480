﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.480)
//  Version 5.480.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.Drawing;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Provide inheritance of palette ribbon background and text properties from source redirector.
    /// </summary>
    public class PaletteRibbonDoubleInheritRedirect : PaletteRibbonDoubleInherit
    {
        #region Instance Fields
        private PaletteRedirect _redirect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonDoubleInheritRedirect class.
        /// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        /// <param name="styleBack">Ribbon item background style.</param>
        /// <param name="styleText">Ribbon item text style.</param>
        public PaletteRibbonDoubleInheritRedirect(PaletteRedirect redirect,
                                                  PaletteRibbonBackStyle styleBack,
                                                  PaletteRibbonTextStyle styleText)
        {
            Debug.Assert(redirect != null);

            _redirect = redirect;
            StyleBack = styleBack;
            StyleText = styleText;
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _redirect = redirect;
        }
        #endregion

        #region StyleBack
        /// <summary>
        /// Gets and sets the ribbon background style to use when inheriting.
        /// </summary>
        public PaletteRibbonBackStyle StyleBack { get; set; }

        #endregion

        #region StyleText
        /// <summary>
        /// Gets and sets the ribbon text style to use when inheriting.
        /// </summary>
        public PaletteRibbonTextStyle StyleText { get; set; }

        #endregion

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
        {
            return _redirect.GetRibbonBackColorStyle(StyleBack, state);
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteState state)
        {
            return _redirect.GetRibbonBackColor1(StyleBack, state);
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteState state)
        {
            return _redirect.GetRibbonBackColor2(StyleBack, state);
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteState state)
        {
            return _redirect.GetRibbonBackColor3(StyleBack, state);
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteState state)
        {
            return _redirect.GetRibbonBackColor4(StyleBack, state);
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteState state)
        {
            return _redirect.GetRibbonBackColor5(StyleBack, state);
        }
        #endregion

        #region IPaletteRibbonText
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteState state)
        {
            return _redirect.GetRibbonTextColor(StyleText, state);
        }
        #endregion
    }
}
