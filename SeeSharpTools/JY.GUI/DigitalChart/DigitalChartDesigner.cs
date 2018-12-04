﻿using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace SeeSharpTools.JY.GUI
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class DigitalChartDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(
                        new DigitalChartActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
    internal class DigitalChartActionList : System.ComponentModel.Design.DesignerActionList
    {
        private DigitalChart colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public DigitalChartActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as DigitalChart;

            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        private void OpenProperty()
        {
            
            var parentControl = (DigitalChart)colUserControl;
            var EasyChartProperty = new DigitalChartPropertyForm((DigitalChart)parentControl);
            EasyChartProperty.ShowDialog();
            //每一次都要改变
            //只改变BackColor进行Designer.cs的强制更新
            GetPropertyByName("BackColor").SetValue(colUserControl, parentControl.BackColor);
        }


        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        // Properties that are targets of DesignerActionPropertyItem entries.
        /*public string[] SeriesNames
        {
            get
            {
                return colUserControl.SeriesNames;
            }
            set
            {
                GetPropertyByName("SeriesNames").SetValue(colUserControl, value);

            }
        }
        public Color[] SeriesColor
        {
            get
            {
                return colUserControl.Palette;
            }
            set
            {
                GetPropertyByName("Palette").SetValue(colUserControl, value);
            }
        }*/

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));
//            items.Add(new DesignerActionPropertyItem("SeriesNames",
//                                 "SeriesNames", "Appearance",
//                                 "Set the names of series."));
//            items.Add(new DesignerActionPropertyItem("SeriesColor",
//                                 "SeriesColor", "Appearance",
//                                 "Set the color of series."));
            items.Add(new DesignerActionMethodItem(this, "OpenProperty", "property"));


            return items;
        }

    }
}