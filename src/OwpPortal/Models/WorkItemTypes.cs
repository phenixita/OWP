using System.ComponentModel.DataAnnotations;

namespace owp_web.Models
{
    public enum WorkItemTypes
    {
        [Display(Name="Unspecified")]
        Unspecified = 0,

        [Display(Name= "Asphalt Maintenance")]
        AsphaltMaintenance,

        [Display(Name = "Paving")]
        Paving,

        [Display(Name = "Bridge Repair")]
        BridgeRepair,

        [Display(Name = "Street Sweeping")]
        StreetSweeping,

        [Display(Name = "Traffic Lights")]
        TrafficLights,

        [Display(Name = "Paint Striping")]
        PaintStriping,

        [Display(Name = "Road Inspections")]
        RoadInspections,

        [Display(Name = "StormWater Management")]
        StormWaterManagement
    }
}
