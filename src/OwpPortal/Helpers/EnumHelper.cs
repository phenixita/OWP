using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace owp_web.Helpers
{
    public class EnumHelper
    {
        public static IEnumerable<SelectListItem> ToList<TEnum, TCast>(bool useDisplayName, string SelectedValue)
        {
            Array enumArray = Enum.GetValues(typeof(TEnum));
            List<SelectListItem> selectListValues = new List<SelectListItem>();
            foreach (var val in enumArray)
            {
                selectListValues.Add(new SelectListItem()
                {
                    Text = DisplayName((Enum)val),
                    Value = useDisplayName ? DisplayName((Enum)val) : ((TCast)val).ToString(),
                    Selected = SelectedValue == null ? false : ((TCast)val).ToString().Equals(SelectedValue, StringComparison.OrdinalIgnoreCase)
                });
            }
            return selectListValues;
        }
        public static string DisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                          .GetMember(enumValue.ToString())
                          .FirstOrDefault()
                          .GetCustomAttribute<DisplayAttribute>()
                          .GetName();
        }

    }
}
