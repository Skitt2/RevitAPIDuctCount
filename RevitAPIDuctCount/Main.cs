using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIDuctCount
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new DuctFilter(), "Выберите воздуховоды");
            var ductList = new List<Duct>();

            string info = string.Empty;
            foreach (var selectedElement in selectedElementRefList)
            {
                Duct oDuct = doc.GetElement(selectedElement) as Duct;
                ductList.Add(oDuct);
            }
            info += $"Количество воздуховодов: {ductList.Count}";

            TaskDialog.Show("Selection", info);

            return Result.Succeeded;
        }
    }
}
