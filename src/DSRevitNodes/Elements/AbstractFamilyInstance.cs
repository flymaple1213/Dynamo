﻿using System;
using System.ComponentModel;
using Autodesk.DesignScript.Geometry;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using DSNodeServices;
using DSRevitNodes.Elements;
using DSRevitNodes.GeometryObjects;
using RevitServices.Persistence;
using RevitServices.Transactions;
using Point = Autodesk.DesignScript.Geometry.Point;
using Curve = Autodesk.DesignScript.Geometry.Curve;
using Face = Autodesk.DesignScript.Geometry.Face;

namespace DSRevitNodes.Elements
{
    /// <summary>
    /// An abstract Revit FamilyInstance - implementors include FamilyInstance, AdaptiveComponent, StructuralFraming
    /// </summary>
    [RegisterForTrace]
    [Browsable(false)]
    public abstract class AbstractFamilyInstance : AbstractElement
    {

        #region Internal properties

        /// <summary>
        /// Reference to the Element
        /// </summary>
        internal Autodesk.Revit.DB.FamilyInstance InternalFamilyInstance
        {
            get;
            private set;
        }

        /// <summary>
        /// Reference to the Element
        /// </summary>
        public override Autodesk.Revit.DB.Element InternalElement
        {
            get { return InternalFamilyInstance; }
        }


        #endregion

        #region Protected mutators

        protected void InternalSetFamilyInstance(Autodesk.Revit.DB.FamilyInstance fi)
        {
            this.InternalFamilyInstance = fi;
            this.InternalElementId = fi.Id;
            this.InternalUniqueId = fi.UniqueId;
        }

        protected void InternalSetFamilySymbol(Autodesk.Revit.DB.FamilySymbol fs)
        {
            TransactionManager.GetInstance().EnsureInTransaction(Document);

            InternalFamilyInstance.Symbol = fs;

            TransactionManager.GetInstance().TransactionTaskDone();
        }

        #endregion

        #region Public properties

        public DSFamilySymbol Symbol
        {
            get
            {
                return DSFamilySymbol.FromExisting(this.InternalFamilyInstance.Symbol, true);
            }
        }

        public Point Location
        {
            get
            {
                var pos = this.InternalFamilyInstance.Location as LocationPoint;
                return Point.ByCoordinates(pos.Point.X, pos.Point.Y, pos.Point.Z);
            }
        }

        #endregion

    }
}