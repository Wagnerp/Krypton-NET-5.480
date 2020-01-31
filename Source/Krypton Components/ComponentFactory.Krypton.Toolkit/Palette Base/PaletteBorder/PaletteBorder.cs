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
using System.Drawing.Design;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for palette border details.
    /// </summary>
    public class PaletteBorder : Storage, 
                                 IPaletteBorder
    {
        #region Internal Classes
        private class InternalStorage
        {
            public InheritBool BorderDraw;
            public PaletteDrawBorders BorderDrawBorders;
            public PaletteGraphicsHint BorderGraphicsHint;
            public Color BorderColor1;
            public Color BorderColor2;
            public PaletteColorStyle BorderColorStyle;
            public PaletteRectangleAlign BorderColorAlign;
            public float BorderColorAngle;
            public int BorderWidth;
            public int BorderRounding;
            public Image BorderImage;
            public PaletteImageStyle BorderImageStyle;
            public PaletteRectangleAlign BorderImageAlign;

            /// <summary>
            /// Initialize a new instance of the InternalStorage structure.
            /// </summary>
            public InternalStorage()
            {
                // Set to default values
                BorderDraw = InheritBool.Inherit;
                BorderDrawBorders = PaletteDrawBorders.All;
                BorderGraphicsHint = PaletteGraphicsHint.Inherit;
                BorderColor1 = Color.Empty;
                BorderColor2 = Color.Empty;
                BorderColorStyle = PaletteColorStyle.Inherit;
                BorderColorAlign = PaletteRectangleAlign.Inherit;
                BorderColorAngle = -1;
                BorderWidth = -1;
                BorderRounding = -1;
                BorderImageStyle = PaletteImageStyle.Inherit;
                BorderImageAlign = PaletteRectangleAlign.Inherit;
            }

            /// <summary>
            /// Gets a value indicating if all values are default.
            /// </summary>
            public bool IsDefault => (BorderDraw == InheritBool.Inherit) &&
                                     (BorderDrawBorders == PaletteDrawBorders.Inherit) &&
                                     (BorderGraphicsHint == PaletteGraphicsHint.Inherit) &&
                                     (BorderColor1 == Color.Empty) &&
                                     (BorderColor2 == Color.Empty) &&
                                     (BorderColorStyle == PaletteColorStyle.Inherit) &&
                                     (BorderColorAlign == PaletteRectangleAlign.Inherit) &&
                                     (BorderColorAngle == -1) &&
                                     (BorderWidth == -1) &&
                                     (BorderRounding == -1) &&
                                     (BorderImage == null) &&
                                     (BorderImageStyle == PaletteImageStyle.Inherit) &&
                                     (BorderImageAlign == PaletteRectangleAlign.Inherit);
        }
        #endregion

        #region Instance Fields
        private IPaletteBorder _inherit;
        private InternalStorage _storage;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBorder class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBorder(IPaletteBorder inherit,
                             NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Remember inheritance
            _inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (_storage == null) || _storage.IsDefault;

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteBorder inherit)
        {
            _inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Draw = GetBorderDraw(state);
            DrawBorders = GetBorderDrawBorders(state);
            GraphicsHint = GetBorderGraphicsHint(state);
            Color1 = GetBorderColor1(state);
            Color2 = GetBorderColor2(state);
            ColorStyle = GetBorderColorStyle(state);
            ColorAlign = GetBorderColorAlign(state);
            ColorAngle = GetBorderColorAngle(state);
            Width = GetBorderWidth(state);
            Rounding = GetBorderRounding(state);
            Image = GetBorderImage(state);
            ImageStyle = GetBorderImageStyle(state);
            ImageAlign = GetBorderImageAlign(state);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Should border be drawn.")]
        [DefaultValue(typeof(InheritBool), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public InheritBool Draw
        {
            get => _storage?.BorderDraw ?? InheritBool.Inherit;

            set 
            {
                if (_storage != null)
                {
                    if (_storage.BorderDraw != value)
                    {
                        _storage.BorderDraw = value;
                        OnPropertyChanged("Draw");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != InheritBool.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderDraw = value
                        };
                        OnPropertyChanged("Draw");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the actual border draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBorderDraw(PaletteState state) => Draw != InheritBool.Inherit ? Draw : _inherit.GetBorderDraw(state);
        #endregion

        #region DrawBorders
        /// <summary>
        /// Gets a value indicating which borders should be drawn.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Specify which borders should be drawn.")]
        [DefaultValue(typeof(PaletteDrawBorders), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [Editor(typeof(PaletteDrawBordersEditor), typeof(UITypeEditor))]
        public PaletteDrawBorders DrawBorders
        {
            get => _storage?.BorderDrawBorders ?? PaletteDrawBorders.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderDrawBorders != value)
                    {
                        _storage.BorderDrawBorders = value;
                        OnPropertyChanged("DrawBorders");
                        PerformNeedPaint(true);
                    }
                }
                else
                {
                    if (value != PaletteDrawBorders.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderDrawBorders = value
                        };
                        OnPropertyChanged("DrawBorders");
                        PerformNeedPaint(true);
                    }
                }
            }
        }

        private bool ShouldSerializeDrawBorders() => (DrawBorders != PaletteDrawBorders.Inherit);

        /// <summary>
        /// Gets the actual borders to draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public PaletteDrawBorders GetBorderDrawBorders(PaletteState state) => DrawBorders != PaletteDrawBorders.Inherit ? DrawBorders : _inherit.GetBorderDrawBorders(state);

        #endregion

        #region GraphicsHint
        /// <summary>
        /// Gets the graphics hint for drawing the border.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Hint for drawing graphics.")]
        [DefaultValue(typeof(PaletteGraphicsHint), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteGraphicsHint GraphicsHint
        {
            get => _storage?.BorderGraphicsHint ?? PaletteGraphicsHint.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderGraphicsHint != value)
                    {
                        _storage.BorderGraphicsHint = value;
                        OnPropertyChanged("GraphicsHint");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteGraphicsHint.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderGraphicsHint = value
                        };
                        OnPropertyChanged("GraphicsHint");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the actual border graphics hint value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state) =>
            GraphicsHint != PaletteGraphicsHint.Inherit ? GraphicsHint : _inherit.GetBorderGraphicsHint(state);
        #endregion

        #region Color1
        /// <summary>
        /// Gets the first border color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Main border color.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color1
        {
            get => _storage?.BorderColor1 ?? Color.Empty;

            set 
            {
                if (_storage != null)
                {
                    if (_storage.BorderColor1 != value)
                    {
                        _storage.BorderColor1 = value;
                        OnPropertyChanged("Color1");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != Color.Empty)
                    {
                        _storage = new InternalStorage
                        {
                            BorderColor1 = value
                        };
                        OnPropertyChanged("Color1");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the actual first border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor1(PaletteState state) => Color1 != Color.Empty ? Color1 : _inherit.GetBorderColor1(state);

        #endregion

        #region Color2
        /// <summary>
        /// Gets and sets the second border color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Secondary border color.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color2
        {
            get => _storage?.BorderColor2 ?? Color.Empty;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderColor2 != value)
                    {
                        _storage.BorderColor2 = value;
                        OnPropertyChanged("Color2");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != Color.Empty)
                    {
                        _storage = new InternalStorage
                        {
                            BorderColor2 = value
                        };
                        OnPropertyChanged("Color2");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor2(PaletteState state) => Color2 != Color.Empty ? Color2 : _inherit.GetBorderColor2(state);
        #endregion

        #region ColorStyle
        /// <summary>
        /// Gets and sets the color drawing style.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border color drawing style.")]
        [DefaultValue(typeof(PaletteColorStyle), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteColorStyle ColorStyle
        {
            get => _storage?.BorderColorStyle ?? PaletteColorStyle.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderColorStyle != value)
                    {
                        _storage.BorderColorStyle = value;
                        OnPropertyChanged("ColorStyle");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteColorStyle.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderColorStyle = value
                        };
                        OnPropertyChanged("ColorStyle");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBorderColorStyle(PaletteState state) => ColorStyle != PaletteColorStyle.Inherit
            ? ColorStyle
            : _inherit.GetBorderColorStyle(state);
        #endregion

        #region ColorAlign
        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border color alignment style.")]
        [DefaultValue(typeof(PaletteRectangleAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteRectangleAlign ColorAlign
        {
            get => _storage?.BorderColorAlign ?? PaletteRectangleAlign.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderColorAlign != value)
                    {
                        _storage.BorderColorAlign = value;
                        OnPropertyChanged("ColorAlign");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteRectangleAlign.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderColorAlign = value
                        };
                        OnPropertyChanged("ColorAlign");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBorderColorAlign(PaletteState state) =>
            ColorAlign != PaletteRectangleAlign.Inherit ? ColorAlign : _inherit.GetBorderColorAlign(state);
        #endregion

        #region ColorAngle
        /// <summary>
        /// Gets and sets the color angle.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border color angle.")]
        [DefaultValue(-1f)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public float ColorAngle
        {
            get 
            {
                if (_storage == null)
                {
                    return -1;
                }
                else
                {
                    return _storage.BorderColorAngle;
                }
            }

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderColorAngle != value)
                    {
                        _storage.BorderColorAngle = value;
                        OnPropertyChanged("ColorAngle");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != -1f)
                    {
                        _storage = new InternalStorage
                        {
                            BorderColorAngle = value
                        };
                        OnPropertyChanged("ColorAngle");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBorderColorAngle(PaletteState state) => ColorAngle != -1 ? ColorAngle : _inherit.GetBorderColorAngle(state);
        #endregion

        #region Width
        /// <summary>
        /// Gets and sets the border width.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border width.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int Width
        {
            get 
            {
                if (_storage == null)
                {
                    return -1;
                }
                else
                {
                    return _storage.BorderWidth;
                }
            }

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderWidth != value)
                    {
                        _storage.BorderWidth = value;
                        OnPropertyChanged("Width");
                        PerformNeedPaint(true);
                    }
                }
                else
                {
                    if (value != -1)
                    {
                        _storage = new InternalStorage
                        {
                            BorderWidth = value
                        };
                        OnPropertyChanged("Width");
                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border width.</returns>
        public int GetBorderWidth(PaletteState state) => Width != -1 ? Width : _inherit.GetBorderWidth(state);
        #endregion

        #region Rounding
        /// <summary>
        /// Gets and sets the border rounding.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("How much to round the border corners.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int Rounding
        {
            get 
            {
                if (_storage == null)
                {
                    return -1;
                }
                else
                {
                    return _storage.BorderRounding;
                }
            }

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderRounding != value)
                    {
                        _storage.BorderRounding = value;
                        OnPropertyChanged("Rounding");
                        PerformNeedPaint(true);
                    }
                }
                else
                {
                    if (value != -1)
                    {
                        _storage = new InternalStorage
                        {
                            BorderRounding = value
                        };
                        OnPropertyChanged("Rounding");
                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the border rounding.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border rounding.</returns>
        public int GetBorderRounding(PaletteState state) => Rounding != -1 ? Rounding : _inherit.GetBorderRounding(state);
        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the border image.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border image.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Image
        {
            get => _storage?.BorderImage;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderImage != value)
                    {
                        _storage.BorderImage = value;
                        OnPropertyChanged("Image");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != null)
                    {
                        _storage = new InternalStorage
                        {
                            BorderImage = value
                        };
                        OnPropertyChanged("Image");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBorderImage(PaletteState state) => Image ?? _inherit.GetBorderImage(state);
        #endregion

        #region ImageStyle
        /// <summary>
        /// Gets and sets the border image style.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border image style.")]
        [DefaultValue(typeof(PaletteImageStyle), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteImageStyle ImageStyle
        {
            get => _storage?.BorderImageStyle ?? PaletteImageStyle.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderImageStyle != value)
                    {
                        _storage.BorderImageStyle = value;
                        OnPropertyChanged("ImageStyle");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteImageStyle.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderImageStyle = value
                        };
                        OnPropertyChanged("ImageStyle");
                        PerformNeedPaint();
                    }
                }
            }
        }

        private bool ShouldSerializeImageStyle()
        {
            return (ImageStyle != PaletteImageStyle.Inherit);
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBorderImageStyle(PaletteState state) => ImageStyle != PaletteImageStyle.Inherit
            ? ImageStyle
            : _inherit.GetBorderImageStyle(state);
        #endregion

        #region ImageAlign
        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border image alignment style.")]
        [DefaultValue(typeof(PaletteRectangleAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteRectangleAlign ImageAlign
        {
            get => _storage?.BorderImageAlign ?? PaletteRectangleAlign.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.BorderImageAlign != value)
                    {
                        _storage.BorderImageAlign = value;
                        OnPropertyChanged("ImageAlign");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteRectangleAlign.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            BorderImageAlign = value
                        };
                        OnPropertyChanged("ImageAlign");
                        PerformNeedPaint();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBorderImageAlign(PaletteState state) =>
            ImageAlign != PaletteRectangleAlign.Inherit ? ImageAlign : _inherit.GetBorderImageAlign(state);
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="property">Name of the property changed.</param>
        protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion
    }
}
