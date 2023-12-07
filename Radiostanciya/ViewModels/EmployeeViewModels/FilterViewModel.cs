using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.EmployeeViewModels
{
    public class FilterViewModel
    {
        public SelectList Employees { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Employee> emp, int? id, string name)
        {
            emp.Insert(0, new Models.Employee { Id = 0 });
            Employees = new SelectList(emp, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
